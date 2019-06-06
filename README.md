## Identity Service

Authenticate against Alfresco's Identity Service with .NET

[![Build Status](https://dev.azure.com/wk-j/identity-service/_apis/build/status/wk-j.identity-service?branchName=master)](https://dev.azure.com/wk-j/identity-service/_build/latest?definitionId=45&branchName=master)
[![NuGet](https://img.shields.io/nuget/v/wk.IdentityService.svg)](https://www.nuget.org/packages/wk.IdentityService)

## Usage

- `dotnet add package wk.IdentityService`
- [tests/WebApi/Startup.cs](tests/WebApi/Startup.cs)

## Development

1. `docker-compose up`
2. Open http://localhost:8080
3. Add client `my-api`
4. `dotnet run tests/WebApi`
5. Open http://localhost:5000/api/hello/hello