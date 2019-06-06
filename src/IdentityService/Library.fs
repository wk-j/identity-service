namespace IdentityService

open Microsoft.IdentityModel.Tokens
open Microsoft.IdentityModel.Protocols.OpenIdConnect
open Microsoft.Extensions.DependencyInjection
open System.Runtime.CompilerServices
open Microsoft.AspNetCore.Http
open Microsoft.IdentityModel.Protocols.OpenIdConnect
open Microsoft.AspNetCore.Authentication.OpenIdConnect
open Microsoft.AspNetCore.Authentication.JwtBearer
open Microsoft.IdentityModel.Tokens
open System.Text

[<CLIMutable>]
type IdentityServiceOptions =  {
    Authority: string
    ClientId: string
    ClientSecret: string
    CallbackPath: string
    Events: OpenIdConnectEvents
}

[<Extension>]
type Extensions =
    [<Extension>]
    static member AddIdentityService(services: IServiceCollection, so: IdentityServiceOptions) =
        services
          .AddAuthentication(fun options ->
            options.DefaultScheme <- "Cookies"
            options.DefaultChallengeScheme <- OpenIdConnectDefaults.AuthenticationScheme
          )
          .AddCookie("Cookies")
          .AddJwtBearer(fun cfg ->
                cfg.RequireHttpsMetadata <- false
                cfg.Authority <- so.Authority
                cfg.IncludeErrorDetails <- true
                cfg.TokenValidationParameters <-
                    TokenValidationParameters(
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidIssuer = so.Authority,
                        ValidateLifetime = true
                    )
                cfg.Events <- JwtBearerEvents()
                cfg.Events.OnAuthenticationFailed <- fun c ->
                    c.NoResult();
                    c.Response.StatusCode <- 401
                    c.Response.ContentType <- "text/plain"
                    c.Response.WriteAsync(c.Exception.ToString())
          )
          .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, fun options ->
            options.Authority <- so.Authority
            options.ClientId <- so.ClientId
            options.ClientSecret <- so.ClientSecret
            options.CallbackPath <- PathString(so.CallbackPath)
            options.ResponseType <- OpenIdConnectResponseType.CodeIdTokenToken
            options.RequireHttpsMetadata <- false
            options.SaveTokens <- true
            options.Events <- so.Events
            options.GetClaimsFromUserInfoEndpoint <- true
          )
