using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketApp.Models.DTOs
{
    public class OrganizationDTO
    {
        public int OrganizationId { get; set; }
        [Required(ErrorMessage = "Organization name can't be  empty")]
        public string OrganizationName { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Email can't be empty")]
        public string ContactEmail { get; set; }
        [Required(ErrorMessage = "Contact number can't be empty")]
        public string ContactPhone { get; set; }
    }
}
