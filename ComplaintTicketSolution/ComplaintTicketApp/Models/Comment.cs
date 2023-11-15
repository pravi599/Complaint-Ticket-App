using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketApp.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        // Relationships
        public int ComplaintId { get; set; } // Foreign key
        public Complaint Complaint { get; set; } // Navigation property

        public string Username { get; set; } // Foreign key
        public User User { get; set; } // Navigation property

    }

}
