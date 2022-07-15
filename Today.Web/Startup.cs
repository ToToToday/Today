using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.Services.AccountService;
using Today.Web.Services.CityService;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Today.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<TodayDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TodayDB"));
            });
            services.AddTransient<IGenericRepository, GenericRepository>();
            services.AddTransient<ICityService, CityService>();
            //註冊DI
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<GenericRepository>();
            //services.AddScoped<IHttpContextAccessor, HttpContextAccessor>(); //(不要用，用內建方法【下面那行】)
            services.AddHttpContextAccessor();

            //驗證
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    //全部寫得和預設值一樣

                    //設定登入Action的路徑： 
                    //options.LoginPath = new PathString("/Account/Login");

                    ////設定 導回網址 的QueryString參數名稱：
                    //options.ReturnUrlParameter = "ReturnUrl";

                    ////設定登出Action的路徑： 
                    //options.LogoutPath = new PathString("/Account/Logout");

                    ////若權限不足，會導向的Action的路徑
                    //options.AccessDeniedPath = new PathString("/Account/AccessDenied");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();//加
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
