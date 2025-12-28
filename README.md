 Offline Ticketing System – ASP.NET Core 8 Web API

Project Overview
This project is a simplified offline ticketing system designed to handle internal support requests within an organization.  
The implementation focuses only on the backend Web API—no frontend is included.
The solution is built using Clean Architecture / Layered Architecture principles.
---
Technologies Used
- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core
- SQLite (Local Database)
- JWT Authentication
- SOLID Principles
- Repository Pattern
- Unit Testing (xUnit, Moq)
- Swagger / OpenAPI
Solution Structure
Solution
├── Ticketing.Domain // Entities and Enums
├── Ticketing.Application // DTOs, Interfaces, Services
├── Ticketing.Infrastructure // EF Core, DbContext, Repositories
├── Ticketing.API // Controllers and Endpoints
└── Ticketing.Tests // Unit Tests
How to Run the Project

Prerequisites
- .NET SDK 8 installed
- Visual Studio 2022 or VS Code


Connection string (appsettings.json):

json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=ticketing.db"
}
Apply Migrations and Create Database
Using Package Manager Console:

Add-Migration InitialCreate
Update-Database
Or using .NET CLI:

dotnet ef database update
This will create a local ticketing.db file.

Run the API
dotnet run
Or press F5 in Visual Studio.

Swagger UI:

http://localhost:<port>/swagger
 Database Seeding (Initial Data)
Initial data is seeded using HasData inside TicketingDbContext.

Default Users
Role	Email	Password
Admin	admin@test.com	admin123
Employee	employee@test.com	emp123

Passwords are securely hashed using BCrypt.

Example seed code:

modelBuilder.Entity<User>().HasData(
    new User {
        Id = adminId,
        FullName = "Admin User",
        Email = "admin@test.com",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
        Role = UserRole.Admin
    },
    new User {
        Id = employeeId,
        FullName = "Employee User",
        Email = "employee@test.com",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("emp123"),
        Role = UserRole.Employee
    }
);
Authentication
Authentication is implemented using JWT Bearer Tokens

Login endpoint:

POST /auth/login
Request body:

{
  "email": "admin@test.com",
  "password": "admin123"
}
Use the returned token in Swagger via the Authorize button:

Copy code
Bearer <your_token_here>
Main API Endpoints
Ticket Management
Method	Endpoint	Access
POST	/tickets	Employee
GET	/tickets/my	Employee
GET	/tickets	Admin
PUT	/tickets/{id}	Admin
GET	/tickets/stats	Admin
GET	/tickets/{id}	Ticket creator / Assigned admin
DELETE	/tickets/{id}	Admin
 Unit Testing
Tests are located in the Ticketing.Tests project

Only the Application Layer is tested

Repositories are mocked using Moq

No EF Core or DbContext is used in unit tests

Run tests:

dotnet test
Assumptions & Design Decisions
The Application layer has no direct dependency on Infrastructure

DbContext is used only in the Infrastructure layer

AssignedToUserId in Ticket is nullable and must reference an Admin user

Business rules (e.g., role validation) are implemented in Services, not Controllers

Separate DTOs are used for Create and Update operations

Simple exceptions are used for business rule violations (can be extended to custom exceptions




