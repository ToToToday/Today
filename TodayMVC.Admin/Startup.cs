using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using TodayMVC.Admin.Repositories;
using TodayMVC.Admin.Repositories.DapperMailRepositories;
using TodayMVC.Admin.Repositories.DapperCommentManage;
using TodayMVC.Admin.Repositories.DapperMemberRepositories;
using TodayMVC.Admin.Repositories.DapperSalesRepositories;
using TodayMVC.Admin.Repositories.DapperOrderRepositories;
using TodayMVC.Admin.Services.MailService;
using TodayMVC.Admin.Repositories.DapperProductRepositories;
using TodayMVC.Admin.Services.MemberService;
using TodayMVC.Admin.Services.SalesServices;
using TodayMVC.Admin.Services.UploadService;
using TodayMVC.Admin.Services.OrderServices;
using TodayMVC.Admin.Services;
using TodayMVC.Admin.Services.CloudinaryService;

using TodayMVC.Admin.Services.ProductServices;

namespace TodayMVC.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //DI®e¾¹
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<TodayDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TodayDB"));

            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodayAdmin", Version = "v1" });
            });
            services.AddScoped<IDbConnection, SqlConnection>(serviceProvider =>
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Configuration.GetConnectionString("TodayDB");
                return conn;
            });

            services.AddTransient<IDapperGenericRepository<Member>, DapperMemberRepository>();
            services.AddTransient<IDapperMemberRepository, DapperMemberRepository>();
            //??
            services.AddTransient<IDapperGenericRepository<Comment>, DapperCommentManage>();
            services.AddTransient<IDapperCommentManage, DapperCommentManage>();
            services.AddTransient<IMemberService, MemberService>();
            //services.AddScoped<CloudinaryService>();

            services.AddTransient<IDapperGenericRepository<OrderDetail>, DapperOrderRepository>();
            services.AddTransient<IDapperOrderRepository, DapperOrderRepository>();
            services.AddTransient<IDapperGenericRepository<Subscription>, DapperMailRepository>();
            services.AddTransient<IDapperMailRepository, DapperMailRepository>();
            services.AddTransient<ICreateProductServices, CreateProductServices>();
            services.AddTransient<IDapperSalesRepository, DapperSalesRepository>();
            
            services.AddTransient<IDapperGenericRepository<Product>, DapperProductRepository>();
            services.AddTransient<IDapperProductRepository,DapperProductRepository>();
            services.AddTransient<IUpdateProductService, UpdateProductService>();
            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<ICloudinaryService2, CloudinaryService2>();



            services.AddTransient<ISalesService, SalesService>();

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
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodayAdmin v1"));
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Sales}/{action=Chart}/{id?}");
            });
        }
    }
}
