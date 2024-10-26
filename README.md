# ExpenseTrackerAPI

ExpenseTrackerAPI is a web API for tracking expenses. It is built using ASP.NET Core and Entity Framework Core, with support for JWT authentication.

## Features

- User authentication with JWT
- CRUD operations for expenses
- Auto-mapping with AutoMapper
- SQLite or PostgresSQL database integration
- Swagger/OpenAPI documentation

## Technologies Used

- ASP.NET Core 8.0
- Entity Framework Core 8.0
- AutoMapper 12.0.1
- SQLite
- Swagger (Swashbuckle.AspNetCore 6.4.0)

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- SQLite

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/ExpenseTrackerAPI.git
    cd ExpenseTrackerAPI
    ```

2. Install the required packages:
    ```sh
    dotnet restore
    ```

3. Update the database:
    ```sh
    dotnet ef database update
    ```

### Configuration

Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=expenseTracker.db"
  }
}
