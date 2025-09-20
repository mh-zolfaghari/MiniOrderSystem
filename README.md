# MiniOrderSystem

## ⚠️ Important Notice
**This project is only a sample implementation for modeling and demonstrating modern architecture concepts in .NET (DDD, CQRS, Clean Architecture).  
It is NOT a real production-ready service and should not be used as such.**

---

## Introduction
MiniOrderSystem is a sample project for managing customers and orders, built with modern architecture in .NET.  
The project is designed as a practical example to understand concepts such as **Domain-Driven Design (DDD)**, **CQRS**, and **Clean Architecture**.

![dotnet-version](https://img.shields.io/badge/dotnet%20version-net8.0-blue)
---

## Project Architecture
The project follows **Clean Architecture** and **DDD principles**, organized into the following layers:

- **Domain Layer**: Contains Entities, Value Objects, and core domain logic.
- **Application Layer**: Contains UseCases (Commands, Queries, Services), business rules, and contracts.
- **Infrastructure Layer**: Provides database access (EF Core, Migrations) and integration with external services.
- **Presentation Layer (API/UI)**: Provides RESTful APIs for external clients.
- **Shared Layer**: Shared Utilities.

---

## Technologies Used
- **.NET 8**
- **Entity Framework Core** for ORM and database management
- **MS SQL Server** for storing data
- **FluentValidation** for input validation
- **MediatR** for CQRS and request handling
- **Swagger / Swashbuckle** for API documentation
- **xUnit / Moq** for unit testing
- **Serilog** for logging

---

## Use Cases
Some of the main **UseCases** implemented in the system include:

- **Customer Management**
  - Create a new customer
  - Retrieve customer details by ID
  - Get a list of all customers

- **Order Management**
  - Create a new order
  - Update order details
  - Retrieve order information by ID
  - Get order for a specific user by OrderNumber

- **Product Management**
  - Create a new product
  - Retrieve product information by ID
  - Get a list of all products

Each UseCase is implemented in the **Application Layer** using the **CQRS pattern** with MediatR.

---

## Repository Pattern
The system follows the **Repository Pattern** for persistence.  
Repositories abstract the data access logic and provide a clean API for the domain layer.

Implemented Repositories include:
- `IClientRepository`
- `ICustomerRepository`
- `IProductRepository`
- `IOrderRepository`

These interfaces are implemented in the **Infrastructure Layer** using EF Core.

---

## API Endpoints
The **Presentation Layer** exposes RESTful APIs. Some key endpoints are:

### Customer Endpoints
- `POST /api/customers` → Create a new customer
- `GET /api/customers/{id}` → Get customer by ID
- `GET /api/customers` → List all customers

### Product Endpoints
- `POST /api/products` → Create a new product
- `GET /api/products/{id}` → Get product by ID
- `GET /api/products` → List all products

### Order Endpoints
- `POST /api/orders` → Create a new order
- `POST /api/orders/items` → Upsert order items and update order details
- `DELETE /api/orders/items/{product_id}` → Delete order item from order and update order details
- `GET /api/orders/{order_number}` → Get order by Order Number
- `PUT /api/orders/change-status` → Change order status

Swagger is available for easy API testing and documentation.

---

## Database Connection & Migration

### Connection String
The application uses **SQL Server** with different configurations for **Development** and **Production** environments.  
In both environments, the `appsettings.json` (or `appsettings.Development.json`) contains:

```json
"ConnectionStrings": {
  "ApplicationDbContext": "Server=SERVER_ADDRESS;Database=MiniOrderSystemDb;User=USER_NAME;Password=PASSWORD;TrustServerCertificate=True;"
}
```

Make sure to configure the connection string according to your environment.

### Database Setup
To set up the database:

1. Open **Package Manager Console** in Visual Studio.  
2. Set **Default Project** to `MiniOrderSystem.Infrastructure`.  
3. Run the following command:

```powershell
Update-Database
```

This will apply all migrations and create the database.

### Seed Data
After running the application, initial **SeedData** will be created for the `Customer` and `Product` tables.  
This provides default test data for evaluation and demonstration purposes.

---

## Getting Started

### Prerequisites
- Install [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Install [Git](https://git-scm.com/)
- SQL Server installed and running

### Clone & Run
```bash
git clone https://github.com/mh-zolfaghari/MiniOrderSystem.git
cd MiniOrderSystem
dotnet ef database update
dotnet run --project src/MiniOrderSystem.Api
```

---

## Folder Structure
```
MiniOrderSystem/
│
├── src/
│   ├── MiniOrderSystem.Domain/        # Domain Layer (Entity Models, ValueObjects, Domain Repository Interfaces)
│   ├── MiniOrderSystem.Application/   # Application Layer (MediatR[Behaviors], DTOs, Model Helpers, CQRS, UseCases)
│   ├── MiniOrderSystem.Infrastructure/# Infrastructure Layer (Implementation of Repositories, EF Core MSSQL Server Provider, Persistency, Entity Model Configurations)
│   ├── MiniOrderSystem.Api/           # Presentation Layer (Web API, GlobalExceptionHandling, Swagger Document Viewer)
|   └── MiniOrderSystem.Shared/        # Shared Layer (AppSettings, GlobalExtensions, Result Response Pattern)
│
├── tests/
│   └── MiniOrderSystem.Tests/         # Unit Tests
│
└── README.md
```

---

## Features
- Customer and profile management
- Order creation and management
- Repository pattern for persistence
- Well-documented REST API with Swagger
- Unit tests for critical parts of the system
- Seed data for initial testing

---

## License
This project is distributed under the MIT License. See the `LICENSE` file for details.

---

## 🩷 Follow Me!

[![LinkedIn][linkedin-shield]][linkedin-url]  [![Telegram][telegram-shield]][telegram-url]  [![WhatsApp][whatsapp-shield]][whatsapp-url]  [![Gmail][gmail-shield]][gmail-url]  ![GitHub followers](https://img.shields.io/github/followers/mh-zolfaghari)

[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?logo=linkedin&color=555
[linkedin-url]: https://www.linkedin.com/in/ronixa/

[telegram-shield]: https://img.shields.io/badge/-Telegram-black.svg?logo=telegram&color=fff
[telegram-url]: https://t.me/DanialDotNet

[whatsapp-shield]: https://img.shields.io/badge/-WhatsApp-black.svg?logo=whatsapp&color=fff
[whatsapp-url]: https://wa.me/989389043224

[gmail-shield]: https://img.shields.io/badge/-Gmail-black.svg?logo=gmail&color=fff
[gmail-url]: mailto:personal.mhz@gmail.com
﻿