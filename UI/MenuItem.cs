using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_simulator.UI
{
    class MenuItem
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public MenuItem(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
