using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderList.Domain.Entities
{
    public class Buyer
    {
        [Required]
        public int BuyerId { get; set; }

        [Display(Name = "ФИО")]
        public  string FIO { get; set; }

        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public  DateTime Birthday { get; set; }

        [Display(Name = "Мобильный телефон")]
        public  string MobilePhone { get; set; }
        //Ссылка на заказы
        public List<Order> Orders { get; set; }
    }
}
