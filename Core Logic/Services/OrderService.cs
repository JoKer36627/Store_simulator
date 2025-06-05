using Store_simulator.Core_Logic.Models;
using Store_simulator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_simulator.Core_Logic.Services
{
    class OrderService
    {

        private readonly List<Order> _orders;
        private readonly List<Product> _products;
        private readonly List<Customer> _customers;

        public OrderService(List<Order> orders, List<Product> products, List<Customer> customers)
        {
            _orders = orders;
            _products = products;
            _customers = customers;
        }

        private Customer GetCustomerById(Guid id)
        {
            return _customers.FirstOrDefault(c => c.Id == id);
        }

        private Product GetProductByName(string name)
        {
            return _products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }


        public Order MakeOrder(Customer customer, List<OrderItem> items)
        {
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return null;
            }

            if (items == null || items.Count == 0)
            {
                Console.WriteLine("No items specified for the order.");
                return null;
            }

            foreach (var item in items)
            {
                var storeProduct = GetProductByName(item.Product.Name);
                if (storeProduct == null)
                {
                    Console.WriteLine($"Product {item.Product.Name} not found in store.");
                    return null;
                }

                if (storeProduct.Quantity < item.Quantity)
                {
                    Console.WriteLine($"Not enough quantity for {item.Product.Name}. Available: {storeProduct.Quantity}, Requested: {item.Quantity}");
                    return null;
                }
            }

            decimal totalBalance = items.Sum(i => i.GetTotalPrice());
            if (customer.Balance < totalBalance)
            {
                Console.WriteLine("Customer does not have enough balance.");
                return null;
            }

            customer.Balance -= totalBalance;

            // create order with cloned products
            List<Product> orderProducts = items.Select(i =>
            {
                var clone = i.Product.Clone();
                clone.Quantity = i.Quantity;
                return clone;
            }).ToList();

            Order order = new Order(orderProducts, customer.Id);
            _orders.Add(order);
            customer.AddOrder(order);

            return order;
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
                    _products.Add(orderedProduct.Clone());
                }
            }

            // Change status to Cancelled
            order.ChangeStatus(OrderStatus.Cancelled);

            _orders.Remove(order);
            Console.WriteLine("Order cancelled successfully.");
        }


        public void ListAllOrders()
        {
            if (_orders.Count == 0)
            {
                Console.WriteLine("No orders available.");
                return;
            }

            Console.WriteLine("All Orders:");
            for (int i = 0; i < _orders.Count; i++)
            {
                var order = _orders[i];
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
    }
}
