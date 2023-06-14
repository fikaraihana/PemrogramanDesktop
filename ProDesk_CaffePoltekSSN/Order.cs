using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProDesk_CaffePoltekSSN
{
    internal class Order
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string level { get; set; } = string.Empty;
        public string Item { get; set; } = string.Empty;
        public decimal ItemPrice { get; set; }
        public string Size { get; set; } = string.Empty;
        public decimal SizePrice { get; set; }
        public string Sugarlvl { get; set; } = string.Empty;
        public string Icelvl { get; set; } = string.Empty;
        public decimal AddonPrice { get; set; }
        public decimal Bill { get; set; }
        public DateTime Waktu { get; set; }

        public void TotalHarga()
        {
            var total = ItemPrice + SizePrice + AddonPrice;
            Bill = total;
        }
    }

}
