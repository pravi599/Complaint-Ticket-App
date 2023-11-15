using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketApp.Models
{
    public class Organization
    {
        [Key]
        public int OrganizationId { get; set; }

        [Required]
        public string Name { get; set; }

        // Additional organization properties
        public string Description { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }

        // Navigation properties
        public ICollection<Complaint> Complaints { get; set; }
    }

}
