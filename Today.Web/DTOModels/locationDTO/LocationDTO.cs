namespace Today.Web.DTOModels.locationDTO
{
    public class LocationDTO
    {
        public class ProductLocationDTO
        {
            public int locationID { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
        }
        public class ProductNameDTO
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
        }
        public class ProductPictureDTO
        {
            public int PhotoId { get; set; }
            public string Path { get; set; }
            public int ProductId { get; set; }
            public int Sort { get; set; }
        }

    }
}
