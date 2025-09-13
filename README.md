# 🎬 Movie API

A RESTful ASP.NET Core Web API for managing movies, genres, and authentication. Built with Entity Framework Core and secured using JWT tokens.

## ✨ Features

🔑 JWT Authentication (Register & Login)

🎥 Movies Management (CRUD + bulk add, update)

🎭 Genres Management (CRUD)

📖 Swagger UI for testing endpoints

🗄️ Entity Framework Core with MySQL

## 📂 Project Structure

Controllers/ → API controllers (Auth, Movies, Genre, etc.)
Data/ → EF Core DbContext
DTOs/ → Data Transfer Objects for clean responses
Models/ → Entity models (User, Movies, Genre, Actor, etc.)
Services/ → Business logic (AuthService, JWT handling)
Migrations/ → EF Core migrations

🚀 Getting Started

1. Clone the repository
   git clone https://github.com/<your-username>/<repo-name>.git
   cd <repo-name>

2. Configure database

Update appsettings.json with your own values:

"ConnectionStrings": {
"DefaultConnection": "Server=localhost;Database=movies;user=root;password=YOUR_PASSWORD"
},
"JWTSettings": {
"SecretKey": "<your-secret-key>",
"Issuer": "MyIssuer",
"Audience": "MyAudience",
"ExpiryMinutes": 60
}

3. Run migrations
   dotnet ef database update

4. Run the application
   dotnet run

Navigate to Swagger UI:
👉 http://localhost:5164/swagger

## 📌 API Endpoints

Auth

POST /api/Auth/register → Register new user

POST /api/Auth/login → Login and receive JWT

Genre

GET /api/Genre → Get all genres

POST /api/Genre → Add genre

PUT /api/Genre/{id} → Update genre

DELETE /api/Genre/{id} → Delete genre

Movies

GET /api/Movies → Get all movies

GET /api/Movies/{id} → Get movie by id

POST /api/Movies → Add movie

POST /api/Movies/addmultimovies → Bulk add

PUT /api/Movies/updatemovie/{id} → Update movie

## 🔒 Authentication

Most endpoints require JWT authentication.
Add token in header:

Authorization: Bearer <your_token>

## 🛠️ Tech Stack

ASP.NET Core 8

Entity Framework Core

MySQL

Swagger / Swashbuckle

JWT Authentication

- 📸 Example Swagger UI:  
  ![Swagger Screenshot](docs/swagger.png)
