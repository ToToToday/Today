﻿using System;

namespace Today.Web.DTOModels.ShopCartDTO
{
    public class CreateShopCartInputDTO
    {
        //public int ShoppingCartId { get; set; }
        public int MemberId { get; set; }
        public int SpecificationId { get; set; }
        public DateTime DepartureDate { get; set; }
        public int Quantity { get; set; }
        public int ScreeningId { get; set; }
        public string ProgramTitle { get; set; }
        public string Path { get; set; }
        public string ProductName { get; set; }
        //public DateTime JoinTime { get; set; }
    }
    public class CreateShopCartOutputDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
