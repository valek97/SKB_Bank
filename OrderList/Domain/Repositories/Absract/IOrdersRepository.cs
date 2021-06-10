using OrderList.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderList.Domain.Repositories.Absract
{
    public interface IOrdersRepository
    {
        IQueryable<Order> GetOrders();
        Order GetOrderById(int id);
        void SaveOrder(Order entity);
        void DeleteOrder(int id);
    }
}
