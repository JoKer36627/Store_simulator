using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_simulator
{
    class Order
    {
        public List<Product> Products { get; private set; }

        public DateTime OrderDate { get; private set; }

        public Order(List<Product> products)
        {
            Products = products ?? new List<Product>();
            OrderDate = DateTime.Now;
        }

        public decimal TotalAmount
        {
            get
            {
                return Products.Sum(p=> p.Price * p.Quantity);
            }
        }

        public override string ToString()
        {
            return $"Order from {OrderDate}: Total {TotalAmount:C}, Items: {Products.Count}";
        }

        enum OrderStatus
        {
            Created;
            Paid;
            Compleated;
            Cancelled;
        }


    }
}
