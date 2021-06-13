using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MobilePhone { get; set; }
    }
}
