using TodayMVC.Admin.DTOModels.ProductDTO;
using Today.Model.Repositories;
using Today.Model.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using TodayMVC.Admin.ViewModels;

namespace TodayMVC.Admin.Services.ProductServices
{
    public class CreateProductServices : ICreateProductServices
    {
        private readonly IDbConnection _repo;
        public CreateProductServices(IDbConnection repo)
        {
            _repo = repo;
        }
        public void CreateProduct(CreateProductDTO product)
        {

            string sqlProduct = @"INSERT INTO Product(ProductName,CityId,HowUse,Illustrate,ShoppingNotice,CancellationPolicy,Isdeleted,SupplierId)" +
                                             " VALUES (@ProductName,@CityId,@HowUse,@Illustrate,@ShoppingNotice,@CancellationPolicy,@Isdeleted,@SupplierId) ";

            string sqlPhoto = @"INSERT INTO ProductPhoto(Path,Sort,ProductId)" +
                                                " VALUES (@Path,@Sort,@ProductId)";

            string sqlProgarm = @"INSERT INTO Program(ProductId,Title,Context,Isdeleted) 
                                         VALUES(@ProductId,@Title,@Context,@Isdeleted)";

            string sqlsupplier = @"INSERT INTO Supplier(Address,CompanyName,ContactName,ContactTitle,Phone,CityId)" +
                                              " VALUES (@Address,@CompanyName,@ContactName,@ContactTitle,@Phone,@CityId) ";

            string sqlaboutProgram = @"INSERT INTO AboutProgram(ProgramId,AboutProgramOptionsId) 
                                         VALUES(@ProgramId,@AboutProgramOptionsId)";
            string sqlCreateProgramSpecification = @"INSERT INTO ProgramSpecification(IsScreening,ProgramId,Itemtext,UnitPrice,OriginalUnitPrice,UnitText,Status) 
                                                                                VALUES(@IsScreening,@ProgramId,@Itemtext,@UnitPrice,@OriginalUnitPrice,@UnitText,@Status) ";

            string sqlProgramCantUseDate = @"INSERT INTO ProgramCantUseDate(Date,ProgramId) 
                                                                VALUES(@Date,@ProgramId) ";

            string sqlProgramInclude = @"INSERT INTO ProgramInclude(ProgramId,IsInclude,Text) 
                                                                VALUES(@ProgramId,@IsInclude,@Text) ";

            string sqlAboutProgramOption = @"INSERT INTO AboutProgramOptions(ProductId,IconClass,Context) 
                                                                      VALUES(@ProductId,@IconClass,@Context)";

            ;
            //假資料-------------------------------
            //product.supplier = new DTOModels.ProductDTO.Supplier();
            //product.supplier.Address = "123123";
            //product.supplier.CompanyName = "123123";
            //product.supplier.ContactName = "123123";
            //product.supplier.ContactTitle = "123123";
            //product.supplier.Phon = "123123";
            //product.supplier.City = 1;
            //假資料-------------------------------
            //product.CategoryList = new List<int> { 1, 2, 3 };
            //product.location = new location
            //{   
            //    locationTitle = "桃禧航空城酒店-新館",
            //    Latitude = "121.1728099",
            //    locationAddress = "桃園市大園區園航路28號",
            //    locationtext = null ,
            //    Longitude = "25.0607882",
            //    Type = 0
            //};
            //假資料-------------------------------
            var supplier = new Today.Model.Models.Supplier();
            supplier.Address = product.Supplier.Address;
            supplier.CompanyName = product.Supplier.CompanyName;
            supplier.ContactName = product.Supplier.ContactName;
            supplier.ContactTitle = product.Supplier.ContactTitle;
            supplier.Phone = product.Supplier.Phon;
            supplier.CityId = product.Supplier.City;
            ;
            _repo.Execute(sqlsupplier, supplier);
            ;
            string sqlsupplierId = $"select * from Supplier where Address = '{supplier.Address}' ";
            var supilerId = _repo.Query<Today.Model.Models.Supplier>(sqlsupplierId);

            //假資料-------------------------------
            //product.HowUse = "asasdasd";
            //product.ShoppingNotice = "asasdasd";
            //product.ProductName = "asasdasd";
            //product.City = 1;
            //product.ProductText = "asasdasd";
            //product.CancellationPolicy = "asasdasd";
            //product.Isdeleted = true;
            //假資料-------------------------------
            ;
            var CreateProduct = new Today.Model.Models.Product();
            //ProductName = product.ProductName,
            //CityId = product.City,
            //HowUse = product.HowUse,
            //Illustrate = product.ProductText,
            //ShoppingNotice = product.ShoppingNotice,
            //CancellationPolicy = product.CancellationPolicy,
            //Isdeleted = product.Isdeleted,
            //SupplierId = supilerId.First().SupplierId,
            CreateProduct.ProductName = product.ProductName;
            CreateProduct.CityId = product.City;
            CreateProduct.HowUse = product.HowUse;
            CreateProduct.Illustrate = product.ProductText;
            CreateProduct.ShoppingNotice = product.ShoppingNotice;
            CreateProduct.CancellationPolicy = product.CancellationPolicy;
            CreateProduct.Isdeleted = product.Isdeleted;
            CreateProduct.SupplierId = supilerId.First().SupplierId;
            ;
            _repo.Execute(sqlProduct, CreateProduct);
            ;
            string sqlProductId = $"select *  from Product where ProductName = '{CreateProduct.ProductName}' ";
            var ProductId = _repo.Query<Today.Model.Models.Product>(sqlProductId);
            string sqlCreateLocation = @"INSERT INTO Location(Address,Latitude,Longitude,ProductId,Text,Type,Title) 
                                                    VALUES(@Address,@Latitude,@Longitude,@ProductId,@Text,@Type,@Title)";

            var Createlocation = new Location();
            Createlocation.Address = product.location.locationAddress;
            Createlocation.Latitude = product.location.Latitude;
            Createlocation.Longitude = product.location.Longitude;
            Createlocation.ProductId = ProductId.First().ProductId;
            Createlocation.Text = product.location.locationtext;
            Createlocation.Type = (int)product.location.Type;
            Createlocation.Title = product.location.locationTitle;
            ;
            _repo.Execute(sqlCreateLocation, Createlocation);
            ;
            string sqlCreateCategory = @"INSERT INTO ProductCategory(ProductId,CategoryId) 
                                                                VALUES(@ProductId,@CategoryId)";


            var categoryList=product.CategoryList.Select(c => new ProductCategory
            {
                ProductId = ProductId.First().ProductId,
                CategoryId = c,
            });
            ;
            _repo.Execute(sqlCreateCategory, categoryList);
            ;

            //假資料------------------
            //product.PhtotList = new List<Photo>()
            //{
            //    new Photo(){ Sort= 1,PhotoUrl = "https://image.kkday.com/v2/image/get/h_650%2Cc_fit/s1.kkday.com/product_123611/20211020070942_g6k7U/jpg"},
            //    new Photo(){ Sort= 2,PhotoUrl = "https://image.kkday.com/v2/image/get/h_650%2Cc_fit/s1.kkday.com/product_123611/20211020070942_g6k7U/jpg"},
            //    new Photo(){ Sort= 3,PhotoUrl = "https://image.kkday.com/v2/image/get/h_650%2Cc_fit/s1.kkday.com/product_123611/20211020070942_g6k7U/jpg"},
            //    new Photo(){ Sort= 4,PhotoUrl = "https://image.kkday.com/v2/image/get/h_650%2Cc_fit/s1.kkday.com/product_123611/20211020070942_g6k7U/jpg"}
            //};
            //假資料------------------

            var CreatePhoto = product.PhtotList.Select((p ,Index)=> new Today.Model.Models.ProductPhoto
            {
                ProductId = ProductId.First().ProductId,
                Path = p,
                Sort = Index,
            });
            ;
            _repo.Execute(sqlPhoto, CreatePhoto);
            var CreateProram = new Today.Model.Models.Program();
            CreateProram.ProductId = ProductId.First().ProductId;
            ////假資料----------------------
            //product.ProgarmList = new List<Progarm>()
            //{
            //    new Progarm(){PrgarmText = "1we12e" , PrgramName = "145723qwe",ProgarmIsdeleted = true},
            //    new Progarm(){PrgarmText = "123123" , PrgramName = "werwqerw",ProgarmIsdeleted = true},
            //    new Progarm(){PrgarmText = "4567456" , PrgramName = "qwerqweq",ProgarmIsdeleted = true},
            //};
            ////假資料----------------------
            var CreateProgarmList = product.ProgarmList.Select(pg => new Today.Model.Models.Program
            {
                ProductId = ProductId.First().ProductId,
                Context = pg.PrgarmText,
                Title = pg.PrgramName,
                Isdeleted = false,

            });
            ;
            _repo.Execute(sqlProgarm, CreateProgarmList);

            string sqlProgarmList = $"select * from Program where ProductId = {ProductId.First().ProductId}";
            
            var ProgarmList = _repo.Query<Today.Model.Models.Program>(sqlProgarmList);
            ;
            ////假資料----------------------
            //product.AboutProgramOptionList = new List<DTOModels.ProductDTO.AboutProgramOption>()
            //{
            //    new DTOModels.ProductDTO.AboutProgramOption{Context = "2個工作日(不含例假日)內確認", IconClass = "<i class=\"icons icon - check\"></i>"},
            //    new DTOModels.ProductDTO.AboutProgramOption{Context = "立即發送憑證", IconClass = "<i class=\"icons icon-paper-plane\"></i>"},
            //    new DTOModels.ProductDTO.AboutProgramOption{Context = "4個工作日(不含例假日)內確認", IconClass = "<i class=\"icons icon-check\"></i>"},
            //};
            ////假資料----------------------
            var CreateAboutProgramOptionList = product.AboutProgramOptionList.Select(apo =>new Today.Model.Models.AboutProgramOption
            {
                ProductId = ProductId.First().ProductId,
                IconClass = apo.IconClass,
                Context = apo.Context
            });
            ;
            _repo.Execute(sqlAboutProgramOption, CreateAboutProgramOptionList);

            string sqlAboutProgramOptionList = @$"select * from AboutProgramOptions where ProductId = {ProductId.First().ProductId}";

            var AboutProgramOptionList = _repo.Query<Today.Model.Models.AboutProgramOption>(sqlAboutProgramOptionList);

            var aboutProgram = new List<AboutProgram>();
            var CreateProgramSpecification = new  List<Today.Model.Models.ProgramSpecification>();
            var CreateProgramInclude = new List<Today.Model.Models.ProgramInclude>();
            var CreateProgramCantUseDate = new List<ProgramCantUseDate>();
            product.ProgarmList.ForEach(pg => {
                //假資料----------------------
                //pg.ProgramSpecificationList = new  List<DTOModels.ProductDTO.ProgramSpecification>
                //{
                //    new DTOModels.ProductDTO.ProgramSpecification{
                //        UnitText = "間",
                //        Itemtext = "雙人房",
                //        OriginalUnitPrice = 6000000,
                //        PorgarmUnitPrice = 54444444 ,
                //        Status = 0,
                //        IsScreening = false,
                //    },
                //    new DTOModels.ProductDTO.ProgramSpecification{
                //        UnitText = "間", 
                //        Itemtext = "四人房",
                //        OriginalUnitPrice = 2000, 
                //        PorgarmUnitPrice = 54441444 , 
                //        Status = 0,
                //        IsScreening= false
                //    },
                //};
                //pg.DateList = new List<Date>
                //{
                //    new Date{CantuseDate = "2022-09-09"},
                //    new Date{CantuseDate = "2022-09-01"},
                //    new Date{CantuseDate = "2022-09-02"},
                //    new Date{CantuseDate = "2022-09-03"},
                //};

                //pg.ProgramIncludeList = new List<DTOModels.ProductDTO.ProgramInclude>
                //{
                //    new DTOModels.ProductDTO.ProgramInclude{Inciudetext = "個人消費",IsInclude = false},
                //    new DTOModels.ProductDTO.ProgramInclude{Inciudetext = "門票",IsInclude = false},
                //    new DTOModels.ProductDTO.ProgramInclude{Inciudetext = "其他未提及費用",IsInclude = false},
                //    new DTOModels.ProductDTO.ProgramInclude{Inciudetext = "免費嬰兒備品借用乙組，依房間數量提供（如有需求請事先預約），如需增加備品數量，將依照現場公告之收費標準酌收費用",IsInclude = true},
                //    new DTOModels.ProductDTO.ProgramInclude{Inciudetext = "住宿",IsInclude = true},
                //    new DTOModels.ProductDTO.ProgramInclude{Inciudetext = "早餐 2 客",IsInclude = true},
                //};
                ////假資料----------------------

                var ProgarmId = ProgarmList.First(sqlpg => sqlpg.Title == pg.PrgramName).ProgramId;
                AboutProgramOptionList.ToList().ForEach(ap => {
                    var a = new AboutProgram
                    {
                        ProgramId = ProgarmId,
                        AboutProgramOptionsId = ap.AboutProgramOptionsId
                    };
                    aboutProgram.Add(a);
                });


                pg.ProgramIncludeList.ForEach(pc =>
                {
                    var pgc = new Today.Model.Models.ProgramInclude
                    {
                        ProgramId = ProgarmId,
                        IsInclude = pc.IsInclude,
                        Text = pc.Inciudetext,
                    };
                    CreateProgramInclude.Add(pgc);
                });


                pg.ProgramSpecificationList.ForEach(sp => {
                    var pgsp = new Today.Model.Models.ProgramSpecification
                    {
                        IsScreening = sp.IsScreening,
                        ProgramId = ProgarmId,
                        Itemtext = sp.Itemtext,
                        UnitPrice = sp.PorgarmUnitPrice,
                        OriginalUnitPrice = sp.OriginalUnitPrice,
                        UnitText = sp.UnitText, 
                        Status = 1,
                    };
                    CreateProgramSpecification.Add(pgsp);
                });




                pg.DateList.ForEach(pg =>
                {
                    var d = new ProgramCantUseDate
                    {
                        Date = DateTime.Parse(pg),
                        ProgramId = ProgarmId,
                    };
                CreateProgramCantUseDate.Add(d);
                });



            });
            ;
            _repo.Execute(sqlaboutProgram, aboutProgram);
            ;
            _repo.Execute(sqlCreateProgramSpecification, CreateProgramSpecification);
            ;
            _repo.Execute(sqlProgramCantUseDate, CreateProgramCantUseDate);
            ;
            _repo.Execute(sqlProgramInclude, CreateProgramInclude);
            ;

        }

        public GetVM get()
        {
            var result = new GetVM();

            string sqlCity = @"select * from City";
            string sqlCategory = @"select * from Category";
            string sqlTag = @"  select * from Tag";



            var CityList = _repo.Query<Today.Model.Models.City>(sqlCity);
            var CategoryList = _repo.Query<Today.Model.Models.Category>(sqlCategory);
            var TagList = _repo.Query<Today.Model.Models.Tag>(sqlTag);

            var categorys = CategoryList.Select(c => new TodayMVC.Admin.ViewModels.Catecory { CatecoryId  = c.CategoryId, CatecoryName = c.CategoryName}).ToList();
            var Citys  =  CityList.Select(c =>  new TodayMVC.Admin.ViewModels.City { CityId = c.CityId, CityName = c.CityName  }).ToList();
            var Tags = TagList.Select(t => new TodayMVC.Admin.ViewModels.Tag { Tagid = t.TagId , Tagname = t.TagText}).ToList();
            result.cityList = Citys;
            result.catecorieList = categorys;
            result.TagList = Tags;

            return result;
        }
    }
}
