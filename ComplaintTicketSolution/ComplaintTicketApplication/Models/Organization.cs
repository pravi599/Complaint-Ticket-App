using ComplaintTicketApplication.Models;
using System.ComponentModel.DataAnnotations;

public class Organization
{
    [Key]
    public int OrganizationId { get; set; }

    [Required]
    public string OrganizationName { get; set; }

    // Additional organization properties
    public string Description { get; set; }
    public string ContactEmail { get; set; }
    public string ContactPhone { get; set; }

    // Navigation property to Analytics
    public ICollection<Analytics> AnalyticsReports { get; set; }
    public ICollection<Complaint> Complaints { get; set; }
}