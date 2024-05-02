# Benefit Card Payment Processor

## Overview

This project represents an implementation of a transaction processing system and tracking of successful and unsuccessful transactions.

## Technologies

- **ASP.NET Core 8**: Main framework for building web applications in the .NET ecosystem.
- **Entity Framework Core**: ORM for database access.
- **SQLite**: Lightweight and fast SQL database used for development environment.

## Project Structure
- **Controllers**: Contains ASP.NET Core web API controllers serving as the entry point into the application.
- **Data**: Contains the DbContext and migrations, as well as initial data stored in the database.
- **Entities**: Defines the entity classes used in the application.
- **DTOs**: Defines Data Transfer Objects.
- **Middleware**: Custom middleware for exception handling.
- **Services**: Contains the business logic of the application, data access service and service interfaces.

## Getting Started

1. Clone this repository to your local machine.
2. Open Visual Studio or Visual Studio Code.
3. Open the Solution file `BenefitCardPaymentProcessor.sln`.
4. Build and run the application.

## Swagger

Swagger is a tool that allows for documenting and testing APIs. The API has Swagger integration for testing the API's functionality. Swagger will be available at the `/swagger` endpoint when the application is running in the Development environment.
