using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderList.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderList.Domain
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        //Создание таблиц и заполнение первоначальными данными
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TextField> TextFields { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            modelbuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "13ebb1ae-86fd-4e45-b992-2ef3206a28db",
                Name = "admin",
                NormalizedName = "ADMIN"
            });
            modelbuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "e7d8538a-35d9-4b13-927d-e82cf006fcbb",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "my@email.com",
                NormalizedEmail = "MY@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null,"superpassword"),
                SecurityStamp = string.Empty
            });
            modelbuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
               RoleId = "13ebb1ae-86fd-4e45-b992-2ef3206a28db",
               UserId = "e7d8538a-35d9-4b13-927d-e82cf006fcbb"
            });
            modelbuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid ("d3492534-2182-4ba6-9abf-a4f738af0fc7"),
                CodeWord = "PageIndex",
                Title = "Главная"
            });
            modelbuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("8de023bf-7d1d-4851-927f-92a0472fbe28"),
                CodeWord = "PageServices",
                Title = "Наши услуги"
            });
            modelbuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("48909aad-714a-4da4-a985-727035419148"),
                CodeWord = "PageContacts",
                Title = "Контакты"
            });
            modelbuilder.Entity<Buyer>().HasData(new Buyer
            {
                BuyerId = 1,
                FIO = "Критенко Валентин Григорьевич",
                Birthday = DateTime.Parse("14.02.1997"),
                MobilePhone = "+71111111111"
            });
            modelbuilder.Entity<Buyer>().HasData(new Buyer
            {
                BuyerId = 2,
                FIO = "Лоскутов Евгений Анатольевич",
                Birthday = DateTime.Parse("14.06.1998"),
                MobilePhone = "+71111111112"
            });
            modelbuilder.Entity<Buyer>().HasData(new Buyer
            {
                BuyerId = 3,
                FIO = "Чугрин Павел Николаевич",
                Birthday = DateTime.Parse("27.05.1995"),
                MobilePhone = "+71111111113"
            });
            modelbuilder.Entity<Order>().HasData(new Order
            {
                OrderId = 1,
                Price = 6000,
                BuyerId = 1
            }) ;
            modelbuilder.Entity<Order>().HasData(new Order
            {
                OrderId = 2,
                Price = 6500,
                BuyerId = 2
            });
            modelbuilder.Entity<Order>().HasData(new Order
            {
                OrderId = 3,
                Price = 6300,
                BuyerId = 3
            });
            modelbuilder.Entity<Order>().HasData(new Order
            {
                OrderId = 4,
                Price = 6750,
                BuyerId = 1
            });
        }

    }
}
