// AnalyticsService
using System;
using System.Collections.Generic;
using System.Linq;
using ComplaintTicketApp.Interfaces;
using ComplaintTicketApp.Models;
using ComplaintTicketApp.Models.DTOs;

namespace ComplaintTicketApp.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IRepository<int, Analytics> _analyticsRepository;

        public AnalyticsService(IRepository<int, Analytics> analyticsRepository)
        {
            _analyticsRepository = analyticsRepository;
        }

        public AnalyticsDTO GetAnalyticsById(int analyticsId)
        {
            try
            {
                var analytics = _analyticsRepository.GetById(analyticsId);

                return analytics != null
                    ? new AnalyticsDTO
                    {
                        AnalyticsId = analytics.AnalyticsId,
                        ReportName = analytics.ReportName,
                        OrganizationId = analytics.OrganizationId
                        // Map other properties as needed
                    }
                    : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting analytics by ID: {ex.Message}");
                return null;
            }
        }

        public IEnumerable<AnalyticsDTO> GetAllAnalytics()
        {
            try
            {
                var analyticsList = _analyticsRepository.GetAll();

                return analyticsList?.Select(analytics =>
                    new AnalyticsDTO
                    {
                        AnalyticsId = analytics.AnalyticsId,
                        ReportName = analytics.ReportName,
                        OrganizationId = analytics.OrganizationId
                        // Map other properties as needed
                    }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all analytics: {ex.Message}");
                return null;
            }
        }
    }
}
