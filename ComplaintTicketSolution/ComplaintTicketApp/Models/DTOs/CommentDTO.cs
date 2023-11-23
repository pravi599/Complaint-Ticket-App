using System;
using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketApp.Models.DTOs
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        [Required(ErrorMessage = "Commenent can't be empty")]
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ComplaintId { get; set; }
        public string Username { get; set; }
    }
}
