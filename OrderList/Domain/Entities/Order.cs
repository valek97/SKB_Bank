using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderList.Domain.Entities
{
    public class Order
    {
        [Required]
        public int OrderId { get; set; }

        [Display(Name = "Цена")]
        public  double Price { get; set; }
        //Ссылка на покупателя
        public int BuyerId { get; set; }
        public Buyer Buyer { get; set; }
    }
}
