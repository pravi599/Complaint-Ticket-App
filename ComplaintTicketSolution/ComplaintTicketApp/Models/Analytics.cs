using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketApp.Models
{
    public class Analytics
    {
        [Key]
        public int AnalyticsId { get; set; }

        [Required]
        public string ReportName { get; set; }

        // Additional properties for analytics
        // For example, DateRange, NumberOfComplaints, etc.

        // Foreign key to Organization
        public int OrganizationId { get; set; }

        // Navigation property to Organization
        public Organization Organization { get; set; }
    }
}
