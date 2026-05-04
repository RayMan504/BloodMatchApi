# BloodMatchApi

Small ASP.NET Core Web API that answers whether a donor blood type is compatible with a recipient blood type.

This repository contains a minimal .NET 8.0 Web API and a small xUnit test project.

## Contents

- `Program.cs` - minimal Web API host and endpoint wiring
- `Controllers/` - controllers including `BloodMatchController` and a reusable `ApiControllerBase`
- `Services/BloodMatchService.cs` - business logic for compatibility
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

Example request (curl):

```bash
curl "http://localhost:5127/api/BloodMatch/check?donor=O%2D&recipient=AB%2B"
```

## Tests

Run the test suite from the repository root (solution or test project):

```bash
# using solution
dotnet test BloodMatch.sln

# or directly
dotnet test BloodMatchApi/BloodMatchApi.Tests/BloodMatchApi.Tests.csproj
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
az appservice plan create --name my-plan --resource-group my-rg --sku B1
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

### GitHub Actions / CI

I can scaffold a GitHub Actions workflow that runs build, tests, and deploys to Azure Web App using the `azure/webapps-deploy` action. If you'd like that, tell me the desired app name and whether you want to use a publish profile or service principal.

## Notes & suggestions

- Add a `/health` endpoint for App Service health probes.
- Use App Settings (Azure Portal) or Key Vault for any secrets instead of appsettings.json.
- The project is intentionally small and minimal; expand tests to include edge cases (empty/null inputs).

---

If you want, I can add the GitHub Actions workflow now or add the `Dockerfile` to the repo and commit it.