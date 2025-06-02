using Store_simulator.Core_Logic.Services;
using Store_simulator.Data;
using Store_simulator.DataService.Implementations;
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
        private StorageManager storageManager;
        public List<Product> Products { get; private set; }
        public List<Customer> Customers { get; private set; }
        public List<Order> Orders { get; private set; }
        private OrderService _orderService;
        private ProductService _productService;

        public Store()
        {
            storageManager = new StorageManager();

            Products = storageManager.LoadData<List<Product>>("products.json") ?? new List<Product>();
            Customers = storageManager.LoadData<List<Customer>>("customers.json") ?? new List<Customer>();
            Orders = storageManager.LoadData<List<Order>>("orders.json") ?? new List<Order>();
            _orderService = new OrderService();
            _productService = new ProductService();
        }

        public void SaveAll()
        {
            storageManager.SaveData("products.json", Products);
            storageManager.SaveData("customers.json", Customers);
            storageManager.SaveData("orders.json", Orders);
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

            Order order = new Order(orderProducts, customer.Id);
                Orders.Add(order);
                customer.AddOrder(order);

        }

        public void CancelOrder(Order order) 
        {
            if (order.Status == OrderStatus.Completed)
            {
                Console.WriteLine("Completed orders cannot be cancelled.");
                return;
            }

            if (order.Status == OrderStatus.Cancelled)
            {
                Console.WriteLine("Order is already cancelled.");
                return;
            }

            var customer = GetCustomerById(order.CustomerId);
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            // refund customer
            customer.Balance += order.TotalAmount;

            // return products to store
            foreach (var orderedProduct in order.Products)
            {
                var storeProduct = GetProductByName(orderedProduct.Name);
                if (storeProduct != null)
                {
                    storeProduct.Quantity += orderedProduct.Quantity;
                }
                else
                {
                    // Продукт міг бути видалений, але його треба повернути
                    Products.Add(orderedProduct.Clone());
                }
            }

            // Change status to Cancelled
            order.ChangeStatus(OrderStatus.Cancelled);
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
            if (Orders.Count == 0)
            {
                Console.WriteLine("No orders available.");
                return;
            }

            Console.WriteLine("All Orders:");
            foreach (var order in Orders)
            {
                var customer = GetCustomerById(order.CustomerId);
                string customerName = customer != null ? customer.Name : "Unknown Customer";

                Console.WriteLine($"Customer: {customerName}");
                Console.WriteLine("Products in order:");
                foreach (var product in order.Products)
                {
                    Console.WriteLine($" - {product.Name} ({product.Category}): {product.Price:C} x {product.Quantity}");
                }

                Console.WriteLine($"Total: {order.TotalAmount:C}");
                Console.WriteLine(new string('-', 40));
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

