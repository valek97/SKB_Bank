using OrderList.Domain.Repositories.Absract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderList.Domain
{
    public class DataManager
    {
        public ITextFieldsRepository TextFields { get; set; }
        public IServiceItemsRepository ServiceItems { get; set; }

        public IOrdersRepository Orders { get; set; }
        public IBuyersRepository Buyers { get; set; }

        public DataManager(ITextFieldsRepository textFieldsRepository, IServiceItemsRepository serviceItemsRepository,IOrdersRepository ordersRepository, IBuyersRepository buyersRepository)
        {
            TextFields = textFieldsRepository;
            ServiceItems = serviceItemsRepository;
            Orders = ordersRepository;
            Buyers = buyersRepository;
        }
    }
}
