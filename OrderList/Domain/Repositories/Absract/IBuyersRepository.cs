using OrderList.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderList.Domain.Repositories.Absract
{
    public interface IBuyersRepository
    {
        IQueryable<Buyer> GetBuyers();
        Buyer GetBuyerById(int id);
        void SaveBuyer(Buyer entity);
        void DeleteBuyer(int id);
    }
}
