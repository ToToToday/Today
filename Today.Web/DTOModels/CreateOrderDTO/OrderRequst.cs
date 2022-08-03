using System.Collections.Generic;

namespace Today.Web.DTOModels.CreateOrderDTO
{
    public class CreateOrderRequstDTO
    {

            public int? MemeberID { get; set; }
            public List<int> ShoppingCradIdList { get; set; }

    }
}
