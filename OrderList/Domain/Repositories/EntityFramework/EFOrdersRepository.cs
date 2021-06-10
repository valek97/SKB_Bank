using Microsoft.EntityFrameworkCore;
using OrderList.Domain.Entities;
using OrderList.Domain.Repositories.Absract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderList.Domain.Repositories.EntityFramework
{
    public class EFOrdersRepository : IOrdersRepository
    {
        private readonly AppDbContext context;
        public void DeleteOrder(int id)
        {
            context.Orders.Remove(new Order() { OrderId = id });
            context.SaveChanges();
        }

        public Order GetOrderById(int id)
        {
            return context.Orders.FirstOrDefault(x => x.OrderId == id);
        }

        public IQueryable<Order> GetOrders()
        {
            return context.Orders;
        }

        public void SaveOrder(Order entity)
        {
            if (entity.OrderId == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
