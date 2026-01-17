```md
# 🍔 Foodie – Simple Food Ordering App

Foodie is a simple food ordering system built with **.NET 10**, **ASP.NET Web API**, and **Blazor WebAssembly**.  
Users can browse available menu items, place food orders, and view order status.

This project does **not include online payments** and is intended for learning and demonstration purposes.

---

## 🚀 Features

### Backend
- CRUD operations for menu items
- Create food orders
- View all orders
- Order status tracking:
  - Placed
  - Preparing
  - Delivered

### Frontend
- Menu listing page
- Order creation form
- Orders list with status

---

## 🧱 Tech Stack

- **Backend:** ASP.NET Core Web API (.NET 10)
- **Frontend:** Blazor WebAssembly
- **Database:** Any supported DB (SQL Server / SQLite / In-Memory)
- **Communication:** REST APIs (JSON)

---

## 📁 Project Structure

All projects are located in the **same solution folder** (no `src` directory).

```

Foodie.sln
│
├── Foodie.Api        → Backend Web API
├── Foodie.Core       → Shared DTOs, models, enums, services
├── Foodie.Web        → Blazor WebAssembly frontend
│
└── README.md

````

## 🌐 Application URLs

| Application | Port |
|-------------|------|
| Frontend (Blazor WASM) | `http://localhost:3000` |
| Backend API | `http://localhost:5000` |

---

## ▶️ How to Run the Project

### ✅ Prerequisites

- .NET 10 SDK installed  
  Check using:

```bash
dotnet --version
````

---

### ▶️ Run Backend API

From the solution root folder:

```bash
dotnet run --project Foodie.Api
```

Backend will start at:

```
http://localhost:5000
```

---

### ▶️ Run Frontend (Blazor WebAssembly)

Open a new terminal:

```bash
dotnet run --project Foodie.Web
```

Frontend will start at:

```
http://localhost:3000
```

---

## 🔁 Communication Flow

```
Blazor WebAssembly (3000)
        |
        | HTTP / JSON
        v
ASP.NET Web API (5000)
        |
        v
Database
```

---

## 🧩 Shared Core Project

`Foodie.Core` contains:

* DTOs
* Request / Response models
* Enums
* Shared constants
* Interfaces
* Common validation logic

Both **API** and **Web** projects reference this project.

---

## 📌 Example Order Flow

1. User opens the menu page
2. Available food items are loaded from the API
3. User selects items and enters customer name
4. Order is placed
5. Order status defaults to **Placed**
6. Admin or system can update status to:

   * Preparing
   * Delivered

---

## 🎯 Purpose

This project is ideal for:

* Learning ASP.NET Web API
* Understanding Blazor WebAssembly
* Practicing clean solution structure
* Sharing DTOs between frontend and backend
* Beginner-friendly full-stack development

---

## 📄 License

This project is for educational purposes only.
