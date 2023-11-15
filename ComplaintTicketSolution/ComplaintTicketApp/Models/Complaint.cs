using ComplaintTicketApp.Models;
using System.ComponentModel.DataAnnotations;

public class Complaint
{
    [Key]
    public int ComplaintId { get; set; }

    [Required]
    public string Category { get; set; }

    public int? ComplaintCategoryId { get; set; }
 
    public ComplaintCategory ComplaintCategory { get; set; }

    [Required]
    public string Description { get; set; }

    // Additional complaint properties
    //public string Organization { get; set; }
    public string Status { get; set; } // Enum may be useful here: Received, InProgress, Resolved

    // Relationships
    public string Username { get; set; } // Foreign key
    public User User { get; set; } // Navigation property

    public int PriorityId { get; set; } // Foreign key
    public Priority Priority { get; set; } // Navigation property

    public ICollection<Comment> Comments { get; set; }
    public ICollection<Attachment> Attachments { get; set; }

}



