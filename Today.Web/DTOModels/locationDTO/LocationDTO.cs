using System;
using System.Collections.Generic;
using System.Linq;

namespace Today.Web.DTOModels.locationDTO
{
    public class LocationDTO
    {
        public class ProductLocationDTO
        {
            public int LocationID { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
        }
        public class ProductNameDTO
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int CityId { get; set; }
        }
        public class ProductCard
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int CityId { get; set; }
            public IEnumerable<CityDTO> CityDTOs { get; set; }
            public IEnumerable<ProductPictureDTO> ProductPictureDTOs { get; set; }
            public IEnumerable<Program> Programs { get; set; }
            public IEnumerable<ProgramSpecification> ProgramSpecifications { get; set; }
            public IEnumerable<ProgramCantUseDate> CantUseDates { get; set; }
        }
        public class CityDTO
        {
            public int CityId { get; set; }
            public string CityName { get; set; }
            public bool IsIsland { get; set; }
        }
        public class ProductPictureDTO
        {
            public int PhotoId { get; set; }
            public string Path { get; set; }
            public int ProductId { get; set; }
            public int Sort { get; set; }
        }
        public class Program
        {
            public int ProgramId { get; set; }
            public int ProductId { get; set; }

        }
        public class ProgramSpecification
        {
            public int ProgramId { get; set; }
            public int SpecificationId { get; set; }
            public decimal OriginalUnitPrice { get; set; }
            public decimal UnitPrice { get; set; }
        }
        public class ProgramCantUseDate
        {
            public int ProgramId { get; set; }
            public int ProgramDateId { get; set; }
            public DateTime Date { get; set; }
        }
    }
}
