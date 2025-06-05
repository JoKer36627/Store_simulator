using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_simulator.UI
{
    class MenuManager
    {
        private List<MenuItem> menuItems;

        public MenuManager()
        {
            menuItems = new List<MenuItem>();
        }

        public void AddMenuItem(MenuItem item)
        {
            menuItems.Add(item);
        }

        public List<MenuItem> GetMenuItems()
        {
            return menuItems;
        }
    }
}
