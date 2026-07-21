# URL Shortener

A URL shortener built with ASP.NET Core MVC. The application creates short links, redirects users to the original URLs, tracks clicks, and displays URL analytics.

## Features

- Generate a short URL from a valid HTTP or HTTPS URL
- Redirect short URLs to their original destinations
- Track click counts
- View analytics for shortened URLs
- Responsive Razor and Bootstrap interface
- API endpoints for testing

## Built With

- .NET 9
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server LocalDB
- Razor views
- Bootstrap

## Getting Started

### Requirements

- Visual Studio 2022
- .NET 9 SDK
- SQL Server Express LocalDB

### Setup

1. Clone the repository:

```bash
git clone https://github.com/melissaesromero-dev/urlShortener_YGS.git
```

2. Open `URLShortener.sln` in Visual Studio.

3. Set `URLShortener.Web` as the startup project.

4. Open the Package Manager Console:

```text
Tools → NuGet Package Manager → Package Manager Console
```

5. Select `URLShortener.Web` as the default project and create the database:

```powershell
Update-Database
```

6. Run the application using HTTPS.

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| POST | `/shorten` | Creates a shortened URL |
| GET | `/{shortUrl}` | Redirects to the original URL |
| GET | `/analytics/{shortUrl}` | Returns analytics for a shortened URL |

### Example Request

```json
{
  "originalUrl": "https://www.example.com"
}
```

## Author

Melissa Romero
