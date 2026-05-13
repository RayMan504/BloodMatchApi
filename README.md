# BloodMatchApi

Small ASP.NET Core Web API that answers whether a donor blood type is compatible with a recipient blood type.

This repository contains a minimal .NET 8.0 Web API and a small xUnit test project.

## Contents

- `Program.cs` - minimal Web API host and endpoint wiring
- `Controllers/` - controllers including `BloodMatchController` and a reusable `ApiControllerBase`
- `Services/BloodMatchService.cs` - business logic for compatibility
- `Views/` - minimal html views
- `Models/` - simple model classes
- `BloodMatchApi.Tests/` - xUnit tests for the service

## Requirements

- .NET 8 SDK (dotnet 8)
- Optional: Azure CLI if deploying to Azure App Service

## Build

From the repository root:

```bash
dotnet build
```

## Run (local)

Run the API locally:

```bash
dotnet run --project ./
```

Then open `http://localhost:5000/swagger` (or check output for the exact URL) to try the endpoints.

### Home page dropdown

Open the app root at `http://localhost:5127/` and select a blood type from the dropdown. The page will trigger an API call to:

```text
/api/bloodtype/{bloodType}
```

For example, selecting `ABP` will navigate to `/api/bloodtype/ABP` and return the blood match result.

Example request (curl):

```bash
curl "http://localhost:5127/api/bloodtype/abp"
```

## Tests

Run the test suite from the repository root (solution or test project):

```bash
dotnet test BloodMatchApi/BloodMatchApi.Tests
```

## Docker (optional)

To containerize the app, create a `Dockerfile` like the one below and build/push to your registry.

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish BloodMatchApi.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BloodMatchApi.dll"]
```

## Deploy to Azure App Service (quick)

1. Install and sign in to Azure CLI:

```bash
az login
```

2. Create resource group and app service plan (example):

```bash
az group create --name my-rg --location westeurope
az appservice plan create --name my-plan --resource-group my-rg --sku F1
```

3. Create the web app (replace `my-unique-app-name`):

```bash
az webapp create --resource-group my-rg --plan my-plan --name my-unique-app-name --runtime "DOTNETCORE|8.0"
```

4. Publish and deploy:

```bash
dotnet publish -c Release -o publish
az webapp deploy --resource-group my-rg --name my-unique-app-name --src-path ./publish
```
