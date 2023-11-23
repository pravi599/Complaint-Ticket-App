using System.Collections.Generic;
using ComplaintTicketApplication.Models;
using ComplaintTicketApplication.Models.DTOs;

namespace ComplaintTicketApplication.Interfaces
{
    public interface ITrackingService
    {
        TrackingDTO AddTracking(TrackingDTO trackingDTO);
        TrackingDTO UpdateTrackingStatus(int trackingId, string status);
        TrackingDTO GetTrackingById(int trackingId);
        IList<TrackingDTO> GetAllTrackings();
        bool RemoveTracking(int trackingId);
    }
}