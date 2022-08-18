using System.Collections.Generic;

namespace TodayMVC.Admin.ViewModels
{
    public class GetVM
    {
        public List<City> cityList { get; set; }
        public List<Catecory> catecorieList { get; set; }
        public List<Tag> TagList { get; set; }
    }
    public class Tag
    {
        public int Tagid { get; set; }
        public string Tagname { get; set; }
    }
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
    public class Catecory
    {
        public int CatecoryId { get; set; }
        public  string CatecoryName { get; set; }
    }
}
