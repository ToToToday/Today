using System.Collections.Generic;
using System.Linq;
using Today.Model.Models;
using Today.Model.Repositories;

namespace Today.Web.Services.MemberCommentService
{
    public class MemberCommentService : IMemberCommentService
    {
        private readonly IGenericRepository _repo;
        public MemberCommentService(IGenericRepository repo)
        {
            _repo = repo;
        }
        public DTOModels.MemberCommentDTO.MemberCommentResponseDTO ReadMemberComment(DTOModels.MemberCommentDTO.MemberCommentRequestDTO Id)
        {

            var Order = _repo.GetAll<Order>().Where(m => m.MemberId == Id.MemberId);
            var OrderDetail = _repo.GetAll<OrderDetail>();
            var ods = OrderDetail.Join(Order, od => od.OrderId, o => o.OrderId, (od, o) =>
                new
                {
                    od.UnitPrice,
                    o.OrderId,
                    od.OrderDetailsId,
                    od.SpecificationId,
                    od.DepartureDate
                }).ToList();
            var  ProgramSpecification = _repo.GetAll<ProgramSpecification>().ToList();
            var ProgramS = ProgramSpecification.Join(ods, ps => ps.SpecificationId, od => od.SpecificationId, (ps, ods) =>
            new
            {
                ods.OrderDetailsId,
                ods.SpecificationId,
                ods.OrderId,
                ods.UnitPrice,
                ods.DepartureDate,
                ps.ProgramId
            }).ToList();
            var Program = _repo.GetAll<Today.Model.Models.Program>().ToList();
            var Programs = Program.Join(ProgramS, pg => pg.ProgramId, ps => ps.ProgramId, (pg, ps) =>
            new
            {
                pg.ProductId,
                ps.OrderId,
                pg.Title,
                ps.OrderDetailsId,
                ps.SpecificationId,
                ps.UnitPrice,
                ps.DepartureDate
            }).ToList();

            var Products = _repo.GetAll<Product>().ToList();
            var Product = Products.Join(Programs, p => p.ProductId, pg => pg.ProductId, (p, pg) =>
                new
                {
                    p.ProductName,
                    p.ProductPhotos,
                    p.ProductId,
                    pg.OrderId,
                    pg.Title,
                    pg.OrderDetailsId,
                    pg.SpecificationId,
                    pg.UnitPrice,
                    pg.DepartureDate
                }).ToList();

            var result = new DTOModels.MemberCommentDTO.MemberCommentResponseDTO
            {
                OrderInfo = new DTOModels.MemberCommentDTO.Order
                {
                    OrderDtailList = Product.Select(p => new DTOModels.MemberCommentDTO.OrderDetailCard
                    {
                        ProductName = p.ProductName,
                        Title = p.Title,
                        UnitPrice = p.UnitPrice,
                        Path = _repo.GetAll<Today.Model.Models.ProductPhoto>().First(pp => pp.ProductId == p.ProductId).Path.ToString(),
                        OrderId=p.OrderId,
                        DepartureDate=p.DepartureDate
                    }).ToList()
                }
            };
            
            return result;
        }
        //public DTOModels.MemberCommentDTO.MemberCommentResponseDTO CreateMemberComment(DTOModels.MemberCommentDTO.MemberCommentRequestDTO Id)
        //{
            
        //}
    }
}
