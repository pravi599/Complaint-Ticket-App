// AnalyticsController
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ComplaintTicketApplication.Interfaces;
using ComplaintTicketApplication.Models.DTOs;

namespace ComplaintTicketApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("{id}")]
        public ActionResult<AnalyticsDTO> GetAnalyticsById(int id)
        {
            var analyticsDTO = _analyticsService.GetAnalyticsById(id);

            return analyticsDTO != null
                ? Ok(analyticsDTO)
                : NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<AnalyticsDTO>> GetAllAnalytics()
        {
            var analyticsDTOList = _analyticsService.GetAllAnalytics();

            return analyticsDTOList != null && analyticsDTOList.Any()
                ? Ok(analyticsDTOList)
                : NoContent();
        }
    }
}