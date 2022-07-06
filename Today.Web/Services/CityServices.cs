using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Today.Model.Models;
using Today.Web.ViewModels;

namespace Today.Web.Services
{

    public class CityServices
    {
        private readonly TodayDBContext _context;
        public CityServices()
        {
            _context = new TodayDBContext();
        }
        public async Task<List<CityVM>> CityAllPages()
        {
            var result = await _context.Cities.ToListAsync();
            var citypages = (from cp in result
                             where cp.CityId == 12
                             select cp).Select(c => new CityVM { Id = c.CityId, CityName = c.CityName, CityImage = c.CityImage, CityIntrod = c.CityIntrod }).ToList();
            return citypages.ToList();
        }
    }
}
