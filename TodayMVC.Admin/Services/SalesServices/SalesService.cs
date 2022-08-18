using System;
using System.Collections.Generic;
using System.Linq;
using Today.Model.Models;
using TodayMVC.Admin.Repositories.DapperSalesRepositories;
using TodayMVC.Admin.ViewModels;
using static TodayMVC.Admin.ViewModels.SalesVM;

namespace TodayMVC.Admin.Services.SalesServices
{
    public class SalesService : ISalesService
    {
        private readonly IDapperSalesRepository _salesrepo;
        public SalesService(IDapperSalesRepository salesrepo)
        {
            _salesrepo = salesrepo;
        }

        public SalesVM GetSalesMonth()
        {
            var result = new SalesVM()
            {
                SalesList = _salesrepo.SelectAll()
                .Select(o => new { OrderDate = o.Order.OrderDate, Quantity = o.Quantity, UnitPrice = o.UnitPrice, SalesAmount = o.Quantity * o.UnitPrice })
                .GroupBy(o => o.OrderDate.Month)
                .Select(o => new SalesInfo { Month = o.Key, SalesAmount = o.Sum(s => s.SalesAmount) }).ToList()
            };

            return result;
        }

        public SalesVM GetSalesSevenDays()
        {
            var saleList = _salesrepo.SelectAll()
                .Select(o => new { OrderDate = o.Order.OrderDate, Quantity = o.Quantity, UnitPrice = o.UnitPrice, SalesAmount = o.Quantity * o.UnitPrice })
                .Where(o => o.OrderDate > DateTime.Now.AddDays(-7) && o.OrderDate < DateTime.Now)
                .GroupBy(o => o.OrderDate.Day);


            var result = new SalesVM()
            {
                WeekSalesList = new List<WeekSalesInfo>()
            };

            foreach (var time in EachDay(DateTime.Now.AddDays(-7) , DateTime.Now))
            {
                var currentSale = saleList.Where(x => x.Key == time.Day);
                if (currentSale.Any())
                {
                    result.WeekSalesList.Add(new WeekSalesInfo
                    {
                        DateTime = time.ToString("yyyy-MM-dd"),
                        SalesAmount = currentSale.Select(x => x.Sum(o => o.SalesAmount)).First()
                    });
                }
                else
                {
                    result.WeekSalesList.Add(new WeekSalesInfo
                    {
                        DateTime = time.ToString("yyyy-MM-dd"),
                        SalesAmount = 0
                    });
                }

            }

            return result;
        }

        private IEnumerable<DateTime> EachDay(DateTime start,DateTime end)
        {
            for (var day = start.Date; day.Date < end.Date ; day = day.AddDays(1))
                yield return day;
        }

    }
}
