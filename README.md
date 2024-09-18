# todo-api
Sure! Here's the updated guide for the **TodoApi** application:

---

# **Todo API - ASP.NET Core Application**

This project is a simple RESTful API built using **ASP.NET Core**, **Entity Framework Core**, and an **in-memory database** for demonstration purposes. The API allows you to manage a list of tasks (To-Dos), including actions like retrieving, adding, updating, and deleting tasks.

## **Table of Contents**
- [Features](#features)
- [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Installation](#installation)
    - [Running the Application](#running-the-application)
- [Using the API](#using-the-api)
    - [API Endpoints](#api-endpoints)
    - [Sample Requests](#sample-requests)
- [Running Unit Tests](#running-unit-tests)
- [License](#license)

---

## **Features**
- Get a list of all To-Do items.
- Retrieve a specific To-Do item by its ID.
- Create a new To-Do item.
- Update an existing To-Do item.
- Delete a To-Do item.

---

## **Getting Started**

### **Prerequisites**
To run this application, you need to have the following installed:
- [.NET 7 SDK or later](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Git](https://git-scm.com/)

### **Installation**
Follow these steps to get the application running locally:

1. Clone the repository:
   ```bash
   git clone https://github.com/your-repo/TodoApi.git
   cd todo-api
   ```

2. Restore the required packages:
   ```bash
   dotnet restore
   ```

---

## **Running the Application**

### **1. Using In-Memory Database**
This API is configured to use an in-memory database, which doesn't require setting up a real database. It's useful for testing and development.

1. Build and run the application:
   ```bash
   dotnet run
   ```

   The server will start on `http://localhost:5000` (or `https://localhost:7000` for HTTPS).

2. Once the server is running, you can interact with the API using tools like [Postman](https://www.postman.com/) or `curl` in the terminal.

---

## **Using the API**

### **API Endpoints**
The API exposes the following endpoints:

| Method   | Endpoint         | Description                        |
|----------|------------------|------------------------------------|
| `GET`    | `/api/todo`      | Retrieves a list of all to-do items|
| `GET`    | `/api/todo/{id}` | Retrieves a specific to-do by ID   |
| `POST`   | `/api/todo`      | Creates a new to-do item           |
| `PUT`    | `/api/todo/{id}` | Updates an existing to-do item     |
| `DELETE` | `/api/todo/{id}` | Deletes a to-do item               |

### **Sample Requests**

#### 1. **Get All To-Dos**
```bash
curl -X GET "https://localhost:7000/api/todo"
```

#### 2. **Get To-Do by ID**
```bash
curl -X GET "https://localhost:7000/api/todo/1"
```

#### 3. **Create a New To-Do**
```bash
curl -X POST "https://localhost:7000/api/todo" -H "Content-Type: application/json" -d '{
  "title": "New To-Do",
  "description": "New Text",
  "isComplete": false
}'
```

#### 4. **Update a To-Do**
```bash
curl -X PUT "https://localhost:7000/api/todo/1" -H "Content-Type: application/json" -d '{
  "id": 1,
  "title": "Updated To-Do",
  "description": "Updated Text",
  "isComplete": true
}'
```

#### 5. **Delete a To-Do**
```bash
curl -X DELETE "https://localhost:7000/api/todo/1"
```

---

## **Running Unit Tests**

This project includes unit tests using **xUnit** to validate the functionality of the `TodoController`. An in-memory database is used during testing, so no external database is required.

To run the tests, follow these steps:

1. Navigate to the `Tests` project folder (if not already in the root of the solution):
   ```bash
   cd TodoApi.Tests
   ```

2. Run the tests using the .NET CLI:
   ```bash
   dotnet test
   ```

   You should see output indicating whether the tests passed or failed.

---

## **License**

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---
