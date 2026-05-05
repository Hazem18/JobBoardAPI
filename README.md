# Job Board API

A RESTful API for connecting companies with candidates. Built with .NET 9 and Clean Architecture.

## Tech Stack
- ASP.NET Core 9 Web API
- Entity Framework Core + SQL Server
- ASP.NET Core Identity
- JWT Authentication
- AutoMapper
- Clean Architecture (4 layers)

## Features
- Company and candidate registration with role-based JWT auth
- Job listing management with filtering by location, salary, job type, keyword
- Job application system with status tracking (Pending, Reviewed, Accepted, Rejected)
- Ownership checks — companies can only manage their own listings
- Duplicate application prevention

## Architecture
4 layers following Clean Architecture:
- **Domain** — entities, interfaces, exceptions
- **Application** — services, DTOs, AutoMapper profiles
- **Infrastructure** — EF Core, repositories, Identity
- **API** — controllers, middleware, Program.cs

## Getting Started

### Prerequisites
- .NET 9 SDK
- SQL Server

### Setup
1. Clone the repository
2. Copy `API/appsettings.example.json` to `API/appsettings.json`
3. Fill in your SQL Server connection string and JWT secret key
4. Run migrations: `dotnet ef database update --project Infrastructure --startup-project API`
5. Run the API: `dotnet run --project API`
6. Open Swagger: `https://localhost:{port}/swagger`

## API Endpoints

### Auth
| Method | Route | Description |
|--------|-------|-------------|
| POST | /api/auth/RegisterCompany | Register a company |
| POST | /api/auth/RegisterCandidate | Register a candidate |
| POST | /api/auth/Login | Login and receive JWT token |

### Jobs
| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| GET | /api/jobs | Anonymous | Get all open jobs with filters |
| GET | /api/jobs/{id} | Anonymous | Get job by ID |
| POST | /api/jobs | Company | Create a job listing |
| PUT | /api/jobs/{id} | Company | Update a job listing |
| DELETE | /api/jobs/{id} | Company | Delete a job listing |
| PATCH | /api/jobs/{id} | Company | Close a job listing |

### Applications
| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| POST | /api/jobapplications/{jobId} | Candidate | Apply to a job |
| GET | /api/jobapplications/mine | Candidate | View my applications |
| GET | /api/jobapplications/{jobId} | Company | View applications to listing |
| PATCH | /api/jobapplications/{id}/status | Company | Update application status |
