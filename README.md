**Store Simulator**

Overview
Store Simulator is a console-based C# application designed to manage products, customers, and orders in a simple retail environment. The project covers core programming concepts such as OOP, inheritance, interfaces, serialization, and user input validation.

Features
-- Product management with validation (name, price, quantity, category)
-- Customer handling
-- Order processing with total calculation
-- Data persistence using JSON serialization
-- Robust input validation to prevent invalid data
-- Use of inheritance, interfaces, and abstract classes
-- Exception handling and user interaction via console


Technologies
-- C# (.NET 8.0)
-- JSON serialization (System.Text.Json)
-- Console UI

Structure
Models: Product, Customer, Order classes
Interfaces: IStorageManager for data operations
Implementations: StorageManager handles JSON file read/write
UI Layer: ConsoleUI for input/output management
Business Logic: Store class manages core app functionality

How to Run
1. Clone the repository
2. Open the solution in Visual Studio
3. Build and run the project in Debug mode
4. Follow on-screen instructions to add products, customers, and create orders

Future Improvements
-- Implement GUI for better user experience
-- Add database integration
-- Extend order management features

Author
Andrii Derkach
