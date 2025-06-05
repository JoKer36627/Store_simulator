using Store_simulator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_simulator.Core_Logic.Services
{
    class ProductService
    {

        private readonly List<Product> _products;

        public ProductService(List<Product> products)
        {
            _products = products;
        }
        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public Product GetProductByName(string name)
        {
            var product = _products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (product != null)
                return product;

            else
            {
                Console.WriteLine("Product not found or unavailable.");
                return null;
            }
        }

        public Product GetProductById(Guid id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void ListAllProducts()
        {
            if (_products.Count == 0)
            {
                Console.WriteLine("No products available.");
                return;
            }

            Console.WriteLine("Available products:");

            foreach (var product in _products)
            {
                Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}, Category: {product.Category}"
                    );
            }
        }





    }
}
