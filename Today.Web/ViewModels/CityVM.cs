﻿using System.Collections.Generic;
using Today.Model.Models;

namespace Today.Web.ViewModels
{
   
    public class CityVM
    {
        public CityInfo CityPage{ get; set; }

        public class CityInfo
        {
            public int Id { get; set; }
            public string CityName { get; set; }
            public string CityIntrod { get; set; }
            public string CityImage { get; set; }
        }
        
        
    }

}
