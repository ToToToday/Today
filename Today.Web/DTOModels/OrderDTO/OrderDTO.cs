using System.Collections.Generic;

namespace Today.Web.DTOModels.OrderDTO
{
    public class OrderDTO
    {
        public class ProductInfoRequstDTO
        {
            public int MemeberID { get; set; }
            public IEnumerable<int> ShoppingCradIdList { get; set; }
        }


    }
}
