using System;
using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketApplication.Models.DTOs
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ComplaintId { get; set; }
        public string Username { get; set; }
    }
}

