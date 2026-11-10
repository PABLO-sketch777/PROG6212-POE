# RaceDay — PROG6212 POE (Parts 1–3)

This package contains the complete RaceDay solution: Part 1 planning evidence, the Part 2 REST API, and the Part 3 MVC application.

RaceDay is a session-authenticated ASP.NET Core Web API for a road-race system. **Organisers** create and manage events, define age/distance categories, and record results. **Participants** enrol in a category and view their personal race history. Both roles can update their own profile.

## Requirements
- .NET 8 SDK
- SQL Server (local instance or Docker)

## Run locally
1. Start SQL Server and update `src/RaceDay.Api/appsettings.json` or set `ConnectionStrings__DefaultConnection`.
2. Run `dotnet restore` and `dotnet run --project src/RaceDay.Api`.
3. Open `http://localhost:5080/swagger`.
4. Apply `docs/schema.sql` to create and seed the database, or use EF Core migrations when extending the project.

Sessions use an HttpOnly cookie; Swagger requests must retain cookies between calls. Passwords are PBKDF2-SHA256 hashed and never stored in plaintext. The SQL demo accounts are `organiser1@raceday.test` / `Organiser123!` and `participant1@raceday.test` / `Participant123!`.

## Tests and CI
`dotnet test` runs the unit tests. The solution was verified locally with .NET 8: `dotnet build RaceDay.sln --configuration Release` completes with 0 errors and 0 warnings, and all 3 automated tests pass. GitHub Actions runs restore, build, tests, and validates that `/docs` contains the ERD source, endpoint plan, and SQL schema on every push and pull request.

## Part 3 integration contract
The MVC client should call this API only. It can use the event/banner URL returned by the API; Azure Blob upload logic belongs in the API layer, preserving the required separation of concerns.

## Evidence
- Public GitHub repository: https://github.com/PABLO-sketch777/PROG6212-POE
- Swagger: `/swagger`
- Part 1 documents: `/docs`
- CI workflow: `.github/workflows/ci.yml`
- YouTube demonstration: **Pending — to be added after final recording**
- CI screenshot: **Pending — capture after pushing this repository to GitHub**

## Part 3: MVC web application
Run the API on port 5080, then run the MVC client with `dotnet run --project src/RaceDay.Web`; browse to `http://localhost:5090`. The MVC application calls the API for every data operation and stores the API session cookie in its own session. It does not access SQL Server directly.

## Docker
From the repository root, run `docker compose up --build`. The MVC app is available at `http://localhost:5090`. Set `ApiBaseUrl` to the reachable API URL when the API is hosted elsewhere.

## Azure Blob Storage
Set `AzureBlob__ConnectionString` and optionally `AzureBlob__Container=raceday-media` on the API. The MVC sends image files to the API endpoints `/api/events/{id}/banner` and `/api/profile/image`; the API uploads them to Azure Blob Storage and returns the URL. The MVC never contacts Azure directly. With no connection string, the API uses local `wwwroot/uploads` storage for development.

See `docs/part3-checklist.md` for a criterion-by-criterion map.
