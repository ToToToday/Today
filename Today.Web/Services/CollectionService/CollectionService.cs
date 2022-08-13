using System;
using System.Collections.Generic;
using System.Linq;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.DTOModels.CollectionDTO;
using Today.Web.DTOModels.ProductDTO;
using Today.Web.Services.ProductService;
using Today.Web.ViewModels;

namespace Today.Web.Services.CollectService
{
    public class CollectionService : ICollectionService
    {
        private readonly IGenericRepository _repo;
        private readonly IProductService _productService;
        public CollectionService(IGenericRepository repo, IProductService productService)
        {
            _repo = repo;
            _productService = productService;
        }

        public void CreateCollect(CollectionVM request)
        {
            var productIdVerify = _repo.GetAll<Collect>().Where(c => c.ProductId == request.ProductId && c.MemberId == request.ProductId).Count();

            if (productIdVerify == 0)
            {
                var result = new Collect()
                {
                    MemberId = request.MemberId,
                    ProductId = request.ProductId,
                    CreateTime = request.Time
                };

                _repo.Create<Collect>(result);
                _repo.SavaChanges();
            }
        }
        public void RemoveCollect(CollectionVM request)
        {
            var result = _repo.GetAll<Collect>().Where(c => c.MemberId == request.MemberId && c.ProductId == request.ProductId).FirstOrDefault();

            _repo.Delete<Collect>(result);
            _repo.SavaChanges();
        }
        public List<ProductDTO.ProductInfo> GetAllCollect(int userid)
        {
            var favoriteList = _repo.GetAll<Collect>().Where(c => c.MemberId == userid).OrderByDescending(c => c.CollectId).Select(c => c.ProductId);
            var productQueryable = _productService.AllProduct().QueryProduct;

            var result = productQueryable.Where(p => favoriteList.Contains(p.Id)).ToList();

            return result;
        }
    }
}
