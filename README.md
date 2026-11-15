# AetherFlow — Travel & Tourism Administration Portal

A comprehensive, full-stack travel booking and tourism management web platform built using C#, ASP.NET MVC, and Entity Framework.

---

##  Project Overview
AetherFlow is engineered to streamline tour browsing, booking workflows, and administrative management. It provides a seamless experience for customers exploring travel packages while giving administrators full control over catalog data, user reservations, and reporting.

##  Technological Stack & Architecture
- **Backend:** C#, ASP.NET MVC (.NET Framework)
- **Database & ORM:** SQL Server (LocalDB / MDF), Entity Framework
- **Authentication:** ASP.NET Identity (Role-Based: Admin / User)
- **Frontend:** Razor Views, HTML5, CSS3, JavaScript, Bootstrap
- **Tools:** Visual Studio, Git & GitHub

---

##  Key Functional Modules
- **Tour & Destination Catalog:** Dynamic browsing of destinations, tour packages, durations, and pricing.
- **Booking & Reservation Engine:** Step-by-step reservation system tracking traveler details, booking dates, and confirmation statuses.
- **Role-Based Authentication:** Distinct permissions for standard customers and administrators via ASP.NET Identity.
- **Admin Management Panel:** Comprehensive CRUD capabilities for administrators to manage tours, destinations, booking requests, and administrative logs.
- **Relational Integrity:** Structured entity-relationship design connecting users, destinations, tours, and bookings with foreign-key constraints.

## Local Installation & Setup

**Clone the repository:**
```bash
git clone https://github.com/ozlemnurbora/AetherFlow.git

1. Open the solution:
Open Visual Studio.
Select Open a project or solution and choose the AetherFlow.sln file from the repository folder.

2. Restore NuGet Packages:
Visual Studio usually restores missing packages automatically. If not, right-click the solution in the Solution Explorer and select Restore NuGet Packages.

3. Configure the Database:
Open the Package Manager Console via Tools > NuGet Package Manager > Package Manager Console.
Run the migration command to set up the database:
Update-Database

4. Run the Application:
Press F5 or click the IIS Express button to launch the web application.
   

