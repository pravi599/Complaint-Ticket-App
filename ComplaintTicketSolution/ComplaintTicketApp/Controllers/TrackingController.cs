using Microsoft.AspNetCore.Mvc;
using ComplaintTicketApp.Interfaces;
using ComplaintTicketApp.Models.DTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using ComplaintTicketApp.Exceptions;

namespace ComplaintTicketApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingService _trackingService;
        private readonly ILogger<TrackingController> _logger;

        public TrackingController(ITrackingService trackingService, ILogger<TrackingController> logger)
        {
            _trackingService = trackingService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult AddTracking([FromBody] TrackingDTO trackingDTO)
        {
            try
            {
                var result = _trackingService.AddTracking(trackingDTO);

                if (result != null)
                {
                    return Ok(result);
                }

                _logger.LogWarning("Failed to add tracking information.");
                return BadRequest("Failed to add tracking information.");
            }
            catch (TrackingAddException ex)
            {
                _logger.LogError(ex, "Error adding tracking information.");
                return StatusCode(500, "Internal server error");
            }
            catch (TrackingOperationException ex)
            {
                _logger.LogError(ex, "Error performing tracking operation.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{trackingId}/status")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult UpdateTrackingStatus(int trackingId, [FromBody] string status)
        {
            try
            {
                var result = _trackingService.UpdateTrackingStatus(trackingId, status);

                if (result != null)
                {
                    return Ok(result);
                }

                _logger.LogWarning("Tracking entry not found.");
                return NotFound("Tracking entry not found.");
            }
            catch (TrackingUpdateException ex)
            {
                _logger.LogError(ex, "Error updating tracking status.");
                return StatusCode(500, "Internal server error");
            }
            catch (TrackingNotFoundException ex)
            {
                _logger.LogError(ex, "Tracking entry not found.");
                return NotFound("Tracking entry not found.");
            }
            catch (TrackingOperationException ex)
            {
                _logger.LogError(ex, "Error performing tracking operation.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{trackingId}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetTrackingById(int trackingId)
        {
            try
            {
                var trackingDTO = _trackingService.GetTrackingById(trackingId);

                if (trackingDTO != null)
                {
                    return Ok(trackingDTO);
                }

                _logger.LogWarning("Tracking entry not found.");
                return NotFound("Tracking entry not found.");
            }
            catch (TrackingNotFoundException ex)
            {
                _logger.LogError(ex, "Tracking entry not found.");
                return NotFound("Tracking entry not found.");
            }
            catch (TrackingOperationException ex)
            {
                _logger.LogError(ex, "Error performing tracking operation.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetAllTrackings()
        {
            try
            {
                var trackingDTOs = _trackingService.GetAllTrackings();

                if (trackingDTOs != null)
                {
                    return Ok(trackingDTOs);
                }

                _logger.LogWarning("No tracking entries found.");
                return NotFound("No tracking entries found.");
            }
            catch (TrackingNotFoundException ex)
            {
                _logger.LogError(ex, "No tracking entries found.");
                return NotFound("No tracking entries found.");
            }
            catch (TrackingOperationException ex)
            {
                _logger.LogError(ex, "Error performing tracking operation.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{trackingId}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult RemoveTracking(int trackingId)
        {
            try
            {
                var result = _trackingService.RemoveTracking(trackingId);

                if (result)
                {
                    return Ok("Tracking entry removed successfully");
                }

                _logger.LogWarning("Tracking entry not found.");
                return NotFound("Tracking entry not found.");
            }
            catch (TrackingNotFoundException ex)
            {
                _logger.LogError(ex, "Tracking entry not found.");
                return NotFound("Tracking entry not found.");
            }
            catch (TrackingOperationException ex)
            {
                _logger.LogError(ex, "Error performing tracking operation.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
