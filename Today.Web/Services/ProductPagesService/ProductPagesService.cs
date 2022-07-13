using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.ViewModels;
using Today.Web.DTOModel;
using static Today.Web.DTOModel.ProductInfoDTO;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Today.Web.Services.ProductPagesService
{

    public class ProductPagesService : IProductPagesService
    {
        private static string _connectionStr = "Data Source=(localdb)\\mssqllocaldb;Database=TodayDB;";
        private readonly IGenericRepository _repo;
        public ProductPagesService(IGenericRepository repo)
        {
            _repo = repo;
        }
        public ProductInfoResponseDTO GetProduct(ProductInfoRequstDTO requst)
        {
            if (requst.ProductId <= 0) return null;
            // 呼叫 Repository 存取資料
            var source = _repo.GetAll<Model.Models.Product>().FirstOrDefault(p => p.ProductId == requst.ProductId);

            // 找不到這個商品id要怎麼處理 -> 這邊我也回傳null
            if (source == null) return null;

            var result = new ProductInfoResponseDTO { ProductInfo = new ProductInfoDTO.Product { } };

            using (SqlConnection conn = new SqlConnection(_connectionStr))
            {
                //抓到唯一Product
                string SqlProduct = @$"select  *
                                    from Product p 
                                    INNER JOIN City  C on p.CityId = c.CityId
                                    where p.ProductId = {requst.ProductId} ";
                //抓City
                string SqlCity = @$"select  c.CityName
                                    from Product p 
                                    INNER JOIN City  C on p.CityId = c.CityId
                                    where p.ProductId = {requst.ProductId} ";
                //抓City
                string SqlTag = @$"select  t.TagText
                                    from Product p 
                                    INNER JOIN ProductTag  pt on pt.ProductTagId = p.ProductTagId
                                    INNER JOIN Tag  t on pt.TagId = t.TagId
                                    where p.ProductId = {requst.ProductId} ";
                //抓地址
                string SqlLocation = @$"select L.Title,L.Address
                                    from Product p 
                                    INNER JOIN Location L on L.ProductId = p.ProductId
                                    where p.ProductId = {requst.ProductId} ";
                //抓圖片
                string SqlPhoto = @$"select pdp.Path
                                    from Product p
                                    INNER JOIN ProductPhoto pdp on pdp.ProductId = p.ProductId
                                    where p.ProductId = {requst.ProductId} ";
                //抓Progarm:標題、說明
                string SqlProgarm = @$"select  pg.ProgramId, pg.Title ,pg.Context
                                    from Product p 
                                    INNER JOIN Program  pg on p.ProductId = pg.ProductId
                                    where p.ProductId = {requst.ProductId} ";
                var productList = conn.Query<Model.Models.Product>(SqlProduct);
                var ProgarmList = conn.Query<Model.Models.Program>(SqlProgarm);
                var CityList = conn.Query<Model.Models.City>(SqlCity);
                //var TagList = conn.Query<Model.Models.Tag>(SqlTag);
                var LocationList = conn.Query<Model.Models.Location>(SqlLocation);
                var PhotoList = conn.Query<Model.Models.ProductPhoto>(SqlPhoto);

                if (productList != null)
                {
                    var product = productList.First();
                    var Location = LocationList.First();
                    result = new ProductInfoResponseDTO
                    {
                        ProductInfo = new ProductInfoDTO.Product
                        {
                            ShoppingNotice = product.ShoppingNotice,
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            ProductText = product.Illustrate,
                            HowUse = product.HowUse,
                            CancellationPolicy = product.CancellationPolicy,
                            CityName = CityList.First().CityName,
                            //ProductTag = TagList.First().TagText,
                            ProductLocationName = Location.Title,
                            ProductLocationAddress = Location.Address,
                            progarmList = ProgarmList.Select(p =>
                            new ProductInfoDTO.Progarm
                            {
                                PorgramName = p.Title,
                                PrgarmText = p.Context
                            }).ToList(),
                            PhtotList = PhotoList.Select(p =>
                            new ProductInfoDTO.Photo
                            {
                                PhotoUrl = p.Path
                            }).ToList(),
                        }
                    };
                }
                return result;
            }
        }
    }
}
