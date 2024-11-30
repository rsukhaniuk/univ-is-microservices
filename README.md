# SmartMenu


SmartMenu is an information system designed for managing restaurant online orders, viewing and editing dishes, handling order statuses, and providing customers with the ability to view dish details, as well as place orders.

With a focus on automation, efficiency, and security, SmartMenu integrates seamlessly with Stripe for safe online transactions, offering an enhanced experience for both restaurant managers and customers.

---

### 📖 **Documentation** ([link](https://rsukhaniuk.github.io/univ-is-microservices-docs/))

### 🎥 **Video Demonstration** ([link](https://drive.google.com/file/d/1EHZ9Ba1PW_LBhFm-rdxPx5ZV5UZCItG4/view?usp=sharing))

### 📄 **Report** ([link](https://docs.google.com/document/d/1XrM24vUF2psDKovD91E1ov__boC49Zn43PrIfosYt0U/edit?usp=sharing))

---

## 🛠️ Tech Stack

### **Backend**
- **Framework:** .NET Core Web API (C#) with a Microservices Architecture
- **Microservices:**
  Each microservice operates independently and has its own dedicated database to ensure a clear separation of concerns and improve scalability:

  - **Auth Service:** Manages authentication and authorization.
    - **Database Schema:** [link](https://dbdiagram.io/d/SmartMenu_Auth-670f628a97a66db9a325b7c6)
  - **Order Service:** Handles order processing and status updates.
    - **Database Schema:** [link](https://dbdiagram.io/d/SmartMenu_Orders-670f62e597a66db9a325c66e)
  - **Product Service:** Manages productі and categories.
    - **Database Schema:** [link](https://dbdiagram.io/d/SmartMenu_Products-673d29b6e9daa85aca094e8d)
  - **Shopping Cart Service:** Provides shopping cart functionality.
    - **Database Schema:** [link](https://dbdiagram.io/d/SmartMenu_ShoppingCart-670f63df97a66db9a325edf7)
  - **Coupon Service:** Handles discount and coupon logic.
    - **Database Schema:** [link](https://dbdiagram.io/d/SmartMenu_Coupons-674a9ca0e9daa85aca313fc4)
- **Database:** SQL Server 2022
- **Communication:** REST API

### **Frontend**
- **Framework:** ASP.NET Core MVC (C#)
- **Features:**
  - Razor views for dynamic and interactive HTML rendering.
  - Integration with REST API for backend communication.
  - Responsive design for desktop and mobile platforms.

### **Payments**
- **Stripe API** for secure payment processing.

---
