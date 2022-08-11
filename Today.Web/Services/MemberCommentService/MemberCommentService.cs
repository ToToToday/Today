using System;
using System.Collections.Generic;
using System.Linq;
using Today.Model.Models;
using Today.Model.Repositories;
using Today.Web.CommonEnum;
using Today.Web.DTOModels;
using Today.Web.Helper;
using Today.Web.ViewModels;

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
            var member = _repo.GetAll<Member>().Where(m => m.MemberId == Id.MemberId).Select(m => m.MemberName).First();
            var Order = _repo.GetAll<Order>().Where(m => m.MemberId == Id.MemberId&&m.Status==2);
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
            ;
            var ProgramSpecification = _repo.GetAll<ProgramSpecification>().ToList();
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
            var comment2 = _repo.GetAll<Today.Model.Models.Comment>().Where(cm => cm.MemberId == Id.MemberId).ToList();
            var result = new DTOModels.MemberCommentDTO.MemberCommentResponseDTO
            {
                MemberName = member,
                OrderInfo = new DTOModels.MemberCommentDTO.Order
                {
                    OrderDtailList = Product.Select(p => new DTOModels.MemberCommentDTO.OrderDetailCard
                    {
                        ProductName = p.ProductName,
                        Title = p.Title,
                        UnitPrice = p.UnitPrice,
                        Path = _repo.GetAll<Today.Model.Models.ProductPhoto>().First(pp => pp.ProductId == p.ProductId).Path.ToString(),
                        OrderId = p.OrderId,
                        DepartureDate = p.DepartureDate,
                        comment = new MemberCommentDTO.Comment
                        {
                            OrderDetailId = p.OrderDetailsId,
                            ProductId = p.ProductId,

                        },
                    }).ToList()
                }
            };
            result.OrderInfo.OrderDtailList.ForEach(od =>
            comment2
            .Where(cm => cm.OrderDetailsId == od.comment.OrderDetailId)
            .ToList()
            .ForEach(cm =>
            {
                od.comment.CommentText = cm.CommentText;
                od.comment.CommentTitle = cm.CommentTitle;
                od.comment.Partnertype = (CommonEnum.AllEnum.PartnerType)cm.PartnerType;
                od.comment.RatingStar = cm.RatingStar;
                od.comment.CommentDate = cm.CommentDate;
            }));
            return result;
        }

        public string Create(ViewModels.Comment a)
        {
            var typeList = Enum.GetNames(typeof(AllEnum.PartnerType));
            var typeName = typeList.First(x => x == a.PartnerType.ToString());
            var entity = new Model.Models.Comment
            {
                RatingStar = a.RatingStar,
                PartnerType = (int)Enum.Parse(typeof(AllEnum.PartnerType), typeName),
                CommentTitle = a.CommentTitle,
                CommentText = a.CommentText,
                CommentDate = DateTime.Now,
                OrderDetailsId = a.OrderDetailId,
                ProductId = a.ProductId,
                MemberId = a.MemberId
            };
            try
            {
                _repo.Create(entity);
                _repo.SavaChanges();
                return "新增成功";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Update(ViewModels.Comment a)
        {

            var typeList = Enum.GetNames(typeof(AllEnum.PartnerType));
            var typeName = typeList.First(x => x == a.PartnerType.ToString());
            var comment = _repo.GetAll<Model.Models.Comment>().First(c => c.OrderDetailsId == a.OrderDetailId);
            try
            {
            comment.PartnerType = (int)Enum.Parse(typeof(AllEnum.PartnerType), typeName);
            comment.CommentTitle = a.CommentTitle;
            comment.RatingStar = a.RatingStar;
            comment.CommentText = a.CommentText;
                comment.CommentDate = DateTime.Now;
                _repo.Update(comment);
                _repo.SavaChanges();
                return "修改成功";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

