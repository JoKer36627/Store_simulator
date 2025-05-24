using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_simulator
{
    class Customer
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public List<Order> Orders { get; private set; }
        -=
        public Customer(string name, decimal balance)
        {
            Name = name;
            Balance = balance;
            Orders = new List<Order>();
        }

        public void AddOrder(Order order)
        {
            Orders.Add(order);
            Balance -= order.TotalAmount;
        }

        public override string ToString()
        {
            return $"{Name} - Balance: {Balance:C}, Orders count: {Orders.Count}";
        }

    }
}
