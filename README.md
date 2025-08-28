# JWT Authentication API in ASP.NET Core
This is a simple Web API built with **ASP.NET Core 9** that demonstrates **JWT Authentication** and **Role-based Authorization**.
## Features
- User Registration and Login
- JWT Token generation
- Role-based access (User / Admin)
- Protected endpoints using `[Authorize]`
- Swagger UI for testing APIs

## Tech Stack
- ASP.NET Core 9 (C#)
- Entity Framework Core + SQL Server
- ASP.NET Identity
- JWT Authentication
- Swagger UI

## Endpoints
- `POST /api/auth/register` → Register new user
- `POST /api/auth/login` → Login and get JWT token
- `GET /api/secure/user` → Accessible to logged-in users
- `GET /api/secure/admin` → Accessible only to Admin

