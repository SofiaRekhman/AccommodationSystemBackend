# Accommodation System Backend

Modern .NET 8 backend structured around a clean 3-layer architecture: AccommodationSystemApi (Web API), BLL (business logic), and DAL (data access). Designed for modular development, PostgreSQL integration, and straightforward extension with new features.

---

## 🧱 Architecture

- AccommodationSystemApi  
  Minimal API host, dependency injection wiring, CORS and logging middleware, HTTP endpoints that delegate to BLL services.

- BLL (Business Logic Layer)  
  Domain services, DTOs, AutoMapper profiles, validation and orchestration of data-access operations.

- DAL (Data Access Layer)  
  Entity definitions, repositories, EF Core DbContext, Identity integration, PostgreSQL provider configuration.

---

## 📦 NuGet Packages

| Layer | Packages |
| --- | --- |
| Shared / API | Microsoft.AspNetCore.OpenApi 8.0.21 · Swashbuckle.AspNetCore 6.6.2 |
| BLL | AutoMapper 12.0.1 |
| DAL | Microsoft.EntityFrameworkCore 8.0.4 · Microsoft.EntityFrameworkCore.Design 8.0.4 · Microsoft.EntityFrameworkCore.Tools 8.0.4 · Microsoft.AspNetCore.Identity.EntityFrameworkCore 8.0.4 · Npgsql.EntityFrameworkCore.PostgreSQL 8.0.4 |

---

## 📁 Solution Layout

AccommodationSystemBackend.sln
├─ AccommodationSystemApi/          -> API host (presentation layer)
│  └─ Program.cs
├─ BLL/                             -> Business logic layer
│  ├─ Models/
│  └─ Services/
└─ DAL/                             -> Data access layer
   ├─ Entities/
   └─ Repositories/

---

## 🚀 Getting Started

1. Install prerequisites
   - .NET SDK 8.0
   - PostgreSQL server (local or remote)

2. Restore dependencies
      dotnet restore
   

3. Configure database
   - Update the connection string in AccommodationSystemApi/appsettings.json (or environment config)

4. Apply migrations *(placeholder – run once DbContext and migrations are defined)*
      dotnet ef migrations add InitialCreate -p DAL -s AccommodationSystemApi
   dotnet ef database update -p DAL -s AccommodationSystemApi
   

5. Run the API
      dotnet run --project AccommodationSystemApi
   

---
