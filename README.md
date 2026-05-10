# HireWave — Job Board API & Web App

A full-stack job board platform connecting companies with candidates. Built with ASP.NET Core 9, Clean Architecture, React, and AI-powered features.

## Live Features

- 🏢 **Companies** — register, post job listings, review applicants, update application status
- 👤 **Candidates** — register, browse and filter jobs, apply with cover letters, track application status
- ✨ **AI Cover Letter Generator** — candidates get an AI-generated cover letter based on the job description and their profile
- 🤖 **AI Match Score** — companies see an AI-generated match score for each applicant with a one-line explanation
- 🔐 **Role-based authentication** — Company, Candidate, and Admin roles with JWT
- 📊 **Application tracking** — Pending → Reviewed → Accepted → Rejected status flow

---

## Tech Stack

### Backend
| Technology | Purpose |
|---|---|
| ASP.NET Core 9 | REST API |
| Clean Architecture | 4-layer structure (Domain, Application, Infrastructure, API) |
| ASP.NET Core Identity | User management, password hashing, roles |
| Entity Framework Core | Data access with SQL Server |
| AutoMapper | Entity to DTO mapping |
| JWT Bearer | Authentication and authorization |
| Gemini AI API | Cover letter generation and candidate match scoring |

### Frontend
| Technology | Purpose |
|---|---|
| React 18 + Vite | Frontend framework |
| Tailwind CSS | Styling |
| Axios | HTTP client |
| React Router | Navigation |

---

## Architecture

The backend follows Clean Architecture with strict dependency direction — dependencies always point inward.

```
JobBoardAPI/
├── Domain/              → Entities, interfaces, exceptions, enums
├── Application/         → Services, DTOs, AutoMapper profiles, AI interfaces
├── Infrastructure/      → EF Core, repositories, Identity, AI service (Gemini)
└── API/                 → Controllers, middleware, Program.cs
```

### Dependency Direction
```
API → Application → Domain
Infrastructure → Application → Domain
```

Domain has zero dependencies. Business logic never touches EF Core or any framework directly.

---

## API Endpoints

### Auth
| Method | Route | Auth | Description |
|---|---|---|---|
| POST | /api/auth/RegisterCompany | Anonymous | Register a company account |
| POST | /api/auth/RegisterCandidate | Anonymous | Register a candidate account |
| POST | /api/auth/Login | Anonymous | Login and receive JWT token |

### Jobs
| Method | Route | Auth | Description |
|---|---|---|---|
| GET | /api/jobs | Anonymous | Get all open jobs with filters |
| GET | /api/jobs/{id} | Anonymous | Get job detail |
| GET | /api/jobs/my-jobs | Company | Get company's own listings |
| POST | /api/jobs | Company | Create a job listing |
| PUT | /api/jobs/{id} | Company | Update a listing |
| DELETE | /api/jobs/{id} | Company | Delete a listing |
| PATCH | /api/jobs/{id} | Company | Close a listing |

### Applications
| Method | Route | Auth | Description |
|---|---|---|---|
| POST | /api/jobapplications/{jobId} | Candidate | Apply to a job |
| GET | /api/jobapplications/mine | Candidate | View my applications |
| GET | /api/jobapplications/{jobId} | Company | View applications for a listing |
| PATCH | /api/jobapplications/{id}/status | Company | Update application status |

### Profiles
| Method | Route | Auth | Description |
|---|---|---|---|
| GET | /api/candidates/{id} | Authorized | View candidate profile |
| GET | /api/candidates/me | Candidate | View own profile |
| GET | /api/companies/{id} | Anonymous | View company profile |
| GET | /api/companies/me | Company | View own profile |

### AI
| Method | Route | Auth | Description |
|---|---|---|---|
| POST | /api/ai/cover-letter | Candidate | Generate AI cover letter |
| POST | /api/ai/match-score | Company | Get AI match score for applicant |

---

## Job Filtering

`GET /api/jobs` supports query parameters:

```
/api/jobs?keyword=dotnet&location=Cairo&jobType=FullTime&minSalary=5000&maxSalary=15000
```

| Parameter | Type | Description |
|---|---|---|
| keyword | string | Search in title and description |
| location | string | Filter by location |
| jobType | string | FullTime, PartTime, Contract, Remote |
| minSalary | decimal | Minimum salary filter |
| maxSalary | decimal | Maximum salary filter |

---

## Getting Started

### Prerequisites
- .NET 9 SDK
- Node.js 18+
- SQL Server
- Gemini API key (free at [aistudio.google.com](https://aistudio.google.com))

### Backend Setup

1. Clone the repository
```bash
git clone https://github.com/Hazem18/JobBoardAPI.git
cd JobBoardAPI
```

2. Copy the example config and fill in your values
```bash
cp API/appsettings.example.json API/appsettings.json
```

3. Update `appsettings.json` with your values:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=JobBoardDB;Trusted_Connection=true;TrustServerCertificate=true;"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key-minimum-32-characters",
    "Issuer": "JobBoardAPI",
    "Audience": "JobBoardAPI",
    "ExpiryMinutes": 60
  },
  "Gemini": {
    "ApiKey": "your-gemini-api-key"
  }
}
```

4. Run migrations and start the API
```bash
dotnet ef database update --project Infrastructure --startup-project API
dotnet run --project API
```

5. Open Swagger UI at `https://localhost:{port}/swagger`

### Frontend Setup

```bash
cd job-board-client
npm install
npm run dev
```

Open `http://localhost:5173`

---

## Key Design Decisions

**Why Clean Architecture?**
Separating the domain from infrastructure means the business logic never depends on EF Core, ASP.NET, or any external library. Swapping the database or AI provider requires changing only the Infrastructure layer.

**Why ASP.NET Core Identity?**
Identity handles password hashing, user management, and roles out of the box. No manual BCrypt or custom user tables needed.

**Why server-side AI calls?**
The AI API key lives in `appsettings.json` on the server — never exposed to the browser. The frontend calls `/api/ai/cover-letter` and `/api/ai/match-score` on the backend, which then calls the AI provider.

**Why JWT over sessions?**
Stateless authentication scales horizontally without shared session storage. Tokens carry claims (user ID, role) so most requests need zero database lookups for auth.

---

## Business Rules

- A candidate cannot apply to the same job twice
- A candidate cannot apply to a closed job listing
- Only the company that posted a listing can update, delete, or close it
- Only the company that owns a listing can view or update its applications
- Deleting a listing cascades to delete all its applications

---

## Screenshots

### Landing Page
Clean hero section with job search and feature highlights.

### Browse Jobs
Filterable job cards with location, salary, type, and keyword search.

### Company Dashboard
Post jobs, view applicants, see AI match scores, update application status.

### Candidate Dashboard
Track all applications with real-time status updates.

### AI Cover Letter
One-click AI-generated cover letter based on job description and candidate profile.

---

## Project Structure

```
JobBoardAPI/
├── Domain/
│   ├── Entities/          ApplicationUser, Company, Candidate, JobListing, JobApplication
│   ├── Interfaces/        IJobListingRepository, IApplicationRepository, ICandidateRepository, ICompanyRepository
│   ├── Enums/             JobType, JobStatus, ApplicationStatus
│   └── Exceptions/        NotFoundException, UnauthorizedException, DuplicateApplicationException
│
├── Application/
│   ├── DTOs/              Auth, JobListing, JobApplication, Candidate, Company, AI
│   ├── Interfaces/        IJobListingService, IApplicationService, IAuthService, ITokenService, IAIService
│   ├── Services/          JobListingService, ApplicationService, AuthService, TokenService
│   └── Mappings/          JobListingProfile, JobApplicationProfile
│
├── Infrastructure/
│   ├── Data/              JobBoardContext, JobBoardContextFactory
│   ├── Repositories/      JobListingRepository, ApplicationRepository, CandidateRepository, CompanyRepository
│   ├── Services/          GeminiService (implements IAIService)
│   └── Migrations/
│
└── API/
    ├── Controllers/       AuthController, JobsController, JobApplicationsController, CandidatesController, CompaniesController, AIController
    └── Middleware/        ExceptionHandlingMiddleware
```

---

## Author

**Hazem Emad**
- GitHub: [@Hazem18](https://github.com/Hazem18)
- LinkedIn: [hazem-emad-hussien](https://linkedin.com/in/hazem-emad-hussien)
- Email: eng.hazemm.emad@gmail.com

---

*Built with ASP.NET Core 9, React, and Clean Architecture — 2026*
