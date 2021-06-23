# Overview
To implement this API, I have used .NET 5.0, ASP.NET Core and CQRS principles to store database entries within an in-memory Entity Framework Core context. A Swagger interface has been provided to ease testing and document the functionality of each endpoint.

The unit tests make use of FixItEasy to fake components and ensure that none of the automated tests become integration tests.

# Features
* IoC and DI: Used to simplify testing and integrate with the IoC container provided by ASP.NET Core.
* Logging via Serilog
* A Swagger interface for testing and documentation purposes
* Use of EF Core to present a realistic interface to a DAL. A custom seeder is provided to read from the JSON database file.

# Design Decisions
* CQRS was chosen to make it clear which operations change application state versus operations that return existing state. This also helps to keep the controllers as lean as possible.
* Serilog was chosen as the logging tool since it features more functionality than the default .NET logging tools and allows for common logging integrations and structured logging.
* FluentValidation was chosen to increase testability of validations and centralize common logging code.
* Tests make use of FluentAssertions in order to provide a more BDD-like experience. FakeItEasy is used because of its use of fluent interfaces.

# Instructions
When the project is run from IIS Express, you should be directed to the Swagger UI, from which you will see endpoint documentation, schemas, and an interface easily allowing you to test endpoint functionality.