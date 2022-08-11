using System;
using System.Collections.Generic;

namespace Today.Web.DTOModels
{
    public class ShopCartMemberDTO
    {
        public class ShopCartMemberRequestDTO
        {
            public int MemberId { get; set; }
            public int SpecificationId { get; set; }
        }
        public class ShopCartMemberResponseDTO
        {
            public List<ShopCartCard> ShopCartCards { get; set; }
            //public List<RecommendCartCard> RecommendCartCards { get; set; }
            public class ShopCartCard
            {

                public int ShopCartId { get; set; }
                public int ProgramId { get; set; }
                public int ProductId { get; set; }
                public int MemberId { get; set; }
                public bool Favorite { get; set; }
                public string ProductName { get; set; }
                public string ProgramTitle { get; set; }
                public DateTime DepartureDate { get; set; }
                public int? SpecificationId { get; set; }
                public int Quantity { get; set; }
                public DateTime JoinTime { get; set; }
                public string ScreenTime { get; set; }  
                public int ScreenId { get; set; }  
                public string ProductPhoto { get; set; }
                public bool Productdeleted { get; set; }
                public bool Programdeleted { get; set; }
                public decimal UnitPrice { get; set; }
                public string UnitText { get; set; }
            }
        }

            


        //public class RecommendCartCard
        //{
        //    public int ProductId { get; set; }
        //    public string ProductPhoto { get; set; }
        //    public string CityName { get; set; }
        //    public string ProductName { get; set; }
        //    public int RatingStar { get; set; }
        //    public string TagText { get; set; }

        //    public int OrderDetialsId { get; set; }
        //    public decimal UnitPrice { get; set; }

        //}
    }

    
}
