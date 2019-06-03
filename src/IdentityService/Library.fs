namespace IdentityService
open Microsoft.IdentityModel.Protocols.OpenIdConnect
open Microsoft.Extensions.DependencyInjection
open System.Runtime.CompilerServices
open Microsoft.AspNetCore.Http
open Microsoft.IdentityModel.Protocols.OpenIdConnect

[<CLIMutable>]
type IdentityServiceOptions =  {
    Authority: string
    ClientId: string
    ClientSecret: string
    CallbackPath: string
}

[<Extension>]
type Extensions =
    [<Extension>]
    static member AddIdentityService(services: IServiceCollection, so: IdentityServiceOptions) =
        services
          .AddAuthentication(fun options ->
            options.DefaultScheme <- "Cookies"
            options.DefaultChallengeScheme <- "oidc"
          )
          .AddCookie("Cookies")
          .AddOpenIdConnect("oidc", fun options ->
            options.Authority <- so.Authority
            options.ClientId <- so.ClientId
            options.ClientSecret <- so.ClientSecret
            options.CallbackPath <- PathString(so.CallbackPath)
            options.ResponseType <- OpenIdConnectResponseType.CodeIdTokenToken
            options.RequireHttpsMetadata <- false
          )
