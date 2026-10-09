# Pizzeria Toscana

Pizzeria Toscana is a full-stack web application developed using **ASP.NET Core MVC**, designed to provide an online pizza ordering experience and an administration system for managing products, ingredients, and sales.

The project combines traditional e-commerce functionalities with advanced search techniques, including **TF-IDF** for relevance-based product searching and **Apache Lucene.NET** for full-text searching within PDF product specifications.

## Features

- **User Authentication & Authorization** – User registration, login, profile management, and role-based access control using ASP.NET Core Identity.
- **Product Catalog** – Browse pizzas, filter products by category, and view product details.
- **Intelligent Search** – TF-IDF-based product search with relevance ranking and real-time autocomplete suggestions.
- **PDF Specification Search** – Generate product specification PDFs and search their indexed content using Apache Lucene.NET.
- **Shopping Cart** – Add and remove products, update quantities, and automatically calculate the total price.
- **Order Management** – Place orders and view order history.
- **Admin Panel** – Manage products, categories, images, and ingredients, including product-ingredient associations and quantities.
- **Sales Reports** – View product sales statistics, including quantities sold and sales values calculated using current product prices.

## Tech Stack

| Category | Technologies |
|---|---|
| Backend | C#, ASP.NET Core MVC |
| Frontend | HTML, CSS, JavaScript, Razor Views, Bootstrap |
| Database & ORM | Entity Framework Core |
| Authentication | ASP.NET Core Identity |
| Search | TF-IDF, Apache Lucene.NET |
| PDF Processing | iText |
| Architecture | MVC, Repository Pattern, Service Layer, Dependency Injection |

## Architecture

The application follows a layered architecture to separate responsibilities and improve maintainability.

- **Controllers** – Handle HTTP requests and user interactions.
- **Services** – Implement business logic and application functionality.
- **Repositories** – Abstract database operations using a generic repository and repository wrapper.
- **Models** – Define application entities and their relationships through Entity Framework Core.

## Technical Highlights

### TF-IDF Search Engine

A custom TF-IDF implementation calculates relevance scores for product names, enabling ranked search results and dynamic search suggestions.

### Lucene.NET & PDF Indexing

Product specifications are dynamically generated as PDF documents using iText. Their textual content is extracted, indexed, and searched using Apache Lucene.NET.

### Role-Based Access Control

ASP.NET Core Identity manages authentication and distinguishes between regular users and administrators, restricting access to administrative features.

### Database Design

A relational data model built with Entity Framework Core, managing users, products, categories, ingredients, shopping carts, and orders through interconnected entities.

## Screenshots

Screenshots showcasing the application's main features and interface can be found [here](./Pizzeria_Toscana/wwwroot/screenshots/).
