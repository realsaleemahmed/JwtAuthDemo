ASP.NET Core JWT Authentication Demo

A lightweight, straightforward Web API demonstrating how to implement JSON Web Tokens (JWT) in ASP.NET Core.

I put this together as a quick reference template for setting up secure login and protecting API endpoints without the bloat of a massive boilerplate.

What's Inside

Framework: .NET 8/9/10 (ASP.NET Core Web API)

Auth: JWT Bearer Authentication (Microsoft.IdentityModel.Tokens)

Storage: In-memory user store (to keep the demo self-contained and easy to run)

Token generation, validation middleware, and [Authorize] attributes in action.

Project Structure

JwtDemo/
├── Controllers/
│   └── UserController.cs
├── Services/
│   └── TokenService.cs
├── Program.cs
├── appsettings.json
└── JwtDemo.csproj

Quick Start

Configure your settings: Check the appsettings.json file. You can leave the defaults for local testing, but if you change the secret, make sure it's at least 32 characters long or the HS256 algorithm will throw an exception.

{
  "JwtSettings": {
    "Secret": "this_is_a_super_secure_32_byte_secret_key!!",
    "Issuer": "JwtDemoApi",
    "Audience": "JwtDemoApi",
    "ExpirationInMinutes": 60
  }
}


Run the API:

dotnet restore
dotnet run


The API will spin up, usually on http://localhost:5221.

Endpoints

You can test these using Postman, cURL, or the built-in Swagger UI.

1. Get a Token

POST /User/login

Send your credentials:

{
  "userName": "testuser",
  "password": "testpassword"
}


Response:

{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}


2. Hit a Protected Endpoint

GET /User/secure-data

Pass the token you just generated in the headers:

Authorization: Bearer <paste_your_token_here>


Response:

{
  "message": "This is a protected endpoint!"
}


Taking this to Production?

This is a working concept, but it's not production-ready out of the box. Before deploying something like this, make sure to:

Ditch the in-memory users: Hook this up to a real database using EF Core or Dapper.

Hash passwords: Never store plain text. Use BCrypt or ASP.NET Core Identity.

Hide your secrets: Do not commit your real JWT secret to source control. Use Environment Variables, Azure Key Vault, or standard .NET Secret Manager for local dev.

Author: Saleem Ahmed
Backend Developer
