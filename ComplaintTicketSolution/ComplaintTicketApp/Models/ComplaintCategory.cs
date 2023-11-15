namespace ComplaintTicketApp.Models
{
    public class ComplaintCategory
    {
        public int ComplaintCategoryId { get; set; }
        public string CategoryName { get; set; }
        // Other properties if needed

        // Relationship with Complaint
        public ICollection<Complaint> Complaints { get; set; }
    }
}
