
using System.Collections.Generic;
using Today.Model.Models;
using Today.Model.Repositories;
using System.Linq;
using Today.Web.DTOModels.locationDTO;
using static Today.Web.DTOModels.locationDTO.LocationDTO;

namespace Today.Web.Services.locationService
{
    public class LocationService:ILocationService
    {
        private readonly IGenericRepository _repo;
        public LocationService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public LocationDTO GetLocation()
        {
            var result = new LocationDTO() { ProductLocationList = new List<ProductLocationinfo>() };

            var product = _repo.GetAll<Product>();
            var productLocation = _repo.GetAll<Location>().Where(lo=> product.Select(p => p.ProductId).Contains(lo.ProductId));
            


            var city = _repo.GetAll<City>().Where(c => product.Select(p => p.CityId).Contains(c.CityId));
            var photo = _repo.GetAll<ProductPhoto>().Where(ph=> product.Select(p=>p.ProductId).Contains(ph.ProductId));

            var program = _repo.GetAll<Today.Model.Models.Program>().Where(p => product.Select(pr => pr.ProductId).Contains(p.ProductId));
            var specificationList = _repo.GetAll<ProgramSpecification>();

            var ProductCategory =_repo.GetAll<ProductCategory>().Where(p => product.Select(pr => pr.ProductId).Contains(p.ProductId));
            var Category =_repo.GetAll<Category>().Where(p=> ProductCategory.Select(pc=>pc.CategoryId).Contains(p.CategoryId));


            var comment = _repo.GetAll<Today.Model.Models.Comment>();
            var commentdata = product.Select(x => new
            {
                ProductId = x.ProductId,
                AvgStar = (comment.Where(c => c.ProductId == x.ProductId)
                   .Count() != 0) ? (float)comment.Where(c => c.ProductId == x.ProductId)
                   .Sum(c => c.RatingStar) / comment.Where(c => c.ProductId == x.ProductId).Count() : 0
            });

            var Geting = (from pro in product 
                          join loca in productLocation on pro.ProductId equals loca.ProductId
                          join c in city on pro.CityId equals c.CityId
                          join pho in photo on loca.ProductId equals pho.ProductId
                          where pho.Sort == 1
                          join pg in program on pro.ProductId equals pg.ProgramId
                          join spe in specificationList on pg.ProgramId equals spe.SpecificationId
                          join pc in ProductCategory on pro.ProductId equals pc.ProductId 
                          select new {ProductId = pro.ProductId ,CityId =c.CityId , CityName = c.CityName,LocationId =loca.LocationId,
                              IsIsland=c.IsIsland, ProductName = pro.ProductName, Longitude = loca.Longitude, Latitude = loca.Latitude, Path = pho.Path ,spe.UnitPrice ,spe.Itemtext ,
                              pc.CategoryId 
                          }
                           ).ToList();

            foreach (var item in Geting)
            {
                var totalstartemp = commentdata.Where(x => x.ProductId == item.ProductId).Select(x => x.AvgStar).First();

                result.ProductLocationList.Add(new ProductLocationinfo() { ProductId = item.ProductId,
                    LocationId = item.LocationId ,
                    ProductName = item.ProductName ,
                    CityId=item.CityId,
                    CityName = item.CityName,
                    Longitude = item.Longitude,
                    Latitude = item.Latitude,
                    Path = item.Path,
                    RatingStar = totalstartemp,
                    Price =item.UnitPrice,
                    Itemtext = item.Itemtext,
                    IsIsland = item.IsIsland,
                    CategoryId=item.CategoryId,

                });

            }

            return result;
        }
        public LocationDTO GetCityLocation(int id)
        {
            var city = _repo.GetAll<City>().Where(x=>x.CityId==id);
            var result = new LocationDTO() { CityLocationList = new List<CityLocationInfo>()};

            var Get = (from c in city
                       select new { c.CityId, c.Latitude, c.Longitude });
            foreach(var item in Get)
            {
                result.CityLocationList.Add(new CityLocationInfo()
                {
                   CityId=item.CityId,
                   City_Latitude=item.Latitude,
                   City_Longtitude=item.Longitude,
                   
                });
            }
            return result;
        }
        public LocationDTO GetParentCard()
        {
            var result = new LocationDTO{ GetParentCardList = new List<GetParentCard>() };
            var productList = _repo.GetAll<Product>();
            var productPhotoList = _repo.GetAll<ProductPhoto>();
            var productCategoryList = _repo.GetAll<ProductCategory>();
            var categoryList = _repo.GetAll<Category>();
            var cityList = _repo.GetAll<City>();
            var productTagList = _repo.GetAll<ProductTag>();
            var tagList = _repo.GetAll<Tag>();
            var programList = _repo.GetAll<Model.Models.Program>();
            var specificationList = _repo.GetAll<ProgramSpecification>();
            var commentList = _repo.GetAll<Model.Models.Comment>();
            var orderDetailList = _repo.GetAll<OrderDetail>();
            var mainCategoryList = categoryList.Where(category => category.ParentCategoryId == null);
            var categoryListGroup = categoryList.Where(category => category.ParentCategoryId != null).ToList().GroupBy(category => category.ParentCategoryId);

            result = new LocationDTO()
            {
                GetParentCardList = productList.Select(x => new GetParentCard
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    ProductPhoto = productPhotoList.Where(pp => pp.ProductId == x.ProductId).Select(x => x.Path).First(),
                    CityId = x.CityId,
                    CityName = cityList.Where(c => c.CityId == x.CityId).Select(c => c.CityName).First(),
                    Tags = productTagList.Where(pt => pt.ProductId == x.ProductId).Join(tagList, pt => pt.TagId, t => t.TagId, (pt, t) => new { pt.ProductId, t.TagText }).Select(x => x.TagText).ToList(),
                    Rating = new RatingInfo() { RatingStar = (commentList.Where(comment => comment.ProductId == x.ProductId).Count() != 0) ? (float)commentList.Where(comment => comment.ProductId == x.ProductId).Sum(comment => comment.RatingStar) / commentList.Where(comment => comment.ProductId == x.ProductId).Count() : 0, TotalGiveComment = commentList.Where(comment => comment.ProductId == x.ProductId).Count() },

                    TotalOrder = programList.Where(program => program.ProductId == x.ProductId)
                                            .Join(specificationList, program => program.ProgramId, specification => specification.ProgramId, (program, specification) => new { program.ProgramId, specification.SpecificationId })
                                            .Join(orderDetailList, specification => specification.SpecificationId, orderDetail => orderDetail.SpecificationId, (specification, orderDetail) => new { orderDetail.Quantity })
                                            .Sum(n => n.Quantity),

                    Prices = programList.Where(program => program.ProductId == x.ProductId).Join(specificationList, program => program.ProgramId, specification => specification.ProgramId, (program, specification) => new PriceInfo { OriginalPrice = specification.OriginalUnitPrice, Price = specification.UnitPrice }).OrderBy(specification => specification.Price).FirstOrDefault()
                }).ToList()
            };


            return result;
        }
        public LocationDTO GetOffIslandCard()
        {
            var city = _repo.GetAll<City>().Where(x => x.IsIsland == false);
            var result = new LocationDTO() { OffIslandList = new List<OffCity>() };

            var Get = (from c in city
                       select new { c.CityId, c.CityName, c.CityImage });
            foreach (var item in Get)
            {
                result.OffIslandList.Add(new OffCity()
                {
                    CityId = item.CityId,
                    CityName = item.CityName,
                    CityImage = item.CityImage,

                });
            }
            return result;
        }
    }
}
