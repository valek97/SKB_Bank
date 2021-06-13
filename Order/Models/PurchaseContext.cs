using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Models
{
    public class PurchaseContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public PurchaseContext(DbContextOptions<PurchaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
