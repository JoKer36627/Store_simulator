﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Store_simulator.Data
{
    enum OrderStatus
    {
        Created,
        Paid,
        Completed,
        Cancelled
    }

    class Order
    {
        public List<Product> Products { get; private set; }
        public DateTime OrderDate { get; private set; }
        public OrderStatus Status { get; private set; }

        public Guid CustomerId { get; private set; } 

        public Order(List<Product> products, Guid customerId)
        {
            Products = products ?? new List<Product>();
            OrderDate = DateTime.Now;
            Status = OrderStatus.Created;
            CustomerId = customerId;
        }

        public decimal TotalAmount
        {
            get
            {
                return Products.Sum(p => p.Price * p.Quantity);
            }
        }

        public override string ToString()
        {
            return $"Order from {OrderDate}: Total {TotalAmount:USD}, Items: {Products.Count}";
        }

        public void ChangeStatus(OrderStatus newStatus)
        {
            OrderStatus currentStatus = Status;

            if (currentStatus == newStatus)
            {
                Console.WriteLine("Status is already " + newStatus);
                return;
            }

            if (currentStatus == OrderStatus.Completed && newStatus == OrderStatus.Created)
            {
                Console.WriteLine("You can't change status from Completed back to Created.");
                return;
            }

            if (currentStatus == OrderStatus.Cancelled && newStatus == OrderStatus.Paid)
            {
                Console.WriteLine("You can't change status from Cancelled back to Paid.");
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
                case OrderStatus.Completed:
                    Console.WriteLine("Order completed.");
                    break;
                case OrderStatus.Cancelled:
                    Console.WriteLine("Order cancelled.");
                    break;
            }
        }
    }
}
