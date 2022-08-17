using System.Collections;
using System.Collections.Generic;
using Today.Model.Models;
using TodayMVC.Admin.DTOModels.ProductDTO;

namespace TodayMVC.Admin.Repositories.DapperProductRepositories
{
    public interface IDapperProductRepository : IDapperGenericRepository<Product>
    {
        public IEnumerable<UpdateProductDTO.ProductInfo> SelectAllProduct();
    }
}
