# TaskManagement API

A RESTful Task Management API built with **ASP.NET Core 10** following **Clean Architecture** and **CQRS** principles.

## Architecture

```
TaskManagement/
├── src/
│   ├── TaskManagement.Domain/          # Entities, enums, repository interfaces
│   ├── TaskManagement.Application/     # CQRS commands/queries, DTOs, interfaces
│   ├── TaskManagement.Infrastructure/  # EF Core, database, repository implementations
│   └── TaskManagement.API/             # ASP.NET Core minimal API, endpoints
└── tests/
    ├── TaskManagement.Domain.Tests/
    └── TaskManagement.Application.Tests/
```

### Layer Responsibilities

| Layer | Responsibility |
|---|---|
| **Domain** | Core business entities (`TaskItem`), enums (`Priority`, `TaskItemStatus`), repository contracts |
| **Application** | CQRS with MediatR, use case handlers, DTOs, `Result<T>` wrapper |
| **Infrastructure** | EF Core `DbContext`, repository implementations, migrations |
| **API** | HTTP endpoints, dependency injection wiring |

## Tech Stack

- **.NET 10** / **ASP.NET Core 10**
- **MediatR 14** — CQRS mediator pattern
- **FluentValidation 12** — request validation
- **Entity Framework Core 10** — ORM
- **xUnit** — unit testing

## Domain Model

**TaskItem** — the core aggregate root

| Property | Type |
|---|---|
| `Id` | `Guid` |
| `Title` | `string` |
| `Description` | `string` |
| `Status` | `TaskItemStatus` (Todo, InProgress, Done, Cancelled) |
| `Priority` | `Priority` (Low, Medium, High) |
| `DueDate` | `DateTime?` |
| `CreatedAt` | `DateTime` |
| `UpdatedAt` | `DateTime` |

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Run the API

```bash
cd src/TaskManagement.API
dotnet run
```

### Run Tests

```bash
dotnet test
```

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/tasks` | Get all tasks |
| `GET` | `/api/tasks/{id}` | Get task by ID |
| `POST` | `/api/tasks` | Create a new task |
| `PUT` | `/api/tasks/{id}` | Update a task |
| `DELETE` | `/api/tasks/{id}` | Delete a task |
