using ComplaintTicketApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketApplication.Models.DTOs
{
    public class ComplaintDTO
    {
        public int ComplaintId { get; set; }
        [Required(ErrorMessage = "Organizationname can't be empty")]
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Username is empty")]
        public string Username { get; set; }
        [Required(ErrorMessage = "FilePath can't be empty")]
        public string FilePath { get; set; }     

    }

}