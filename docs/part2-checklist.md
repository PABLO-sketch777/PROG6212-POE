# Part 2 completion checklist

| Requirement | Evidence in this package |
|---|---|
| ASP.NET Core Web API in C# | `src/RaceDay.Api` targeting .NET 8 |
| SQL Server and EF Core code-first model | `Data/RaceDayDbContext.cs`, SQL Server package, `docs/schema.sql` |
| Session authentication and password hashing | `Controllers/AuthController.cs`, `Services/AuthServices.cs`, `Program.cs` |
| Organiser/Participant role enforcement | `SessionAuth`, event/category/result/enrolment controller checks |
| Profile management | `Controllers/ProfileController.cs` |
| Event management | `Controllers/EventsController.cs` |
| Categories, enrolments, results | `Controllers/EnrollmentsController.cs` |
| Swagger | `Program.cs` and `/swagger` |
| Unit tests for success/failure/security primitives | `tests/RaceDay.Api.Tests` |
| GitHub Actions build/test/document validation | `.github/workflows/ci.yml` |
| Part 1 preserved for Part 3 | `docs/erd.mmd`, `docs/erd.png`, `docs/endpoint-plan.md`, `docs/schema.sql` |

Before academic submission, add the real GitHub repository URL, a real green-build screenshot, and the unlisted YouTube demonstration URL to the README. Those evidence items cannot be created without access to the student's GitHub/YouTube accounts.
