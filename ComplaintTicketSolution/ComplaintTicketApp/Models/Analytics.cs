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
    }

}
