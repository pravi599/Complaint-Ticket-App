namespace ComplaintTicketApp.Models.DTOs
{
    public class TrackingDTO
    {
        public int TrackingId { get; set; }
        public int ComplaintId { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Status { get; set; }
    }

}
