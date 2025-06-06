using Store_simulator.Core_Logic.Models;
using Store_simulator.Data;
using Store_simulator.DataService.Implementations;
using Store_simulator.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_simulator.Controllers
{
    public static class MenuController
    {
        public static void Start()
        {
            StorageManager.InitializeStorage();
            Store store = new Store();
            store.InitializeTestData(store);
            store.SaveAll();

            bool exit = false;

            while (!exit)
            {
                ConsoleUI.ShowTitle();
                ConsoleUI.ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        string productName = ConsoleUI.ReadValidatedProductName("Enter a Product Name");

                        decimal price;
                        while (!decimal.TryParse(ConsoleUI.ReadInput("Enter product price (>= 0):"), out price) || price < 0) ;

                        int quantity;
                        while (!int.TryParse(ConsoleUI.ReadInput("Enter product quantity (>= 0):"), out quantity) || quantity < 0) ;

                        string category = ConsoleUI.ReadInput("Enter product category:");

                        Product newProduct = new Product(productName, price, quantity, category);
                        store.AddProduct(newProduct);
                        store.SaveAll();

                        ConsoleUI.ShowMessage("Product added successfully!");
                        ConsoleUI.Wait();
                        break;

                    case "2":
                        store.ListAllCustomers();
                        string inputId = ConsoleUI.ReadInput("Enter Customer ID:");

                        if (!Guid.TryParse(inputId, out Guid customerId))
                        {
                            ConsoleUI.ShowMessage("Invalid ID format.");
                            ConsoleUI.Wait();
                            break;
                        }

                        Customer customer = store.GetCustomerById(customerId);
                        if (customer == null)
                        {
                            ConsoleUI.ShowMessage("Customer not found.");
                            ConsoleUI.Wait();
                            break;
                        }

                        List<OrderItem> selectedOrderItems = new();

                        while (true)
                        {
                            store.ListAllProducts();
                            productName = ConsoleUI.ReadInput("Enter product name to add (or type 'done' to finish):");

                            if (productName.ToLower() == "done") break;

                            Product product = store.GetProductByName(productName);
                            if (product == null)
                            {
                                ConsoleUI.ShowMessage("Product not found.");
                                continue;
                            }

                            if (!int.TryParse(ConsoleUI.ReadInput($"Enter quantity for {product.Name} (Available: {product.Quantity}):"), out quantity) || quantity <= 0)
                            {
                                ConsoleUI.ShowMessage("Invalid quantity.");
                                continue;
                            }

                            if (quantity > product.Quantity)
                            {
                                ConsoleUI.ShowMessage("Not enough quantity in stock.");
                                continue;
                            }

                            product.UpdateQuantity(-quantity);
                            selectedOrderItems.Add(new OrderItem(product, quantity));
                            ConsoleUI.ShowMessage($"{quantity} x {product.Name} added to cart. Remaining: {product.Quantity}");
                        }

                        if (selectedOrderItems.Count > 0)
                        {
                            store.MakeOrder(customer, selectedOrderItems);
                            store.SaveAll();
                        }
                        else
                        {
                            ConsoleUI.ShowMessage("No products selected.");
                        }

                        ConsoleUI.Wait();
                        break;

                    case "3":
                        store.ListAllProducts();
                        ConsoleUI.Wait();
                        break;

                    case "4":
                        store.ListAllOrders();
                        ConsoleUI.Wait();
                        break;

                    case "5":
                        store.ListAllOrders();
                        if (int.TryParse(ConsoleUI.ReadInput("Enter index of order to cancel:"), out int indexToCancel) &&
                            indexToCancel >= 0 && indexToCancel < store.Orders.Count)
                        {
                            store.CancelOrder(store.Orders[indexToCancel]);
                            store.SaveAll();
                        }
                        else
                        {
                            ConsoleUI.ShowMessage("Invalid index.");
                        }
                        ConsoleUI.Wait();
                        break;

                    case "6":
                        exit = true;
                        break;

                    default:
                        ConsoleUI.ShowMessage("Invalid choice, please try again.");
                        ConsoleUI.Wait();
                        break;
                }
            }
        }
    }
}

