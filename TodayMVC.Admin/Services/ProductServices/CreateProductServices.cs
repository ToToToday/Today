using TodayMVC.Admin.DTOModels.ProductDTO;
using Today.Model.Repositories;
using Today.Model.Models;

namespace TodayMVC.Admin.Services.ProductServices
{
    public class CreateProductServices : ICreateProductServices
    {
        private readonly IGenericRepository _repo;
        public CreateProductServices(IGenericRepository repo)
        {
            _repo = repo;
        }
        public void CreateProduct(CreateProductDTO product)
        {
            var supplier = new Supplier
            {
                Address = product.supplier.Address,
                CompanyName = product.supplier.CompanyName,
                ContactName = product.supplier.ContactName,
                ContactTitle = product.supplier.ContactTitle,
                Phone = product.supplier.Phon,
                CityId = product.supplier.City,
            };
            //_repo.Create()
            var reuslt = new Product
            {
                ProductName = product.ProductName,
                CityId = product.City,
                HowUse = product.HowUse,
                Illustrate = product.ProductText,
                ShoppingNotice = product.ShoppingNotice,
                CancellationPolicy = product.CancellationPolicy,
                Isdeleted = product.Isdeleted
            };
        }
    }
}
