# 🛒 Order Management System - ASP.NET Core Web API

This is a backend API system for managing orders, customers, products, and invoices, built with ASP.NET Core Web API and Entity Framework Core. The system supports role-based access control (Admin, Customer), JWT authentication, and includes business logic like tiered discounts, stock validation, and email notifications.

---

## 📦 Features

- 🔐 **JWT Authentication** with role-based access (RBAC)
- 🧾 **Order Management** with invoice generation
- 📉 **Tiered Discounts** (e.g., 5% for orders > $100, 10% for > $200)
- 🧮 **Stock Validation** before placing an order
- 💳 **Multiple Payment Methods** supported
- 📧 **Email Notifications** on status changes
- 🧪 **Unit Testing Support** (to be added)
- 🌐 **Swagger API Documentation**

---

## 🧱 Tech Stack

- **ASP.NET Core Web API**
- **Entity Framework Core** (In-Memory Database for simplicity)
- **JWT Authentication**
- **AutoMapper**
- **Swagger UI**
- **SMTP (Gmail)** for email notifications

---

## 🧑‍💻 Roles

- **Admin**  
  Can manage products, orders, invoices.

- **Customer**  
  Can register/login, place orders, and view their order history.

---

## 🔗 Endpoints Overview

### 🔐 User Endpoints

| Method | Route                 | Access   | Description               |
|--------|----------------------|----------|---------------------------|
| POST   | /api/users/register  | Public   | Register a new user       |
| POST   | /api/users/login     | Public   | Login and get JWT token   |

---

### 👤 Customer Endpoints

| Method | Route                              | Access     | Description                |
|--------|-------------------------------------|------------|----------------------------|
| POST   | /api/customers                      | Public     | Create a new customer      |
| GET    | /api/customers/{id}/orders          | Customer   | Get all orders for a customer |

---

### 📦 Order Endpoints

| Method | Route                             | Access     | Description                       |
|--------|-----------------------------------|------------|-----------------------------------|
| POST   | /api/orders                       | Customer   | Place a new order                 |
| GET    | /api/orders/{id}                  | Customer   | Get specific order details        |
| GET    | /api/orders                       | Admin      | Get all orders                    |
| PUT    | /api/orders/{id}/status           | Admin      | Update order status               |

---

### 🛍️ Product Endpoints

| Method | Route                             | Access     | Description                       |
|--------|-----------------------------------|------------|-----------------------------------|
| GET    | /api/products                     | Public     | Get all products                  |
| GET    | /api/products/{id}                | Public     | Get product by ID                 |
| POST   | /api/products                     | Admin      | Add new product                   |
| PUT    | /api/products/{id}                | Admin      | Update product details            |

---

### 🧾 Invoice Endpoints

| Method | Route                             | Access     | Description                       |
|--------|-----------------------------------|------------|-----------------------------------|
| GET    | /api/invoices                     | Admin      | Get all invoices                  |
| GET    | /api/invoices/{id}                | Admin      | Get invoice by ID                 |

---

## ⚙️ Configuration

### 🔐 JWT Settings (`appsettings.json`)

```json
"Jwt": {
  "Key": "your-secret-key",
  "Issuer": "your-app",
  "Audience": "your-app"
}

📧 Email Settings (Optional)
"Email": {
  "SmtpServer": "smtp.gmail.com",
  "Port": "587",
  "Username": "yourEmail@gmail.com",
  "Password": "yourAppPassword",
  "From": "yourEmail@gmail.com"
}

🚀 How to Run
1-Clone the repo
2-Run the project in Visual Studio or via CLI:
dotnet run

3- Navigate to Swagger:
https://localhost:{port}/swagger

📌 Notes
The system uses In-Memory DB for demo purposes.
Admin endpoints are protected and require JWT token with Admin role.
Orders auto-generate invoices.
Email notification on order status change requires proper config and internet access.


👨‍💻 Developed By
Mohamed Salah
