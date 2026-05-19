# Unit Conversion API

## Overview
ASP.NET Core Web API for converting units across multiple categories:
- Length
- Weight
- Temperature

Designed using:
- Strategy Pattern
- Dependency Injection
- Clean Architecture principles

---

## Tech Stack
- ASP.NET Core 8
- Swagger
- Dependency Injection
- Middleware
- REST API

---

## Run Locally

```bash
dotnet restore
dotnet build
dotnet run
```

Swagger:
```text
https://localhost:xxxx/swagger
```

---

## Example Request

POST `/api/v1/conversion`

```json
{
  "category": "length",
  "fromUnit": "meter",
  "toUnit": "feet",
  "value": 10
}
```

---

## Design Decisions

### Strategy Pattern
Each conversion category has isolated logic.

### Base Unit Conversion
Length and weight conversions use base units for scalability.

### Dependency Injection
Converters are injected using interfaces for extensibility.

---

## Future Enhancements
- Database-driven units
- Caching
- Docker support
- Authentication
- CI/CD pipeline