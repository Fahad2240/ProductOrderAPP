# Product Order Management System

A .NET MVC project designed to manage product orders with features like stock management, customer insights, and transactional operations.

## Table of Contents
1. [Overview](#overview)
2. [Features](#features)
3. [APIs](#apis)
4. [Technologies Used](#technologies-used)
5. [Installation](#installation)
6. [Usage](#usage)
7. [Project Structure](#project-structure)
8. [License](#license)

---

## Overview

This system is an ASP.NET MVC application that allows users to manage products, orders, and customers efficiently. It features CRUD operations, stock validation, and transactional integrity.

---

## Features

- Place, update, and delete orders with automatic stock adjustments.
- Retrieve order and product details.
- Generate summaries of product sales and stock status.
- Identify top customers and products without orders.
- Perform bulk operations with rollback support.

---

## APIs

### API 01: Create a New Order
- **Description**: Places an order for an existing product after checking stock availability. Deducts the stock upon success.
- **Example**: Place an order for "Tool C" by "Alex Brown" with a quantity of 25.

### API 02: Update an Order
- **Description**: Updates the quantity of an existing order while ensuring stock sufficiency. Adjusts the stock accordingly.
- **Example**: Update order ID 2 to a quantity of 15.

### API 03: Delete an Order
- **Description**: Deletes an order and restores the stock.

### API 04: Retrieve All Orders
- **Description**: Fetches all orders along with product details (e.g., name, unit price).

### API 05: Product Summary
- **Description**: Provides total quantity ordered and total revenue for each product.

### API 06: Low Stock Products
- **Description**: Lists products with stock below a specified threshold.

### API 07: Top 3 Customers
- **Description**: Identifies the top 3 customers based on total quantity ordered.

### API 08: Unordered Products
- **Description**: Finds products that have not been ordered.

### API 09: Bulk Order Transaction
- **Description**: Performs bulk order creation with rollback in case of insufficient stock for any item.

---

## Technologies Used

- **Backend Framework**: ASP.NET MVC
- **Language**: C#
- **Database**: Entity Framework with migrations
- **Frontend**: Razor Views, HTML5, CSS3, Bootstrap
- **Other**: LINQ, Dependency Injection

---

## Installation

### Prerequisites

1. Visual Studio (latest version recommended)
2. SQL Server (or any compatible database)
3. .NET SDK

### Steps

1. Clone the repository:
   ```bash
   git clone https://github.com/your-repository-link.git
