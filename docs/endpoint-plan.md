# RaceDay API Endpoint Plan

| Method | Route | Description | Role | Request body | Response |
|---|---|---|---|---|---|
| POST | `/api/auth/register` | Register Organiser/Participant | Public | fullName, email, password, role, phone | User |
| POST | `/api/auth/login` | Create session | Public | email, password | User |
| POST | `/api/auth/logout` | Clear session | Authenticated | none | 204 |
| GET | `/api/auth/me` | Current user | Authenticated | none | User |
| GET/PUT | `/api/profile` | View/update own profile | Authenticated | profile fields | User |
| GET | `/api/events` | Browse events | Public | none | Event[] |
| GET | `/api/events/{id}` | Event detail | Public | none | Event + categories |
| POST/PUT/DELETE | `/api/events[/{id}]` | Manage event | Organiser | event fields | Event/status |
| GET | `/api/events/{id}/categories` | List categories | Public | none | Category[] |
| POST | `/api/events/{id}/categories` | Add category | Organiser | category fields | Category |
| POST | `/api/events/{id}/enrollments` | Enrol in category | Participant | categoryId | Enrollment |
| GET | `/api/events/{id}/enrollments` | View event enrolments | Organiser | none | Enrollment[] |
| GET | `/api/profile/enrollments` | Own race history | Participant | none | Enrollment[] |
| POST | `/api/enrollments/{id}/result` | Record finish result | Organiser | finishTime, finishingPosition | Result |
