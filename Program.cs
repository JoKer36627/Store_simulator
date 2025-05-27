using System.Diagnostics.Metrics;

namespace Store_simulator
{
    internal class Program
    {
        static void Main(string[] args)
        {

            StorageManager.InitializeStorage();
            Store store = new Store();

            store.InitializeTestData(store);
            store.SaveAll();



            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Store Simulator!");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Create Order");
                Console.WriteLine("3. Show All Products");
                Console.WriteLine("4. Show All Orders");
                Console.WriteLine("5. Cancel Order");
                Console.WriteLine("6. Exit");

                string choice = Console.ReadLine();

                string productName;
                int quantity;
                

                switch (choice)
                {
                    case "1":
                        // Logic to add a new product
                        Console.WriteLine("Enter a Product Name");
                        productName = Console.ReadLine();

                        decimal price;

                        do
                        {
                            Console.WriteLine("Enter product price (>= 0): ");
                        }
                        while (!decimal.TryParse(Console.ReadLine(), out price) || price < 0);

                        do
                        {
                            Console.WriteLine("Enter product quantity (>= 0): ");
                        }
                        while (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0);

                        Console.Write("Enter product category: ");
                        string category = Console.ReadLine();

                        Product newProduct = new Product(productName, price, quantity, category);
                        store.AddProduct(newProduct);
                        store.SaveAll();

                        Console.WriteLine("Product added successfully!");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();

                        break;

                    case "2":
                        store.ListAllCustomers();

                        Console.WriteLine("Enter Customer ID:");
                        string inputId = Console.ReadLine();

                        if (!Guid.TryParse(inputId, out Guid customerId))
                        {
                            Console.WriteLine("Invalid ID format.");
                            Console.ReadKey();
                            break;
                        }

                        Customer customer = store.GetCustomerById(customerId);

                        if (customer == null)
                        {
                            Console.WriteLine("Customer not found.");
                            Console.ReadKey();
                            break;
                        }

                        List<Product> selectedProducts = new List<Product>();

                        while (true)
                        {
                            store.ListAllProducts();
                            Console.WriteLine("Enter product name to add (or type 'done' to finish):");
                            productName = Console.ReadLine();

                            if (productName.ToLower() == "done") break;

                            Product product = store.GetProductByName(productName);
                            if (product == null)
                            {
                                Console.WriteLine("Product not found.");
                                continue;
                            }

                            Console.WriteLine($"Enter quantity for {product.Name} (Available: {product.Quantity}):");
                            if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                            {
                                Console.WriteLine("Invalid quantity.");
                                continue;
                            }

                            if (quantity > product.Quantity)
                            {
                                Console.WriteLine("Not enough quantity in stock.");
                                continue;
                            }

                            product.UpdateQuantity(-quantity); // Update the product quantity in the store

                            Product orderedProduct = product.Clone();
                            orderedProduct.Quantity = quantity;
                            selectedProducts.Add(orderedProduct);

                            Console.WriteLine($"{quantity} x {product.Name} added to cart. Remaining: {product.Quantity}");

                            Console.WriteLine($"{product.Name} x {quantity} added to order.");
                        }

                        if (selectedProducts.Count > 0)
                        {
                            store.MakeOrder(customer, selectedProducts);
                            store.SaveAll();
                        }
                        else
                        {
                            Console.WriteLine("No products selected.");
                        }

                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();

                        break;

                    case "3":
                        // Logic to show all products
                        store.ListAllProducts();


                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();

                        break;

                    case "4":
                        store.ListAllOrders();

                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();

                        // Logic to show all orders
                        break;

                    case "5":
                        // Logic to cancel an order
                        store.ListAllOrders();

                        Console.WriteLine("Enter index of order to cancel:");

                        if (int.TryParse(Console.ReadLine(), out int indexToCancel) &&
                            indexToCancel >= 0 && indexToCancel < store.Orders.Count)
                        {
                            var orderToCancel = store.Orders[indexToCancel];
                            store.CancelOrder(orderToCancel);
                            store.SaveAll();

                            Console.WriteLine("Order cancelled successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid index.");
                        }

                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "6":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }
    }
}
