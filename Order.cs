using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_simulator
{

    enum OrderStatus
    {
        Created,
        Paid,
        Compleated,
        Cancelled
    }
    class Order
    {
        public List<Product> Products { get; private set; }

        public DateTime OrderDate { get; private set; }

        public OrderStatus Status { get; private set; }

        public Order(List<Product> products)
        {
            Products = products ?? new List<Product>();
            OrderDate = DateTime.Now;
            Status = OrderStatus.Created;
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

        public void ChangeStatus(OrderStatus newStatus)
        {

            OrderStatus currentStatus = Status;

            // Check if the new status is valid
            if (currentStatus == newStatus)
            {
                Console.WriteLine("Status is already " + newStatus);
                return;
            }


            if (currentStatus == OrderStatus.Compleated && newStatus == OrderStatus.Created)
            {
                Console.WriteLine("You can't change status from Completed back to Created.");
                return;
            }

            if (currentStatus == OrderStatus.Cancelled && newStatus == OrderStatus.Paid)
            {
                Console.WriteLine("You can't change status from Cancceled back to Paid.");
                return;
            }



            Status = newStatus;


            switch (newStatus)
            {
                case OrderStatus.Created:
                    Console.WriteLine("Order created.");
                    break;

                case OrderStatus.Paid:
                    Console.WriteLine("Order Paid.");
                    break;
                case OrderStatus.Compleated:
                    Console.WriteLine("Order completed.");
                    break;
                case OrderStatus.Cancelled:
                    Console.WriteLine("Order cancelled.");
                    break;
            }


            
        }


    }
}
