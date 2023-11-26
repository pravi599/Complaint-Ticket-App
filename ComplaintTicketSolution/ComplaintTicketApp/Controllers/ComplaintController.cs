using Microsoft.AspNetCore.Mvc;
using ComplaintTicketApp.Interfaces;
using ComplaintTicketApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using ComplaintTicketApp.Exceptions;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class ComplaintController : ControllerBase
{
    private readonly IComplaintService _complaintService;
    private readonly ILogger<ComplaintController> _logger;

    public ComplaintController(IComplaintService complaintService, ILogger<ComplaintController> logger)
    {
        _complaintService = complaintService;
        _logger = logger;
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    public IActionResult AddComplaint([FromBody] ComplaintDTO complaintDTO)
    {
        _logger.LogInformation("Adding a complaint.");

        try
        {
            var result = _complaintService.Add(complaintDTO);
            return Ok(complaintDTO);
        }
        catch (OrganizationNotFoundException ex)
        {
            _logger.LogError(ex, "Failed to add complaint due to organization not found.");
            return NotFound($"Failed to add complaint. {ex.Message}");
        }
        catch (ComplaintOperationException ex)
        {
            _logger.LogError(ex, "Error adding complaint.");
            return BadRequest("Failed to add complaint");
        }
    }

    [Authorize(Roles = "User")]
    [HttpDelete("{complaintId}")]
    public ActionResult RemoveComplaint(int complaintId)
    {
        _logger.LogInformation($"Removing complaint with ID {complaintId}.");

        try
        {
            var success = _complaintService.Remove(complaintId);

            if (success)
            {
                _logger.LogInformation("Complaint deleted");
                return Ok("Complaint deleted successfully");
            }

            return NotFound("Complaint not found");
        }
        catch (ComplaintNotFoundException ex)
        {
            _logger.LogError(ex, $"Failed to remove complaint.");
            return NotFound($"Failed to remove complaint. {ex.Message}");
        }
        catch (ComplaintOperationException ex)
        {
            _logger.LogError(ex, "Error removing complaint.");
            return StatusCode(500, "Internal server error");
        }
    }

    [Authorize(Roles = "User")]
    [HttpPut]
    public IActionResult UpdateComplaint([FromBody] ComplaintDTO complaintDTO)
    {
        _logger.LogInformation($"Updating complaint with ID {complaintDTO.ComplaintId}.");

        try
        {
            var result = _complaintService.Update(complaintDTO);

            if (result != null)
            {
                _logger.LogInformation("Complaint updated successfully");
                return Ok(result);
            }

            return NotFound("Complaint not found");
        }
        catch (ComplaintNotFoundException ex)
        {
            _logger.LogError(ex, $"Failed to update complaint.");
            return NotFound($"Failed to update complaint. {ex.Message}");
        }
        catch (ComplaintOperationException ex)
        {
            _logger.LogError(ex, "Error updating complaint.");
            return StatusCode(500, "Internal server error");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{complaintId}")]
    public IActionResult GetComplaintById(int complaintId)
    {
        _logger.LogInformation($"Getting complaint with ID {complaintId}.");

        try
        {
            var complaintDTO = _complaintService.GetComplaintById(complaintId);

            if (complaintDTO != null)
            {
                _logger.LogInformation("Complaint listed with given ID");
                return Ok(complaintDTO);
            }

            return NotFound("Complaint not found");
        }
        catch (ComplaintNotFoundException ex)
        {
            _logger.LogError(ex, $"Failed to get complaint by ID.");
            return NotFound($"Failed to get complaint by ID. {ex.Message}");
        }
        catch (ComplaintOperationException ex)
        {
            _logger.LogError(ex, "Error getting complaint by ID.");
            return StatusCode(500, "Internal server error");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult GetAllComplaints()
    {
        _logger.LogInformation("Getting all complaints.");

        try
        {
            var complaintDTOs = _complaintService.GetAllComplaints();
            _logger.LogInformation("All complaints listed");
            return Ok(complaintDTOs);
        }
        catch (ComplaintOperationException ex)
        {
            _logger.LogError(ex, "Error getting all complaints.");
            return StatusCode(500, "Internal server error");
        }
    }
}
