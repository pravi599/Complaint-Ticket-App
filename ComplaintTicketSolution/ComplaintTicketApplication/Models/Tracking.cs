using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketApplication.Models
{
    public class Tracking
    {
        [Key]
        public int TrackingId { get; set; }
        public int ComplaintId { get; set; }
        public string Status { get; set; }
        public DateTime UpdateDate { get; set; }

        // Navigation property for Complaint (many-to-one relationship)
        public Complaint Complaint { get; set; }

        //public ICollection<Complaint> Complaints { get; set; } // Navigation property
    }

}