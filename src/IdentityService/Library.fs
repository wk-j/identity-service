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
    SignedInRedirectUri: string
    CallbackPath: string
}

[<Extension>]
type Extensions =
    [<Extension>]
    static member AddIdentityService(services: IServiceCollection, options: IdentityServiceOptions) =
        services
          .AddAuthentication(fun options ->
            options.DefaultScheme <- "Cookies"
            options.DefaultChallengeScheme <- "oidc"
          )
          .AddCookie("Cookies")
          .AddOpenIdConnect("oidc", fun options ->
            options.Authority <- options.Authority
            options.ClientId <- options.ClientId
            options.ClientSecret <- options.ClientSecret
            options.ResponseType <- OpenIdConnectResponseType.CodeIdTokenToken
            options.CallbackPath <- options.CallbackPath
          )

