using ComplaintTicketApp.Models.DTOs;
using System.Collections.Generic;

namespace ComplaintTicketApp.Interfaces
{
    public interface IComplaintService
    {
        bool Add(ComplaintDTO complaintDTO);
        bool Remove(int complaintId); // Use unique identifier for removal
        ComplaintDTO Update(ComplaintDTO complaintDTO);
        ComplaintDTO GetComplaintById(int complaintId);
        IEnumerable<ComplaintDTO> GetAllComplaints();
        // Add more methods as needed based on your application requirements
        // ComplaintDTO UpdateTrackingStatus(int complaintId, string trackingStatus);
    }
}