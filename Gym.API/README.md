# Gym API

A RESTful backend API built with **ASP.NET Core Minimal APIs**, **Entity Framework Core**, and **SQLite** for managing workouts and exercises.

I built this project as part of my transition into C#/.NET development. My main goal was not only to build a working CRUD API, but to understand how a real ASP.NET Core application is structured — from receiving an HTTP request to accessing the database, returning responses, handling errors, testing, and securing endpoints with JWT authentication.

---

## Features

- Workout CRUD operations
- Exercise CRUD operations
- Workout and Exercise relationship
- Retrieve exercises belonging to a workout
- DTO-based request and response models
- Input validation
- Service layer for business logic
- Entity Framework Core database access
- SQLite database
- EF Core migrations
- Asynchronous database operations
- User registration
- Secure password hashing
- User login
- JWT token generation
- JWT authentication
- Protected API endpoints
- Global exception handling
- Application logging
- Configuration using `appsettings.json`
- Development-specific configuration
- OpenAPI documentation
- Scalar API interface
- Unit testing
- Integration testing

---

## Tech Stack

- **C#**
- **.NET 10**
- **ASP.NET Core Minimal APIs**
- **Entity Framework Core**
- **SQLite**
- **JWT Bearer Authentication**
- **xUnit**
- **OpenAPI**
- **Scalar**
- **Git / GitHub**

---

## Project Structure

```text
GYM/
│
├── Gym.API/
│   ├── Data/
│   │   ├── GymContext.cs
│   │   └── Migrations/
│   │
│   ├── Dtos/
│   │   ├── Exercise DTOs
│   │   ├── Workout DTOs
│   │   ├── RegisterDto.cs
│   │   └── LoginDto.cs
│   │
│   ├── Endpoints/
│   │   ├── ExerciseEndpoints.cs
│   │   ├── WorkoutEndpoints.cs
│   │   └── AuthEndpoints.cs
│   │
│   ├── Models/
│   │   ├── Exercise.cs
│   │   ├── Workout.cs
│   │   └── User.cs
│   │
│   ├── Services/
│   │   ├── ExerciseService.cs
│   │   ├── WorkoutService.cs
│   │   ├── UserService.cs
│   │   └── TokenService.cs
│   │
│   └── Program.cs
│
└── Gym.API.Tests/
```

The project separates HTTP endpoint handling from application and database logic by using a **service layer**.

---

## API Endpoints

### Workouts

| Method | Endpoint                   | Description                          |
| ------ | -------------------------- | ------------------------------------ |
| GET    | `/workouts`                | Get all workouts                     |
| GET    | `/workouts/{id}`           | Get a workout and its exercises      |
| GET    | `/workouts/{id}/exercises` | Get exercises belonging to a workout |
| POST   | `/workouts`                | Create a workout                     |
| PUT    | `/workouts/{id}`           | Update a workout                     |
| DELETE | `/workouts/{id}`           | Delete a workout                     |

### Exercises

| Method | Endpoint          | Description           |
| ------ | ----------------- | --------------------- |
| GET    | `/exercises`      | Get all exercises     |
| GET    | `/exercises/{id}` | Get an exercise by ID |
| POST   | `/exercises`      | Create an exercise    |
| PUT    | `/exercises/{id}` | Update an exercise    |
| DELETE | `/exercises/{id}` | Delete an exercise    |

### Authentication

| Method | Endpoint         | Description                           |
| ------ | ---------------- | ------------------------------------- |
| POST   | `/auth/register` | Register a new user                   |
| POST   | `/auth/login`    | Authenticate a user and receive a JWT |

---

## Authentication

The API implements JWT-based authentication.

### Registration Flow

```text
Register Request
      ↓
RegisterDto Validation
      ↓
Check if Email Exists
      ↓
Hash Password
      ↓
Create User
      ↓
Save with Entity Framework Core
      ↓
201 Created
```

Passwords are never stored as plain text. The application stores only the password hash.

### Login Flow

```text
Email + Password
      ↓
Find User
      ↓
Verify Password Against Stored Hash
      ↓
Generate JWT
      ↓
Return Token
```

The client can then send the token with requests to protected endpoints:

```http
Authorization: Bearer <token>
```

ASP.NET Core validates the token before allowing access to endpoints that require authentication.

---

## Application Flow

One of the main concepts I learned while building this project was how a request travels through an ASP.NET Core application.

```text
HTTP Request
      ↓
ASP.NET Core Endpoint Routing
      ↓
DTO Binding & Validation
      ↓
Endpoint
      ↓
Service Layer
      ↓
Entity Framework Core / DbContext
      ↓
SQLite Database
      ↓
Service Result
      ↓
Endpoint
      ↓
HTTP Response
```

This helped me understand the responsibility of each layer instead of putting all application logic directly inside API endpoints.

---

## Entity Framework Core

The project uses **Entity Framework Core** as the ORM and **SQLite** as the database.

The main database context is `GymContext`.

EF Core is used for:

- Querying workouts and exercises
- Creating records
- Updating records
- Deleting records
- Managing relationships
- Asynchronous database operations
- Database migrations

Some of the LINQ and EF Core methods practiced in this project include:

```csharp
AnyAsync()
FirstOrDefaultAsync()
FindAsync()
Where()
Include()
SaveChangesAsync()
```

---

## Workout and Exercise Relationship

A workout can contain multiple exercises.

For example:

```text
Workout
│
├── Exercise
├── Exercise
└── Exercise
```

This project helped me understand how relationships are represented in C# models and how Entity Framework Core loads related data.

For example, `Include()` can be used when related entities need to be loaded with the main entity.

---

## DTOs

The API uses DTOs (Data Transfer Objects) instead of exposing database entities directly for every request and response.

DTOs help control:

- What information clients can send
- What information the API returns
- Validation rules
- Separation between the API contract and database models

For example:

```text
HTTP Request
     ↓
CreateExerciseDto
     ↓
Service
     ↓
Exercise Entity
     ↓
Database
```

Response DTOs are also used to return only the information required by the client.

---

## Dependency Injection

Services and database dependencies are registered with ASP.NET Core's built-in dependency injection container.

For example:

```csharp
builder.Services.AddScoped<ExerciseService>();
builder.Services.AddScoped<WorkoutService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TokenService>();
```

ASP.NET Core then provides these dependencies to endpoints when they are needed.

This project helped me understand that the endpoint does not need to manually create its service or database context.

---

## Async / Await

Database operations use asynchronous methods such as:

```csharp
await dbContext.SaveChangesAsync();
await dbContext.Exercises.ToListAsync();
await dbContext.Workouts.FirstOrDefaultAsync(...);
```

I learned how `async` and `await` allow the application to wait for I/O operations such as database queries without blocking the request thread unnecessarily.

---

## Configuration

Database and JWT settings are read through ASP.NET Core configuration rather than being hardcoded into application logic.

Example:

```json
"ConnectionStrings": {
  "GymDatabase": "Data Source=gym.db"
}
```

The project also uses development-specific configuration through:

```text
appsettings.Development.json
```

JWT secrets are not stored in the public source code. For local development, they can be stored using **.NET User Secrets**.

---

## Error Handling

The application uses global exception handling so unexpected server errors can be handled consistently rather than exposing internal implementation details.

The API also returns appropriate HTTP status codes depending on the result of a request.

Examples include:

```text
200 OK
201 Created
204 No Content
400 Bad Request
401 Unauthorized
404 Not Found
409 Conflict
500 Internal Server Error
```

---

## API Documentation

The project uses **OpenAPI** to describe the API and **Scalar** to provide an interactive interface for exploring and testing endpoints.

This makes it possible to inspect available routes, request models, response types, and execute requests while developing the API.

---

## Testing

The solution contains a separate test project:

```text
Gym.API.Tests
```

I practiced both **unit testing** and **integration testing** with xUnit.

### Unit Tests

Unit tests were used to test service behavior independently.

I practiced the:

```text
Arrange
Act
Assert
```

testing pattern.

### Integration Tests

Integration tests were used to send HTTP requests against the application and verify API behavior.

The test environment uses an in-memory database so tests do not depend on the normal development SQLite database.

---

# What I Learned

This project was my first deeper ASP.NET Core backend project where I focused on understanding **why the application is structured this way**, rather than only making the endpoints work.

### ASP.NET Core

I learned how `Program.cs` acts as the startup point of the application, where services, configuration, middleware, authentication, and endpoints are connected.

I also became more comfortable with Minimal API concepts such as:

```csharp
MapGet()
MapPost()
MapPut()
MapDelete()
MapGroup()
```

### Separation of Responsibilities

One of my biggest takeaways was understanding that different parts of the application should have different responsibilities.

```text
Endpoint
→ HTTP responsibility

DTO
→ Data transfer and validation

Service
→ Application/business logic

DbContext
→ Database access

Model
→ Database/domain representation
```

Earlier, it was tempting to put database logic directly inside endpoints. Building the service layer helped me understand why separating these responsibilities makes an application easier to read, maintain, and test.

### Entity Framework Core

I learned how C# objects are connected to database records through EF Core and how `DbContext` tracks changes before `SaveChangesAsync()` writes them to the database.

I also learned the purpose of migrations:

```text
Change C# Model
      ↓
Create Migration
      ↓
EF Core Generates Database Changes
      ↓
Update Database
```

### Authentication vs Authorization

I learned the difference between:

```text
Authentication
→ Who are you?

Authorization
→ What are you allowed to access?
```

I implemented registration, password hashing, login, JWT generation, token validation, and a protected endpoint.

I also learned the practical meaning of common authentication responses:

```text
401 Unauthorized
→ Authentication is missing or invalid

403 Forbidden
→ User is authenticated but does not have permission
```

### Dependency Injection

Before this project, dependency injection felt abstract.

Building the API helped me understand the practical idea:

```text
Register dependency once
        ↓
ASP.NET Core manages it
        ↓
Request it where needed
```

Instead of manually creating services and their dependencies throughout the application.

### Testing

I learned that testing does not only mean manually calling endpoints.

Unit tests can verify individual pieces of application logic, while integration tests can verify how multiple parts of the application work together through real HTTP requests.

### Most Important Lesson

The biggest improvement for me was learning to mentally trace what happens **behind the screen** when an API request arrives.

Instead of seeing:

```text
GET /exercises
```

as one operation, I now think about:

```text
Request arrives
→ ASP.NET Core matches the route
→ Input is bound and validated
→ Endpoint receives the request
→ Dependency Injection provides the service
→ Service executes application logic
→ EF Core queries the database
→ Result returns to the endpoint
→ Endpoint creates the HTTP response
→ Client receives the response
```

Understanding this flow made ASP.NET Core much less abstract and gave me a stronger foundation for building larger backend applications.

---

## Running the Project Locally

### 1. Clone the repository

```bash
git clone <repository-url>
cd Gym/Gym.API
```

### 2. Configure the JWT development secret

Initialize User Secrets if necessary:

```bash
dotnet user-secrets init
```

Set a JWT key:

```bash
dotnet user-secrets set "Jwt:Key" "your-development-secret-key"
```

### 3. Restore dependencies

```bash
dotnet restore
```

### 4. Apply database migrations

```bash
dotnet ef database update
```

### 5. Run the API

```bash
dotnet run
```

The terminal will display the local URL where the application is running.

The Scalar API interface can then be used to explore and test the API.

---

## Running Tests

From the repository root:

```bash
dotnet test ./Gym.API.Tests/Gym.API.Tests.csproj
```

---

## Future Improvements

This project is intentionally focused on building a strong ASP.NET Core foundation.

Possible future improvements include:

- Role-based authorization
- Refresh tokens
- Additional integration tests
- More advanced validation
- Database-level email uniqueness
- Cloud deployment
- CI/CD pipeline
- Additional API documentation

---

## About This Project

This project was built as a hands-on learning project while developing my C# and ASP.NET Core backend skills.

Rather than continuing to add features indefinitely, I am using the concepts learned here as a foundation for my next .NET projects, where I plan to apply the same architecture and concepts more independently.
