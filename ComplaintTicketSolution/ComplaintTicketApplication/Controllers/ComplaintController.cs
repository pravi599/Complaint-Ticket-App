using Microsoft.AspNetCore.Mvc;
using ComplaintTicketApp.Interfaces;
using ComplaintTicketApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using ComplaintTicketApp.Exceptions;

namespace ComplaintTicketApp.Controllers
{
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
            try
            {
                var result = _complaintService.Add(complaintDTO);

                if (result)
                {
                    _logger.LogInformation("Complaint added successfully");
                    return Ok(result);
                }

                return BadRequest("Failed to add complaint");
            }
            catch (OrganizationNotFoundException ex)
            {
                return NotFound($"Failed to add complaint.{ex.Message}");
            }
            catch (ComplaintOperationException)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Roles = "User")]
        [HttpDelete("{complaintId}")]
        public ActionResult RemoveComplaint(int complaintId)
        {
            try
            {
                var success = _complaintService.Remove(complaintId);

                if (success)
                {
                    _logger.LogInformation("Complaint deleted");
                    return Ok("Complaint deleted successfully");
                }
                else
                {
                    return NotFound("Complaint not found");
                }
            }
            catch (ComplaintNotFoundException ex)
            {
                return NotFound($"Failed to remove complaint.{ex.Message}");
            }
            catch (ComplaintOperationException)
            {
                return StatusCode(500, "Internal server error");
            }
        }



        [Authorize(Roles = "User")]

        [HttpPut]
        public IActionResult UpdateComplaint([FromBody] ComplaintDTO complaintDTO)
        {
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
                return NotFound($"Failed to remove complaint.{ex.Message}");
            }
            catch (ComplaintOperationException)
            {
                return StatusCode(500, "Internal server error");
            }

        }
        [Authorize(Roles = "Admin")]

        [HttpGet("{complaintId}")]
        public IActionResult GetComplaintById(int complaintId)
        {
            try
            {
                var complaintDTO = _complaintService.GetComplaintById(complaintId);

                if (complaintDTO != null)
                {
                    _logger.LogInformation("Complaint Listed with given Id");
                    return Ok(complaintDTO);
                }

                return NotFound("Complaint not found");
            }
            catch (ComplaintNotFoundException ex)
            {
                return NotFound($"Failed to remove complaint.{ex.Message}");
            }
            catch (ComplaintOperationException)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllComplaints()
        {
            try
            {
                var complaintDTOs = _complaintService.GetAllComplaints();
                _logger.LogInformation("All Complaints Listed");
                return Ok(complaintDTOs);
            }
            catch (ComplaintOperationException)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}