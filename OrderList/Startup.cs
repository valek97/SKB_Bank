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
            //����������� ������� �� appsettings.json
            Configuration.Bind("Project", new Config());

            //����������� ����������� ���������� � �������� ��������
            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<IOrdersRepository, EFOrdersRepository>();
            services.AddTransient<IBuyersRepository, EFBuyersRepository>();
            services.AddTransient<DataManager>();

            //����������� ��������� ��
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectingString));

            //��������� identity �������
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
             {
                 opts.User.RequireUniqueEmail = true;
                 opts.Password.RequiredLength = 6;
                 opts.Password.RequireNonAlphanumeric = false;
                 opts.Password.RequireLowercase = false;
                 opts.Password.RequireUppercase = false;
                 opts.Password.RequireDigit = false;
             }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //��������� authentitication cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "myCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });

            //����������� �������� ����������� ��� Admin area
            services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
            });
            //��������� ������������ MVC
            services.AddControllersWithViews(x =>
            {
                x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
            })
                //������������� � ��� 3.0
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //������� ����������� ������ � midleware
            //����������� �� �������
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //����������� ��������� ������ � ����������
            app.UseStaticFiles();

            //����������� ������� �������������
            app.UseRouting();

            //����������� �������������� � �����������
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();



            //����������� ������� ��������(��������)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
