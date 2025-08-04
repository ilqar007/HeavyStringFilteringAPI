"# HeavyStringFilteringAPI" 


- How to run the application

Open visual studio 2022 -> f5-> http://localhost:5170/swagger


 - How to test the application
 
 in tests folder there are 2 test classes, right click in visual studio and Run tests
 -- filtered texts are saved  into memory in QueuedHostedService.cs class  filteredTexts List
 
 - Your architecture and filtering logic
 
 I chose the Onion architecture
 
Onion Architecture in C# is a software architectural pattern that emphasizes the separation of concerns and dependency inversion, aiming to create highly testable, maintainable, and loosely coupled applications. It places the core business logic (Domain Layer) at the center, isolated from external concerns like databases, UI, or frameworks.

Key Layers in C# Onion Architecture:

Domain Layer (Core):

Contains the core business entities and rules.
Pure C# logic, free from any external dependencies (e.g., database frameworks, UI frameworks).
Defines interfaces for external services that the domain needs.

Application Layer:

Orchestrates business use cases and application-specific logic.
Defines application services, data transfer objects (DTOs), and interfaces for repositories.
Depends on the Domain Layer but not on the Infrastructure Layer.

Infrastructure Layer:

Implements the interfaces defined in the Domain and Application Layers.
Handles external concerns like database persistence (e.g., Entity Framework Core), external APIs, file system access, and other infrastructure details.
Depends on the Domain and Application Layers but the inner layers do not depend on it.

Presentation/UI Layer (e.g., Web API, Worker):

The entry point of the application (e.g., ASP.NET Core Controllers).
Handles user interactions and presents data.
Depends on the Application Layer to interact with business logic.


- Filtering Logic 

it is pure c# methods used for determining similarities and distance for a given word among the combined text. I found the implementation from internet not much sure about the functionality. Just tried to used 1 interface and 2 different implementation of it to demonstrate how to dynamically choose between the twos.