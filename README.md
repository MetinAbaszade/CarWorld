# CarSelling Website

This repository contains the backend and frontend for the **CarSelling Website**, a web application for managing car listings and sales. The project is developed using **ASP.NET Core** for the backend and **React** for the frontend, utilizing **Entity Framework Core** with a Code-First approach.

## Technologies Used
- **ASP.NET Core**: Backend framework for building the API.
- **React**: Frontend UI library.
- **Entity Framework Core**: Code-First approach for managing database models and migrations.
- **C#**: Primary programming language.
- **JWT (JSON Web Tokens)**: Token-based authentication for secure access.
- **Mail Sending Authentication**: Sends authentication codes via email for user verification.

## Project Structure
- **ClientApp**: Contains the React application for the frontend.
- **Controllers**: Handles API requests and responses.
- **CustomExceptions**: Custom exception handling for the API.
- **Data**: Data access layer using Entity Framework Core.
- **Dto**: Data Transfer Objects used for communication between layers.
- **Entities**: Contains the entity models representing database tables.
- **Mappings**: Automapper configurations for mapping between entities and DTOs.
- **Services**: Business logic layer that connects the API with the database.
- **Validations**: Custom validations for ensuring data integrity.

## Features
- **Code-First Database Approach**: Database schema is generated from entity models using EF Core.
- **React Frontend**: A dynamic and responsive user interface built with React.
- **JWT Authentication**: Secure user authentication and authorization using JWT tokens.
- **Mail Sending Authentication**: Sends a verification code via email for account validation.
- **Custom Exception Handling**: Centralized exception management with detailed error logs.
- **Generic CRUD Operations**: Implementation of generic CRUD functionality for easy extension.
---

This project is a full-stack application for managing car listings and sales, developed from scratch with a focus on security, scalability, and user experience.
