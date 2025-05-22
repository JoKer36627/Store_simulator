namespace Store_simulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Store store = new Store();

            // Додавання тестових даних
            store.AddProduct(new Product("Apple", 1.5m, 20, "Fruits"));
            store.AddProduct(new Product("Milk", 3.2m, 10, "Dairy"));
            store.AddCustomer(new Customer("John", 50));

            while (true)
            {
                Console.WriteLine("\n1. List products\n2. List customers\n3. Make order\n4. Exit");
                Console.Write("Choose action: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        store.ListAllProducts();
                        break;
                    case "2":
                        store.ListAllCustomers();
                        break;
                    case "3":
                        Console.Write("Enter customer name: ");
                        string name = Console.ReadLine();
                        var customer = store.Customers.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                        if (customer == null)
                        {
                            Console.WriteLine("Customer not found.");
                            break;
                        }

                        Console.Write("Enter product name: ");
                        string productName = Console.ReadLine();
                        Console.Write("Enter quantity: ");
                        int qty = int.Parse(Console.ReadLine());

                        var product = store.GetProductByName(productName);
                        if (product == null)
                            break;

                        var productList = new List<Product>
                {
                    new Product(product.Name, product.Price, qty, product.Category)
                };

                        store.MakeOrder(customer, productList);
                        break;
                    case "4":
                        return;
                }
            }
        }
    }
}
