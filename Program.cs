namespace Store_simulator
{
    internal class Program
    {
        static void Main(string[] args)
        {

            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("DEBUG: Start menu");
                Console.WriteLine("Welcome to the Store Simulator!");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Create Order");
                Console.WriteLine("3. Show All Products");
                Console.WriteLine("4. Show All Orders");
                Console.WriteLine("5. Exit");


                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        // Logic to create a new store
                        Console.WriteLine("Enter product name: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter product price: ");
                        decimal price = decimal.Parse(Console.ReadLine());

                        Console.Write("Enter product quantity: ");
                        int quantity = int.Parse(Console.ReadLine());
                        break;
                    case "2":
                        // Logic to add products to the store
                        break;
                    case "3":
                        // Logic to create a customer
                        break;
                    case "4":
                        // Logic to list all products
                        break;
                    case "5":
                        exit = true;
                        // Logic to place an order
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }


        }
    }
}
