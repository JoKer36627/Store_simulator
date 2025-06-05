using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_simulator.UI
{
    public static class ConsoleUI
    {
        public static void ShowTitle()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Store Simulator!");
        }

        public static void ShowMenu()
        {
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Create Order");
            Console.WriteLine("3. Show All Products");
            Console.WriteLine("4. Show All Orders");
            Console.WriteLine("5. Cancel Order");
            Console.WriteLine("6. Exit");
        }

        public static string ReadInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        public static void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static void Wait()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
