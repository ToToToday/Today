

using Today.Web.ViewModels;

namespace Today.Web.Services.MemberCommentService
{
    public interface IMemberCommentService
    {
        DTOModels.MemberCommentDTO.MemberCommentResponseDTO ReadMemberComment(DTOModels.MemberCommentDTO.MemberCommentRequestDTO request);

        public string Create(Comment a);

        public string Update(Comment a);
    }
}
