using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Models
{
    public class SampleData
    {
        public static void Initialize(PurchaseContext context)
        {
            if (!context.Clients.Any())
            {
                context.Clients.AddRange(
                    new Client
                    {
                        FIO = "Критенко Валентин Григорьевич",
                        DateOfBirth = DateTime.Parse("14.02.1997"),
                        MobilePhone = "+7-919-037-64-98"
                    },
                    new Client
                    {
                        FIO = "Чугрин Павел Николаевич",
                        DateOfBirth = DateTime.Parse("25.07.1996"),
                        MobilePhone = "+7-513-037-64-98"
                    },
                    new Client
                    {
                        FIO = "Лоскутов Евгений Анатольевич",
                        DateOfBirth = DateTime.Parse("27.01.1999"),
                        MobilePhone = "+7-513-037-86-98"
                    }
                );
                if (!context.Purchases.Any())
                {
                    context.Purchases.AddRange(
                        new Purchase
                        {
                            PurchaseAmount = 6000,
                            ClientId = 1

                        },
                        new Purchase
                        {
                            PurchaseAmount = 6500,
                            ClientId = 2

                        },
                        new Purchase
                        {
                            PurchaseAmount = 4999,
                            ClientId = 3

                        },
                        new Purchase
                        {
                            PurchaseAmount = 6899,
                            ClientId = 1

                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
