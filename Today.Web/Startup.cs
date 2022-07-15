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
            //���UDI
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<GenericRepository>();
            //services.AddScoped<IHttpContextAccessor, HttpContextAccessor>(); //(���n�ΡA�Τ��ؤ�k�i�U������j)
            services.AddHttpContextAccessor();

            //����
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    //�����g�o�M�w�]�Ȥ@��

                    //�]�w�n�JAction�����|�G 
                    //options.LoginPath = new PathString("/Account/Login");

                    ////�]�w �ɦ^���} ��QueryString�ѼƦW�١G
                    //options.ReturnUrlParameter = "ReturnUrl";

                    ////�]�w�n�XAction�����|�G 
                    //options.LogoutPath = new PathString("/Account/Logout");

                    ////�Y�v�������A�|�ɦV��Action�����|
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

            app.UseAuthentication();//�[
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
