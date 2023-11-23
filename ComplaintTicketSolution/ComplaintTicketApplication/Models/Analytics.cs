using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketApplication.Models
{
    public class Analytics
    {
        [Key]
        public int AnalyticsId { get; set; }

        [Required]
        public string ReportName { get; set; }

        // Foreign key to Organization
        public int OrganizationId { get; set; }

        // Navigation property to Organization
        public Organization Organization { get; set; }
    }
}


