using Store_simulator.Controllers;
using Store_simulator.Core_Logic.Models;
using Store_simulator.Data;
using Store_simulator.DataService.Implementations;
using System.Diagnostics.Metrics;

namespace Store_simulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuController.Start();
        }
    }
}
