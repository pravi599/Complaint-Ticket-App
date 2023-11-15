using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketApp.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }

        // Storing the hashed password as a byte array for added security
        public byte[] Password { get; set; }

        // Storing a key as a byte array 
        public byte[] Key { get; set; }

        // User role (e.g., Admin, SupportAgent, RegularUser)
        public string Role { get; set; }

        // Navigation properties
        public ICollection<Complaint> Complaints { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
