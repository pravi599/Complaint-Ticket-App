using Microsoft.AspNetCore.Mvc;
using ComplaintTicketApplication.Interfaces;
using ComplaintTicketApplication.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace ComplaintTicketApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintService _complaintService;

        public ComplaintController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult AddComplaint([FromBody] ComplaintDTO complaintDTO)
        {
            var result = _complaintService.Add(complaintDTO);

            if (result)
            {
                return Ok("Complaint added successfully");
            }

            return BadRequest("Failed to add complaint");
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
                    return Ok("Complaint deleted successfully");
                }
                else
                {
                    return NotFound("Complaint not found");
                }
            }
            catch (Exception ex)
            {
                // Log the exception for better error handling
                Console.WriteLine($"Error deleting complaint: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }



        [Authorize(Roles = "User")]

        [HttpPut]
        public IActionResult UpdateComplaint([FromBody] ComplaintDTO complaintDTO)
        {
            var result = _complaintService.Update(complaintDTO);

            if (result != null)
            {

                return Ok("Complaint updated successfully");
            }
            return NotFound("Complaint not found");

        }
        [Authorize(Roles = "Admin")]

        [HttpGet("{complaintId}")]
        public IActionResult GetComplaintById(int complaintId)
        {
            var complaintDTO = _complaintService.GetComplaintById(complaintId);

            if (complaintDTO != null)
            {
                return Ok(complaintDTO);
            }

            return NotFound("Complaint not found");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllComplaints()
        {
            var complaintDTOs = _complaintService.GetAllComplaints();

            return Ok(complaintDTOs);
        }
    }
}