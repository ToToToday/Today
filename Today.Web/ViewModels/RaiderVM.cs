namespace Today.Web.ViewModels
{
    public class RaiderVM
    {
        public RaiderInfo RaiderPage{ get; set; }
       
        public class RaiderInfo
        {
            public int Id {get; set;}
            public int CityId { get; set; }
            public string Title { get; set; }
            public string Subtitle { get; set; }
            public string Video { get; set; }
            public string Text { get; set; } 
        }
    }

   
}
