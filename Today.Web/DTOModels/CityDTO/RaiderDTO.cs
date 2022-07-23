namespace Today.Web.DTOModels.CityDTO
{
    public class RaiderDTO
    {
        public class RaiderRequestDTO
        {
            public int RaiderId { get; set; }
        }
        public class RaiderResponseDTO
        {
            public Raider RaiderInfo { get; set; }
        }
        public class Raider
        {
            public int Id { get; set; }
            public int CityId { get; set; }
            public string Title { get; set; }
            public string Subtitle { get; set; }
            public string Video { get; set; }
            public string Text { get; set; }
        }
    }
}
