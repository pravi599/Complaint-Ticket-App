// IAnalyticsService
using ComplaintTicketApp.Models.DTOs;

namespace ComplaintTicketApp.Interfaces
{
    public interface IAnalyticsService
    {
        AnalyticsDTO GetAnalyticsById(int analyticsId);
        IEnumerable<AnalyticsDTO> GetAllAnalytics();
    }
}
