using System.Collections.Generic;
using Today.Model.Models;

namespace Today.Web.ViewModels
{
    //public class CityVM
    //{
    //    public int Id { get; set; }
    //    public string CityName { get; set; }
    //    public string CityIntrod { get; set; }
    //    public string CityImage { get; set; }
    //}
    public class CityVM
    {
        public List<City> city{ get; set; }

    }
    public class city
    {
        public int id { get; set; }
        public string CityName { get; set; }
        public string CityIntrod { get; set; }
        public string CityImage { get; set; }
    }

}
