using Microsoft.AspNetCore.Mvc;
using ComplaintTicketApplication.Interfaces;
using ComplaintTicketApplication.Models.DTOs;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace ComplaintTicketApplication.Controllers
{
    /// <summary>
    /// Controller for managing tracking information.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingService _trackingService;

        public TrackingController(ITrackingService trackingService)
        {
            _trackingService = trackingService ?? throw new ArgumentNullException(nameof(trackingService));
        }

        /// <summary>
        /// Adds a new tracking entry.
        /// </summary>
        /// <param name="trackingDTO">The tracking information to add.</param>
        /// <returns>The added tracking information.</returns>
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult AddTracking([FromBody] TrackingDTO trackingDTO)
        {
            var result = _trackingService.AddTracking(trackingDTO);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Failed to add tracking information.");
        }

        /// <summary>
        /// Updates the status of a tracking entry.
        /// </summary>
        /// <param name="trackingId">The ID of the tracking entry to update.</param>
        /// <param name="status">The new status.</param>
        /// <returns>The updated tracking information.</returns>
        [HttpPut("{trackingId}/status")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult UpdateTrackingStatus(int trackingId, [FromBody] string status)
        {
            var result = _trackingService.UpdateTrackingStatus(trackingId, status);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound("Tracking entry not found.");
        }

        /// <summary>
        /// Gets tracking information by ID.
        /// </summary>
        /// <param name="trackingId">The ID of the tracking entry to retrieve.</param>
        /// <returns>The tracking information.</returns>
        [HttpGet("{trackingId}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetTrackingById(int trackingId)
        {
            var trackingDTO = _trackingService.GetTrackingById(trackingId);

            if (trackingDTO != null)
            {
                return Ok(trackingDTO);
            }

            return NotFound("Tracking entry not found.");
        }

        /// <summary>
        /// Gets all tracking entries.
        /// </summary>
        /// <returns>A list of tracking information.</returns>
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetAllTrackings()
        {
            var trackingDTOs = _trackingService.GetAllTrackings();

            if (trackingDTOs != null)
            {
                return Ok(trackingDTOs);
            }

            return NotFound("No tracking entries found.");
        }

        /// <summary>
        /// Removes a tracking entry by ID.
        /// </summary>
        /// <param name="trackingId">The ID of the tracking entry to remove.</param>
        /// <returns>True if the tracking entry was removed successfully; otherwise, false.</returns>
        [HttpDelete("{trackingId}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult RemoveTracking(int trackingId)
        {
            var result = _trackingService.RemoveTracking(trackingId);

            if (result)
            {
                return Ok("Tracking entry removed successfully");
            }

            return NotFound("Tracking entry not found.");
        }
    }
}