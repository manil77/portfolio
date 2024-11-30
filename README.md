# Project Structure

## 1. Core
- **Purpose**: Encapsulates the business logic and domain entities.
- **Contents**: Domain entities, value objects, domain services, and interfaces.
- **Dependencies**: No dependencies on other projects.

## 2. Application
- **Purpose**: Manages application logic, including use cases and interfaces.
- **Contents**: Use cases, application services, DTOs, and interface definitions.
- **Dependencies**: Depends on the **Core** project. Interfaces defined here may be implemented in **Infrastructure**.

## 3. Infrastructure
- **Purpose**: Implements infrastructure concerns like data access and external service integrations.
- **Contents**: Dapper repositories, external API clients, database migrations, and configurations.
- **Dependencies**: Depends on **Core** and **Application** projects, implementing interfaces defined in **Application**.

## 4. Application (Web Project or MVC Project)
- **Purpose**: Manages the presentation layer for a web application, including controllers, views, and static content.
- **Contents**: MVC controllers, Razor views, and static files (CSS, JavaScript).
- **Dependencies**: Depends on **Application** and potentially **Infrastructure** for services and repositories.

## 5. WebApi
- **Purpose**: Serves as the entry point for the application, exposing its functionality via a RESTful API.
- **Contents**: API controllers, endpoints, and Swagger configuration.
- **Dependencies**: Depends on **Application** and potentially **Infrastructure**.
