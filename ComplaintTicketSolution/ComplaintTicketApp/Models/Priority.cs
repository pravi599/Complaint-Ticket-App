using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketApp.Models
{
    public class Priority
    {
        [Key]
        public int PriorityId { get; set; }

        [Required]
        public string Name { get; set; } // e.g., High, Medium, Low

        // Additional properties for priority
        public int EscalationThreshold { get; set; } // The number of days after which a complaint is considered high-priority

        public ICollection<Complaint> Complaints { get; set; } // Navigation property
    }
}
