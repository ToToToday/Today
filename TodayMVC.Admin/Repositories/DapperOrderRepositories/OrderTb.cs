using System;

namespace TodayMVC.Admin.Repositories.DapperOrderRepositories
{
    public class OrderTb
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int Statu { get; set; }


    }
}
