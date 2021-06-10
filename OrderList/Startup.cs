using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderList.Domain;
using OrderList.Domain.Repositories.Absract;
using OrderList.Domain.Repositories.EntityFramework;
using OrderList.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderList
{
    public class Startup
    {
        public IConfiguration Configuration { get; } 
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public void ConfigureServices(IServiceCollection services)  
        {
            //Подключение конфига из appsettings.json
            Configuration.Bind("Project", new Config());

            //Подключение функционала приложения в качестве сервисов
            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<IOrdersRepository, EFOrdersRepository>();
            services.AddTransient<IBuyersRepository, EFBuyersRepository>();
            services.AddTransient<DataManager>();

            //Подключение контекста БД
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectingString));

            //настройка identity системы
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
             {
                 opts.User.RequireUniqueEmail = true;
                 opts.Password.RequiredLength = 6;
                 opts.Password.RequireNonAlphanumeric = false;
                 opts.Password.RequireLowercase = false;
                 opts.Password.RequireUppercase = false;
                 opts.Password.RequireDigit = false;
             }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //настройка authentitication cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "myCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });

            //Настраиваем политику авторизации для Admin area
            services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
            });
            //Поддержка контролллера MVC
            services.AddControllersWithViews(x =>
            {
                x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
            })
                //Совместимость с кор 3.0
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Порядок регистрации данных в midleware
            //Подробности об ошибках
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Подключение статичных файлов в приложении
            app.UseStaticFiles();

            //Подключение системы маршрутизации
            app.UseRouting();

            //Подключение аутентификации и авторизации
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();



            //Регистрация нужного маршрута(ендпоинт)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
