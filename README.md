# WebAPI.Example

A clean, modern ASP.NET Core 8.0 Web API example project demonstrating REST API best practices with comprehensive test coverage.

## Overview

This project showcases a simple yet well-structured Web API with three endpoints demonstrating different HTTP methods and use cases. It serves as a template for building scalable ASP.NET Core APIs with proper testing practices.

## Features

- **ASP.NET Core 8.0** - Latest .NET framework
- **Minimal APIs** - Modern, lightweight endpoint configuration
- **Swagger/OpenAPI** - Integrated API documentation
- **Comprehensive Testing** - XUnit test suite with integration tests
- **RESTful Endpoints** - Best practices for HTTP methods and status codes
- **Input Validation** - Proper request validation and error handling

## Project Structure

```
WebAPI.Example/
├── Program.cs                      # Main application entry point
├── WeatherForecast.cs             # Data model
├── appsettings.json               # Configuration settings
├── appsettings.Development.json   # Development configuration
├── WebAPI.Example.csproj          # Project file
├── Properties/
│   └── launchSettings.json        # Launch profile settings
└── bin/, obj/                     # Build artifacts

WebAPI.Example.Tests/
├── WeatherForecastTests.cs        # Weather forecast endpoint tests
├── SumTests.cs                    # Sum endpoint tests
├── GreetTests.cs                  # Greet endpoint tests
└── WebAPI.Example.Tests.csproj    # Test project file
```

## Endpoints

### 1. GET /weatherforecast

Returns a 5-day weather forecast with temperatures in Celsius and Fahrenheit.

**Response Example:**
```json
[
  {
    "date": "2026-02-02",
    "temperatureC": 15,
    "temperatureF": 59,
    "summary": "Mild"
  },
  {
    "date": "2026-02-03",
    "temperatureC": -5,
    "temperatureF": 23,
    "summary": "Freezing"
  }
]
```

**Status Codes:**
- `200 OK` - Successful retrieval

---

### 2. GET /api/sum

Calculates the sum of two integers.

**Parameters:**
- `a` (int, query) - First number
- `b` (int, query) - Second number

**Example Request:**
```
GET /api/sum?a=5&b=3
```

**Response Example:**
```json
{
  "a": 5,
  "b": 3,
  "sum": 8
}
```

**Status Codes:**
- `200 OK` - Successful calculation
- `400 Bad Request` - Missing or invalid parameters

---

### 3. POST /api/greet

Generates a personalized greeting message.

**Request Body:**
```json
{
  "name": "John"
}
```

**Response Example:**
```json
{
  "message": "Hello, John!"
}
```

**Status Codes:**
- `200 OK` - Successful greeting
- `400 Bad Request` - Name is empty, whitespace, or missing

---

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- Visual Studio 2022, Visual Studio Code, or Rider (optional)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/WebAPI.Example.git
   cd WebAPI.Example
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the solution:
   ```bash
   dotnet build
   ```

### Running the API

Start the development server:

```bash
dotnet run --project WebAPI.Example
```

The API will be available at:
- **HTTP:** `http://localhost:5000`
- **HTTPS:** `https://localhost:5001`
- **Swagger UI:** `https://localhost:5001/swagger/index.html`

### Running Tests

Execute the test suite:

```bash
dotnet test
```

Run tests with coverage reporting:

```bash
dotnet test /p:CollectCoverage=true
```

Run specific test class:

```bash
dotnet test --filter "ClassName=WebAPI.Example.Tests.SumTests"
```

## Test Coverage

The project includes **21 comprehensive test cases** across three test classes:

### WeatherForecastTests (6 tests)
- Response status validation
- JSON structure validation
- Array length verification (5 forecasts)
- Temperature range validation (-20°C to 55°C)
- Fahrenheit calculation accuracy
- Future date validation
- Content-Type header validation

### SumTests (7 tests)
- Successful calculation with valid numbers
- Multiple calculation scenarios (positive, negative, zero, large numbers)
- Response structure validation (a, b, sum properties)
- Content-Type header validation
- Missing parameter handling
- Non-numeric parameter error handling

### GreetTests (8 tests)
- Successful greeting with valid names
- Multiple name variations (short, long, special characters)
- Empty name rejection
- Whitespace-only name rejection
- Missing name parameter rejection
- Error message validation
- Response structure validation
- Content-Type header validation

## API Documentation

Interactive API documentation is available via Swagger UI when running in development mode:

- Navigate to `https://localhost:5001/swagger/index.html`
- Test endpoints directly from the browser
- View request/response schemas

## Configuration

### Development Settings

Configure development-specific settings in `appsettings.Development.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### Launch Profiles

Launch profiles are defined in `Properties/launchSettings.json`:

- **https** - HTTPS development server
- **http** - HTTP development server
- **IIS Express** - IIS Express server (Windows)

## Building for Production

Build the release version:

```bash
dotnet build -c Release
```

Publish for deployment:

```bash
dotnet publish -c Release -o ./publish
```

## Technologies & Dependencies

### Main Project
- **ASP.NET Core 8.0** - Web framework
- **Microsoft.AspNetCore.OpenApi** - OpenAPI specification support
- **Swashbuckle.AspNetCore** - Swagger/OpenAPI tooling

### Test Project
- **xUnit** - Testing framework
- **Microsoft.NET.Test.Sdk** - Test SDK
- **Microsoft.AspNetCore.Mvc.Testing** - Integration testing utilities

## Code Structure

### Models

**WeatherForecast.cs** - Weather forecast data model
```csharp
public class WeatherForecast
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public string? Summary { get; set; }
}
```

### Request/Response Records

- `GreetRequest` - Contains a name string
- `GreetResponse` - Contains a greeting message
- `SumResponse` - Contains operands and result

## Best Practices Implemented

✅ **RESTful Design** - Appropriate HTTP methods and status codes  
✅ **Input Validation** - Request data validation with error responses  
✅ **OpenAPI Documentation** - Swagger integration for API documentation  
✅ **Integration Testing** - WebApplicationFactory for realistic test scenarios  
✅ **Theory-Based Testing** - Parametrized tests for multiple scenarios  
✅ **Minimal APIs** - Modern, lightweight endpoint configuration  
✅ **Nullable Reference Types** - Strict null checking enabled  
✅ **Implicit Usings** - Cleaner code with C# 10 implicit global usings  

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For issues, questions, or suggestions:
- Open an issue on GitHub
- Check existing documentation in Swagger UI
- Review test cases for usage examples

## Changelog

### Version 1.0.0 (2026-02-01)
- Initial project setup
- Weather forecast endpoint
- Sum calculation endpoint
- Greeting endpoint
- Comprehensive test suite (21 test cases)
- Swagger API documentation

---

**Happy coding!** 🚀
