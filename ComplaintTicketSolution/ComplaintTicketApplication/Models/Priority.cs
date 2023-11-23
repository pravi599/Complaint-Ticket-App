using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComplaintTicketApplication.Models
{
    public class Priority
    {
        [Key]
        public int PriorityId { get; set; }

        [Required]
        public string Name { get; set; } // e.g., High, Medium, Low
        public int ComplaintId { get; set; }

        public Complaint Complaint { get; set; }

        // Additional properties for priority
        public int EscalationThreshold { get; set; } // The number of days after which a complaint is considered high-priority

        //public ICollection<Complaint> Complaints { get; set; } // Navigation property

        public void Escalate()
        {
            if (ShouldEscalate())
            {
                HandleEscalation(); // Custom logic for escalation
            }
        }

        private bool ShouldEscalate()
        {
            // Determine if the escalation threshold has been surpassed
            TimeSpan timeElapsed = DateTime.Now - Complaint.Tracking.UpdateDate;
            return timeElapsed.Days >= EscalationThreshold;
        }

        private void HandleEscalation()
        {
            // Custom logic for escalation
            // For example, you might want to escalate to 'High' or 'Critical'
            // You can adjust this based on your business rules
            // Here, we'll escalate to 'High' as an example
            if (Name == "Low")
            {
                Name = "High";
            }
            else if (Name == "Medium")
            {
                Name = "High";
            }
            // If priority is already 'High' or 'Critical', no further escalation
        }
    }
}