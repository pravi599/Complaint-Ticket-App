using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketApp.Models
{
    public class Attachment
    {
        [Key]
        public int AttachmentId { get; set; }

        [Required]
        public string FileName { get; set; }

        // Additional attachment properties
        public string FilePath { get; set; }

        // Relationships
        public int ComplaintId { get; set; } // Foreign key
        public Complaint Complaint { get; set; } // Navigation property
    }

}
