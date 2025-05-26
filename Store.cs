using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Store_simulator
{
    class Store
    {
        public List<Product> Products { get; private set; }
        public List<Customer> Customers { get; private set; }
        public List<Order> Orders { get; private set; }

        public Store()
        {
            Products = new List<Product>();
            Customers = new List<Customer>();
            Orders = new List<Order>();
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public void MakeOrder(Customer customer, List<Product> products)
        {

            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            if (products == null || products.Count == 0)
            {
                Console.WriteLine("No products specified for the order.");
                return;
            }

            foreach(var product in  products)
            {
                var storeProduct = GetProductByName(product.Name);
                if (storeProduct == null)
                {
                    Console.WriteLine($"Product {product.Name} not found in store.");
                    return;
                }

                if (storeProduct.Quantity < product.Quantity)
                {
                    Console.WriteLine($"Not enough quantity for {product.Name}. Available: {storeProduct.Quantity}, Requested: {product.Quantity}");
                    return;
                }
            }

            decimal totalBalance = products.Sum(p => p.Price * p.Quantity);
            if (customer.Balance < totalBalance)
            {
                Console.WriteLine("Customer does not have enough balance.");
                return;
            }


            // minus balance from customer
            customer.Balance -= totalBalance;

            // clone products before the order
            List<Product> orderProducts = products.Select(p => p.Clone()).ToList();

            Order order = new Order(products, customer);
                Orders.Add(order);
                customer.AddOrder(order);

        }
        public Product GetProductByName(string name)
        {
            var product = Products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (product != null)
                return product;

            else
            {
                Console.WriteLine("Product not found or unavailable.");
                return null;
            }
        }
        public Customer GetCustomerById(Guid id)
        {
            return Customers.FirstOrDefault(c => c.Id == id);
        }

        public Product GetProductById(Guid id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }



        public void ListAllProducts()
        {
            if (Products.Count == 0)
            {
                Console.WriteLine("No products available.");
                return;
            }

            Console.WriteLine("Available products:");

            foreach(var product in Products)
            {
                Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}, Category: {product.Category}"
                    );
            }
        }

        public void ListAllCustomers()
        { 
            if(Customers.Count == 0)
            {
                Console.WriteLine("No customers available.");
                return;
            }

            foreach (var customer in Customers)
            {
                Console.WriteLine($"Id: {customer.Id}, Name: {customer.Name}, Balance: {customer.Balance}");
            }

            Console.WriteLine("Available customers");
        }

        public void ListAllOrders()
        {
            if(Orders.Count == 0)
            {
                Console.WriteLine("No orders available.");
                return;
            }

            Console.WriteLine("All Orders:");
            foreach (var order in Orders)
            {
                Console.WriteLine($"Customer: {order.Customer.Name}");
                Console.WriteLine("Products in order:");
                foreach(var product in order.Products)
                {
                    Console.WriteLine($" - {product.Name} ({product.Category}): {product.Price:C} x {product.Quantity}");
                }

                Console.WriteLine($"Total: {order.TotalAmount:C}");
                Console.WriteLine(new string ('-', 40));

            }
        }

        public void InitializeTestData(Store store)
        {
            var c1 = new Customer("Alice", 1000);  // Id генерується автоматично
            var c2 = new Customer("David", 80);
            var c3 = new Customer("Alex", 570);

            store.AddCustomer(c1);
            store.AddCustomer(c2);
            store.AddCustomer(c3);
        }


}
}

