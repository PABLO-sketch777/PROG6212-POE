# Part 3 completion checklist

- MVC project: `src/RaceDay.Web`
- MVC uses `ApiClient` for all data operations; no EF Core or SQL connection exists in the web project.
- Session state stores the API session cookie and current role.
- Organiser pages: dashboard, create event, banner upload, category management, enrolments, and results.
- Participant pages: upcoming events, event/category details, enrolment, personal history, and result display.
- Profile update and profile-picture upload route through the API.
- API-owned Azure Blob Storage is implemented in `BlobStorageService`; local uploads are used only when no Azure connection string is configured.
- Responsive Bootstrap-based RaceDay styling is in `wwwroot/css/site.css`.
- Dockerfile runs the MVC application with one command: `docker compose up --build`.
- Replace the README evidence placeholders with the student’s real GitHub URL, green-build screenshot, and unlisted YouTube URL before submission.
