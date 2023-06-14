using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProDesk_CaffePoltekSSN
{
    internal class Menu
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Menu(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
