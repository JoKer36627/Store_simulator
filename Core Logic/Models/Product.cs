using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_simulator.Data
{
    public class Product
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }

        public Product(string name, decimal price, int quantity, string category)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Quantity = quantity;
            Category = category;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice >= 0)
            {
                Price = newPrice;
            }
            else
            {
                Console.WriteLine("Invalid price. Price must be greater than zero.");
            }
        }

        public void UpdateQuantity(int amount)
        {
            Quantity += amount;

            if (Quantity < 0)
            {
                Quantity = 0; // Reset to zero if negative
                Console.WriteLine("Invalid quantity. Quantity cannot be negative.");
            }
        }
        public override string ToString()
        {
            return $"{Name} - {Category}: {Price:C} (Quantity: {Quantity})";
        }

        public Product Clone()
        {
            return new Product(Name, Price, Quantity, Category);
        }
    }
}
