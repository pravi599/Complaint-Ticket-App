using ComplaintTicketApplication.Models;
using System.ComponentModel.DataAnnotations;
public class Complaint
{
    [Key]
    public int ComplaintId { get; set; }
    [Required]
    public string Category { get; set; }
    [Required]
    public string Description { get; set; }
    // Additional complaint properties
    [Required]
    public string OrganizationName { get; set; }
    public string? FilePath { get; set; }
    public string Username { get; set; } // Foreign key
    public User User { get; set; } // Navigation property
    public Priority Priority { get; set; }
    public Tracking Tracking { get; set; }
    public ICollection<Comment> Comments { get; set; }

    // Foreign key to Organization
    public int OrganizationId { get; set; }

    // Navigation property to Organization
    public Organization Organization { get; set; }

}