using System.Collections.Generic;
using ComplaintTicketApp.Models;
using ComplaintTicketApp.Models.DTOs;

namespace ComplaintTicketApp.Interfaces
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