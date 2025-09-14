# Bus Ticket Booking System

## Project Overview
The **Bus Ticket Booking System** is a web-based application developed using **ASP.NET MVC** and **Entity Framework Core**.  
It allows passengers to book bus tickets online and admins to manage buses, seats, and bookings efficiently.  

---

## Features

### Admin Module
- Add, update, and delete buses.
- Define total seats for each bus.
- View passenger list for each bus.
- Monitor bus schedules.
- Logout functionality.

### Passenger Module
- User registration and login.
- Search available buses by date.
- View available seats and book tickets.
- Check booking details.
- Logout functionality.

---

## Technologies Used
- **Frontend:** HTML, CSS, Bootstrap, Razor Views  
- **Backend:** C#, ASP.NET MVC, Entity Framework Core  
- **Database:** SQL Server  
- **IDE:** Visual Studio 2022  
- **NuGet Packages:** EF Core, Identity Management  

---

## Technical Concepts
- **MVC Architecture:** Separation of Model, View, Controller.  
- **Repository Pattern & Unit of Work:** Clean database operations.  
- **LINQ & Async/Await:** Efficient, non-blocking data queries.  
- **Delegates:** Modular actions like notifications.  
- **Session & Authorization:** Role-based access and user session management.  
- **Data Annotations & Validation:** `[Required]`, `[Range]`, `[EmailAddress]`, `[StringLength]`.  

---

## Database Design
- **Users Table:** Stores Admin and Passenger info.  
- **Buses Table:** Bus info, travel date, available seats.  
- **Bookings Table:** Maps passengers to buses and seat numbers.  
- Relationships maintained through foreign keys ensuring normalized database design.  

---

## Future Enhancements
- RESTful API for mobile app integration.  
- Online payment integration.  
- Real-time seat availability using SignalR.  
- Upgrade to ASP.NET Core MVC for better performance and cross-platform support.

---
- ## How to Run the Project

Follow these steps to run the Bus Ticket Booking System locally:

1. **Open the solution**  
   - Open Visual Studio 2022.  
   - Click **File → Open → Project/Solution** and select the `.sln` file.  

2. **Restore NuGet Packages**  
   - Right-click the solution in Solution Explorer → **Restore NuGet Packages**.  
   - Or, use the Package Manager Console:  
     ```powershell
     Update-Package -reinstall
     ```

3. **Update Connection String**  
   - Open `appsettings.json` (or `Web.config` if using older MVC).  
   - Update the **SQL Server connection string** to match your local database:  
     ```json
     "ConnectionStrings": {
         "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=BusBookingDB;Trusted_Connection=True;"
     }
     ```

4. **Run EF Core Migrations to Create Database**  
   - Open **Package Manager Console**:  
     ```
     Tools → NuGet Package Manager → Package Manager Console
     ```  
   - Run the following commands:  
     ```powershell
     Add-Migration InitialCreate
     Update-Database
     ```
   - This will create all tables in your SQL Server database according to your EF Core models.

5. **Run the Application**  
   - Press **F5** or click **IIS Express / Run**.  
   - The project will open in your default browser.  
   - Login as **Admin** or **Passenger** to test the functionality.  

---


