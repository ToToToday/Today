using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.ViewModels;


namespace Today.Web.Services.ClassifyService
{
    public class ClassifyService:IClassifyService
    {
        private readonly IGenericRepository _classifyService; 
        public ClassifyService(IGenericRepository classifyService)
        {
            _classifyService = classifyService;
        }

        public List<ClassifyVM> GetClassifyPages()
        {
            //var result = _classifyService.GetAll<ProductPhoto>().
            //var result = _classifyService.GetAll<ProductPhoto>;
            var classifypages = (from cp in _classifyService.GetAll<ProductPhoto>()
                                 where cp.ProductId <= 10 && cp.Sort == 1
                                 select cp).Select(c => new ClassifyVM { Path = c.Path }).ToList();
            return classifypages;
        }
    }
}
