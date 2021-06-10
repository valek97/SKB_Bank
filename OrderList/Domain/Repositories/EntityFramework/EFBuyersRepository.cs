using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderList.Domain.Entities;
using OrderList.Domain.Repositories.Absract;

namespace OrderList.Domain.Repositories.EntityFramework
{
    public class EFBuyersRepository : IBuyersRepository
    {
        private readonly AppDbContext context;

        public void DeleteBuyer(int id)
        {
            context.Buyers.Remove(new Buyer() { BuyerId = id });
            context.SaveChanges();
        }

        public Buyer GetBuyerById(int id)
        {
            return context.Buyers.FirstOrDefault(x => x.BuyerId == id);
        }

        public IQueryable<Buyer> GetBuyers()
        {
            return context.Buyers;
        }

        public void SaveBuyer(Buyer entity)
        {
            if (entity.BuyerId == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
