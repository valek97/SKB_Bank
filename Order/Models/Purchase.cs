using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public double PurchaseAmount { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
