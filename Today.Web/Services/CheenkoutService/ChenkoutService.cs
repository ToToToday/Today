using System.Collections.Generic;
using System.Linq;
using Today.Model.Models;
using Today.Model.Repositories;
using static Today.Web.DTOModels.ChenkoutDTO.ChenkoutDTO;

namespace Today.Web.Services.CheenkoutService
{
    public class ChenkoutService : IChenkoutService
    {
        private readonly IGenericRepository _repo;
        public ChenkoutService(IGenericRepository repo)
        {
            _repo = repo;
        }
        public List<MemberInfo> GetOrderMember(ChenkoutRequestDTO request)
        {
            var memberData = _repo.GetAll<Member>();
            var orderData = _repo.GetAll<Order>();
            var cityData = _repo.GetAll<City>();
            
            var memberinfo = from o in orderData
                             join m in memberData on
                             o.MemberId equals m.MemberId
                             join c in cityData on
                             m.CityId equals c.CityId
                             select new { o.OrderId, m.MemberId, m.MemberName, m.Phone, m.Email, c.CityName, };
            
            var member = memberinfo.Where(m => m.OrderId == request.OrderId).Select(minfo => new MemberInfo {
                Name = minfo.MemberName,
                CityName = minfo.CityName,
                PhoneNumber = minfo.Phone,
                Email = minfo.Email
            }).ToList();
            

            return member;
        }
        public List<OrderInfo> GetOrderProduct(ChenkoutRequestDTO request)
        {
            var orderData = _repo.GetAll<Order>();
            var orderDetailData = _repo.GetAll<OrderDetail>();
            var productData = _repo.GetAll<Product>();
            var programData = _repo.GetAll<Model.Models.Program>();
            var SpecificationData = _repo.GetAll<ProgramSpecification>();
            var screnningData = _repo.GetAll<Screening>();
            var photoData = _repo.GetAll<ProductPhoto>();
            if (orderData == null) return null;
            if (orderDetailData == null) return null;
            if (productData == null) return null;
            if (programData == null) return null;
            if (SpecificationData == null) return null;
            if (screnningData == null) return null;
            var orderInfo = from o in orderData
                            join od in orderDetailData on
                            o.OrderId equals od.OrderId
                            join sp in SpecificationData on
                            od.SpecificationId equals sp.SpecificationId
                            //join s in screnningData on
                            //sp.SpecificationId equals s.SpecificationId
                            join pg in programData on
                            sp.ProgramId equals pg.ProgramId
                            join p in productData on
                            pg.ProductId equals p.ProductId
                            join pp in photoData on
                            p.ProductId equals pp.ProductId 
                            where pp.Sort == 1
                            
                            select new { p.ProductName, p.ProductId, pp.Path, pg.Title, sp.UnitPrice, sp.Itemtext, sp.UnitText, od.DepartureDate, od.Quantity, od.Discount, o.OrderId };
            var orderResult = orderInfo.Where(or => or.OrderId == request.OrderId).Select(order => new OrderInfo
            {
                OrderId = order.OrderId,
                ProductName = order.ProductName,
                ProgramTitle = order.Title,
                Photo = order.Path,
                DepartureDate = order.DepartureDate,
                //Screen = order.Time ,
                UnitPrice = order.UnitPrice,
                Itemtext = order.Itemtext,
                UnitText = order.UnitText,
                Quantity = order.Quantity,


            }).ToList();

            return orderResult;
        }
        public List<OrderScreen> GetOrderSceen(ChenkoutRequestDTO request)
        {
            var orderDetailData = _repo.GetAll<OrderDetail>();
            var SpecificationData = _repo.GetAll<ProgramSpecification>();
            var screenData = _repo.GetAll<Screening>();
            var sData = from od in orderDetailData
                        join sp in SpecificationData on od.SpecificationId equals sp.SpecificationId
                        join s in screenData on
                        sp.SpecificationId equals s.SpecificationId
                        select new { od.OrderId, s.ScreeningId, s.Time };
            try
            {
                var screenResult = sData.Where(s => s.OrderId == request.OrderId).Select(s => new OrderScreen
                {
                    Screen = s.Time
                }).ToList();

                return screenResult;
            }
            catch
            {
                return null;
            }
        }

    }
}
