namespace IdentityService
open Microsoft.IdentityModel.Protocols.OpenIdConnect
open Microsoft.Extensions.DependencyInjection
open System.Runtime.CompilerServices
open Microsoft.AspNetCore.Http
open Microsoft.IdentityModel.Protocols.OpenIdConnect
open Microsoft.AspNetCore.Authentication.OpenIdConnect
open Microsoft.AspNetCore.Authentication.JwtBearer
open Microsoft.IdentityModel.Tokens

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
