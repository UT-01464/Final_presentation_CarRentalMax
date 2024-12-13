# Code Standards for Car Rental Project

This document establishes the coding standards for the Car Rental Project to maintain consistency, readability, and code quality across the development team.

## Naming Conventions

### 1.1 General Naming
- **PascalCase**: Used for Class, Method, Property, and Enum names.
  - Examples: `Car`, `CarService`, `CarRentalDetails`
- **camelCase**: Used for local variables, parameters, and private fields.
  - Examples: `RentDate`, `carId`
- **Constants**: Use all uppercase letters with underscores to separate words.
  

### 1.2 File Naming
Each Class or Interface should have its own file, named after the class. File names are written in PascalCase, matching the class name, e.g., `CarCategory.cs`.

## Code Structure and Formatting

### 2.1 Indentation and Spacing
- **Indentation**: Use 4 spaces per indentation level.
- **Braces**: Place the opening brace `{` on a new line.
- **Spacing**: Insert a blank line between methods and after class properties to enhance readability.

## Error Handling and Logging

### 3.1 Exceptions
Use exceptions for catching errors and ensure proper logging of those exceptions.

## Documentation Standards

### 4.1 Inline Comments
Use inline comments sparingly to clarify complex logic. Avoid redundant comments; the code itself should be self-explanatory when possible.

## Source Control and Branching

### 6.1 Branch Naming Convention
Follow a consistent naming pattern: `Main}`.

### 6.2 Commit Messages
Write clear, concise commit messages in the imperative form (e.g., “Add Car API”). Include a brief description of the change and, if applicable, any relevant issue numbers.

By adhering to these standards, we ensure that the codebase remains organized, efficient, and accessible to all members of the development team.