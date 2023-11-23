// IAnalyticsService
using ComplaintTicketApplication.Models.DTOs;

namespace ComplaintTicketApplication.Interfaces
{
    public interface IAnalyticsService
    {
        AnalyticsDTO GetAnalyticsById(int analyticsId);
        IEnumerable<AnalyticsDTO> GetAllAnalytics();
    }
}