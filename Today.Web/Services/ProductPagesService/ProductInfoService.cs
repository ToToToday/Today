using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.ViewModels;
using static Today.Web.DTOModels.ProductInfoDTO.ProductInfoDTO;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using System;
using Today.Web.DTOModels.ProductInfoDTO;

namespace Today.Web.Services.ProductInfoService
{
    public class ProductInfoService : IProductInfoService
    {
        //private static string _connectionStr = "Data Source=(localdb)\\mssqllocaldb;Database=TodayDB;";
        private readonly IGenericRepository _repo;
        public ProductInfoService(IGenericRepository repo)
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

            var citySource = _repo.GetAll<City>().First(c => c.CityId == source.CityId);
            var locationSource = _repo.GetAll<Location>().First(loc => loc.ProductId == source.ProductId);
            var productPhotoSource = _repo.GetAll<ProductPhoto>().Where(p => p.ProductId == source.ProductId).ToList();
            var programSource = _repo.GetAll<Model.Models.Program>().Where(p => p.ProductId == source.ProductId).ToList();

            var result = new ProductInfoResponseDTO
            {
                ProductInfo = new ProductInfoDTO.Product
                {
                    ShoppingNotice = source.ShoppingNotice,
                    ProductIsdeleted = source.Isdeleted,
                    ProductId = source.ProductId,
                    ProductName = source.ProductName,
                    ProductDesc = source.Illustrate,
                    HowUse = source.HowUse,
                    CancellationPolicy = source.CancellationPolicy,
                    CityName = citySource.CityName,
                    //ProductTag = TagList.First().TagText,
                    ProductLocationName = locationSource.Title,
                    ProductLocationAddress = locationSource.Address,
                    ProgarmList = programSource.Select(p =>
                    new ProductInfoDTO.Progarm
                    {
                        ProgarmId = p.ProgramId,
                        PorgramName = p.Title,
                        PrgarmText = p.Context,
                        ProgarmIsdeleted = p.Isdeleted,
                        AboutProgramList = new List<ProductInfoDTO.AboutProgram>(),
                        ProgramInciudeList = new List<ProgramInciude>(),
                        ProgramSpecificationList = new List<ProductInfoDTO.ProgramSpecification>(),
                        DateList = new List<ProductInfoDTO.Date>(),
                    }).ToList(),
                    PhtotList = productPhotoSource.Select(p =>
                    new ProductInfoDTO.Photo
                    {
                        PhotoUrl = p.Path
                    }).ToList(),
                }
            };
            result.ProductInfo.MemberComment = _repo.GetAll<Model.Models.Comment>()
                                                .Where(c => c.ProductId == requst.ProductId)
                                                .Select(c => new MemberComment
                                                {
                                                    CommentId = c.CommentId,
                                                    MemberId = c.MemberId,
                                                    MembermMessageText = c.CommentText ,
                                                    Star = c.RatingStar,
                                                    Data = c.CommentDate
                                               }).ToList();
            result.ProductInfo.MemberComment.ForEach(c =>
                _repo.GetAll<Member>().Where(m => c.MemberId == m.MemberId)
                .ToList()
                .ForEach(m =>
                {
                    c.MemberPhoto = m.Image;
                    c.MemberName = m.MemberName;
                }));

            result.ProductInfo.ProgarmList.ForEach(p =>
            {
                //DateList
                p.DateList = _repo.GetAll<Model.Models.ProgramCantUseDate>()
                    .Where(pcud => pcud.ProgramId == p.ProgarmId)
                    .Select(pcud => new Date { CantuseDate = pcud.Date, ProgramDateId = pcud.ProgramDateId }).ToList();

                _repo.GetAll<Model.Models.AboutProgram>()
                    .Where(ap => p.ProgarmId == ap.ProgramId)
                    .ToList()
                    .ForEach(ap =>
                    {
                        var apo = _repo.GetAll<Model.Models.AboutProgramOption>()
                                    .First(apo => apo.AboutProgramOptionsId == ap.AboutProgramOptionsId);
                        p.AboutProgramList.Add(
                            new ProductInfoDTO.AboutProgram
                            {
                                AboutProgramName = apo.Context,
                                IconClass = apo.IconClass,
                            });
                    });
                //ProgramIncludeList=,
                _repo.GetAll<Model.Models.ProgramInclude>()
                    .Where(pgid => p.ProgarmId == pgid.ProgramId)
                    .ToList()
                    .ForEach(pgid =>
                    {
                        p.ProgramInciudeList.Add(
                            new ProgramInciude
                            {
                                Inciudetext = pgid.Text,
                                IsInclude = (bool)pgid.IsInclude
                            });
                    });
                //ProgramSpecificationList=
                _repo.GetAll<Model.Models.ProgramSpecification>()
                .Where(pgsc => p.ProgarmId == pgsc.ProgramId)
                .ToList()
                .ForEach(pgsc =>
                {

                    p.ScreeningList = _repo.GetAll<Model.Models.Screening>()
                    .Where(sc => sc.SpecificationId == pgsc.SpecificationId)
                    .Select(pss =>
                        new ProductInfoDTO.Screening
                        {
                            ScreenId = pss.ScreeningId,
                            Date = pss.Time,
                            Status = pss.Status,
                            SpecificationId = pss.SpecificationId
                        }).ToList();
                    p.ProgramSpecificationList.Add(
                        new ProductInfoDTO.ProgramSpecification
                        {
                            PorgarmUnitPrice = pgsc.UnitPrice,
                            UnitText = pgsc.UnitText,
                            Itemtext = pgsc.Itemtext,
                            SpecificationId = pgsc.SpecificationId
                        });

                });
            });
           return result;
        }
    }
}
