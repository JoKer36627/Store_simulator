using Store_simulator.Core_Logic.Models;
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

            _productService = new ProductService(Products);
            _orderService = new OrderService(Orders, Products, Customers);
        }

        public void SaveAll()
        {
            storageManager.SaveData("products.json", Products);
            storageManager.SaveData("customers.json", Customers);
            storageManager.SaveData("orders.json", Orders);
        }

        public void AddProduct(Product product)
        {
            _productService.AddProduct(product);
        }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public Order MakeOrder(Customer customer, List<OrderItem> items)
        {
            return _orderService.MakeOrder(customer, items);
        }

        public void CancelOrder(Order order) 
        {
           _orderService.CancelOrder(order);
        }


        public Product GetProductByName(string name)
        {
            return _productService.GetProductByName(name);
        }
        public Customer GetCustomerById(Guid id)
        {
            return Customers.FirstOrDefault(c => c.Id == id);
        }

        public Product GetProductById(Guid id)
        {
            return _productService.GetProductById(id);
        }



        public void ListAllProducts()
        {
            _productService.ListAllProducts();
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
            _orderService.ListAllOrders();
        }
     

        public void InitializeTestData(Store store)
        {
            if (store.Customers.Count == 0)
            {

                var c1 = new Customer("Alice", 1000);  // Id generated automatically
                var c2 = new Customer("David", 80);
                var c3 = new Customer("Alex", 570);

                store.AddCustomer(c1);
                store.AddCustomer(c2);
                store.AddCustomer(c3);

            }
        }


}
}

