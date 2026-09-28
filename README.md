# MorningCoffee — ASP.NET MVC 5 Coffee Shop

Complete sample coffee-shop application using **ASP.NET MVC 5 + C# + Entity Framework 6 + SQL Server LocalDB**.

## Database
Server:
`(localdb)\MSSQLLocalDB`

Database:
`MorningCoffeeDb`

Connection string is already configured in `Web.config`.

## Requirements
- Visual Studio 2022
- .NET Framework 4.8 Developer Pack
- SQL Server LocalDB / `(localdb)\MSSQLLocalDB`
- ASP.NET MVC 5 / Entity Framework 6 NuGet packages

## Database setup
Open SQL Server Object Explorer or SSMS and execute these scripts in order:

1. `Database/01_CreateDatabase.sql`
2. `Database/02_Tables.sql`
3. `Database/03_StoredProcedures.sql`
4. `Database/04_SeedData.sql`

## Demo accounts
Admin:
- Email: `admin@morningcoffee.com`
- Password: `Admin@123`

Customer:
- Email: `customer@morningcoffee.com`
- Password: `Customer@123`

## Important
The project uses SHA-256 for a simple educational authentication example. For a production application, use ASP.NET Identity with a modern password hasher rather than storing application passwords this way.

## Run
1. Open `MorningCoffee.sln` in Visual Studio.
2. Restore NuGet packages.
3. Ensure LocalDB is installed.
4. Run the database scripts.
5. Build and press F5.
6. Open `/Menu`.
7. Login as the admin to access `/Admin/Dashboard`.

## Features
- Home/About/Contact
- Product/menu listing
- Search and category filtering
- Product details
- Shopping cart
- Quantity update/remove
- Customer registration/login/logout
- Checkout
- Orders and order history
- Admin dashboard
- Product CRUD/disable
- Category CRUD
- Order status management
- User listing
- Stored procedures
- SQL indexes and foreign keys
- Bootstrap responsive UI
