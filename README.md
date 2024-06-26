# Benefit Card Payment Processor

## Overview

The Benefit Card Payment Processor is a transaction processing system designed to facilitate payments using benefit cards. It provides functionalities for tracking information about both successful and unsuccessful transactions, ensuring efficient handling of transaction requests.

## Technologies

- **ASP.NET Core 8**: Main framework for building web applications in the .NET ecosystem.
- **Entity Framework Core**: ORM for database access.
- **SQLite**: Lightweight and fast SQL database used for development environment.

## Project Structure

- **API**: Contains the ASP.NET Core web API project.
  - **Controllers**: Contains ASP.NET Core web API controllers serving as the entry point into the application.
  - **Data**: Contains the DbContext and migrations, as well as initial data stored in the database.
  - **Entities**: Defines the entity classes used in the application.
  - **DTOs**: Defines Data Transfer Objects.
  - **Middleware**: Custom middleware for exception handling.
  - **Services**: Contains the business logic of the application, data access service and service interfaces.
  - **PaymentProcessor.db**: SQLite database file.

- **API.Tests**: Contains unit tests for the API project.

## Testing

- **XUnit**: Testing framework used for unit testing.
- **FakeItEasy**: Mocking framework used for creating fake objects for testing.
- **FluentAssertions**: Fluent API for asserting the results of unit tests, providing more readable and expressive test code.

### Swagger

Swagger is a tool that allows for documenting and testing APIs. The API has Swagger integration for testing the API's functionality. Swagger will be available at the `/swagger` endpoint when the application is running in the Development environment.

## Getting Started

1. Clone this repository to your local machine.
2. Open Visual Studio or Visual Studio Code.
3. Open the Solution file `BenefitCardPaymentProcessor.sln`.
4. Build and run the application.

## Docker Support

The project also supports Docker containers for easier management and distribution. To run the application using Docker, follow these steps:

### Building Docker Image

Run the following command in your terminal within the root directory of the project to build the Docker image:

```bash
docker build -t payment-processor -f API/Dockerfile .
```

### Creating Docker Container

Once the image is built, run the Docker container using the following command:

```bash
docker run --name payment-processor -e ASPNETCORE_ENVIRONMENT=Development -p 8080:8080 -p 8081:8081 payment-processor
```
**Note:** Setting `ASPNETCORE_ENVIRONMENT=Development` is there becouse of Swagger.
