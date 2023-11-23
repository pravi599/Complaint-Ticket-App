using ComplaintTicketApplication.Models.DTOs;
using ComplaintTicketApplication.Models.DTOs;
using System.Collections.Generic;

namespace ComplaintTicketApplication.Interfaces
{
    public interface ICommentService
    {
        bool AddComment(CommentDTO commentDTO);
        bool RemoveComment(int commentId);
        bool UpdateComment(CommentDTO commentDTO);
        CommentDTO GetCommentById(int commentId);
        IEnumerable<CommentDTO> GetCommentsByComplaintId(int complaintId);
    }
}