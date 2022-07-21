using System;
using System.Collections.Generic;
using System.Linq;

namespace Today.Web.ViewModels
{
    public class LocationVM
    {
        public List<ProductPhotoVM> GetProductPhoto { get; set; }
        public List<ProductVM> GetProdocutName { get; set; }
        public List<ProductLocationVM> GetLocation { get; set; }
        public List<CityVM> cityVMs { get; set; }
        public List<Program> programs { get; set; }
        public List<ProgramSpecificationVM> programSpecificationVMs { get; set; }   
        public List<ProgramCantUseDateVM> programCantUseDateVMs{get;set;}
        public List<ProductCard> GetProductCards { get; set; }

        public class ProductLocationVM
        {
            public int Id { get; set; }
            public string longitude { get; set; }   
            public string latitude { get; set; }
            public string Title { get; set; }

        }
        public class ProductVM
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int CityId { get; set; }
        }
        public class ProductPhotoVM
        {
            public int PhotoId { get; set; }
            public int ProductId { get; set; }
            public string PhotoPath { get; set; }
            public int Sort { get; set; }
        }
        public class CityVM
        {   
            public int CityId { get; set; }
            public string CityName { get; set; }
        }
        public class ProgramVM
        {
            public int ProgramId { get; set; }
            public int ProductId { get; set; }

        }
        public class ProgramSpecificationVM
        {
            public int ProgramId { get; set; }
            public int SpecificationId{ get; set; }
            public decimal OriginalUnitPrice { get; set; }
            public decimal UnitPrice { get; set; }
        }
        public class ProgramCantUseDateVM
        {
            public int ProgramId { get; set; }
            public int ProgramDateId { get; set; }
            public DateTime Date { get; set; }
        }
        public class ProductCard
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int CityId { get; set; }
            public IEnumerable<CityVM> CityVM { get; set; }
            public IEnumerable<ProductPhotoVM> ProductPictureVM { get; set; }
            public IEnumerable<ProgramVM> ProgramVM { get; set; }
            public IEnumerable<ProgramSpecificationVM> ProgramSpecificationVM { get; set; }
            public IEnumerable<ProgramCantUseDateVM> CantUseDateVM { get; set; }
        }
    }

}
