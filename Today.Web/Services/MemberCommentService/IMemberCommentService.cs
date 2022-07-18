

namespace Today.Web.Services.MemberCommentService
{
    public interface IMemberCommentService
    {
        DTOModels.MemberCommentDTO.MemberCommentResponseDTO ReadMemberComment(DTOModels.MemberCommentDTO.MemberCommentRequestDTO request);
    }
}
