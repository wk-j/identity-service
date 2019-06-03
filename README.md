## Identity Service

Authentication against Alfresco's Identity Service with .NET

[![NuGet](https://img.shields.io/nuget/v/wk.IdentityService.svg)](https://www.nuget.org/packages/wk.IdentityService)

## Usage

- `dotnet add package wk.IdentityService`
- [tests/WebApi/Startup.cs](tests/WebApi/Startup.cs)

## Development

1. `docker-compose up`
2. Open http://localhost:8080
3. Add client `my-api`
4. `dotnet run tests/WebApi`
5. Open http://localhost:500/api/hello/hello