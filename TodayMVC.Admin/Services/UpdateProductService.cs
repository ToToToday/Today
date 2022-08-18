using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Today.Model.Models;
using TodayMVC.Admin.Repositories.DapperProductRepositories;
using TodayMVC.Admin.ViewModels;
using static TodayMVC.Admin.ViewModels.UpdateProductVM;

namespace TodayMVC.Admin.Services
{
    public class UpdateProductService : IUpdateProductService
    {
        private readonly IDapperProductRepository _dapperRepo;
        public UpdateProductService(IDapperProductRepository dapperRepo)
        {
            _dapperRepo = dapperRepo;
        }
        public int UpdateProduct([FromBody]UpdateProductVM updateProduct)
        {
            

            var productVM = new Product()
            {
                ProductId = updateProduct.ProductId,
                ProductName = updateProduct.ProductName,
                //City = new City { CityName = updateProduct.CityName },
            };


            var result = _dapperRepo.Update(productVM);
            

            return result;

        }
    }
}
