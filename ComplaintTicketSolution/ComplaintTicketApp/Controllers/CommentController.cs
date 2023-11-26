using Microsoft.AspNetCore.Mvc;
using ComplaintTicketApp.Interfaces;
using ComplaintTicketApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using ComplaintTicketApp.Services;
using Microsoft.Extensions.Logging; // Import the logging namespace
using System;

namespace ComplaintTicketApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IComplaintService _complaintService;
        private readonly ILogger<CommentController> _logger; // Add ILogger

        public CommentController(ICommentService commentService,
            IComplaintService complaintService,
            ILogger<CommentController> logger) // Inject ILogger
        {
            _commentService = commentService;
            _complaintService = complaintService;
            _logger = logger; // Initialize ILogger
        }
        [Authorize (Roles="Admin,User")]

        [HttpPost]
        public IActionResult AddComment([FromBody] CommentDTO commentDTO)
        {
            try
            {
                // Check if the user is authenticated
                if (User.Identity.IsAuthenticated)
                {
                    // Retrieve the complaint associated with the comment
                    var associatedComplaint = _complaintService.GetComplaintById(commentDTO.ComplaintId);

                    // Check if the user is the creator of the complaint
                    if (associatedComplaint != null && associatedComplaint.Username == commentDTO.Username)
                    {
                        var result = _commentService.AddComment(commentDTO);

                        if (result)
                        {
                            return Ok("Comment added successfully");
                        }

                        return BadRequest("Failed to add comment");
                    }
                    return BadRequest("You are not the creator of the provided complaint Id.");
                }

                return Forbid(); // User is not authenticated or not authorized to add a comment
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding comment"); // Log the error
                return BadRequest("Failed to add comment");
            }
        }
        [Authorize(Roles = "Admin,User")]
        [HttpDelete("{commentId}")]
        public IActionResult RemoveComment(int commentId)
        {
            try
            {
                var commentDTO = _commentService.GetCommentById(commentId);

                // Check if the user is the creator of the comment or an admin
                if (User.IsInRole("Admin") || (commentDTO != null && commentDTO.Username == User.FindFirst("sub").Value))
                {
                    var result = _commentService.RemoveComment(commentId);

                    if (result)
                    {
                        return Ok("Comment removed successfully");
                    }

                    return NotFound("Comment not found");
                }

                return Forbid(); // User is not authorized to delete this comment
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing comment"); // Log the error
                return NotFound("Comment not found");
            }
        }
        [Authorize(Roles = "Admin,User")]
        [HttpPut]
        public IActionResult UpdateComment([FromBody] CommentDTO commentDTO)
        {
            try
            {
                var existingComment = _commentService.GetCommentById(commentDTO.CommentId);

                    var result = _commentService.UpdateComment(commentDTO);

                    if (result)
                    {
                        return Ok("Comment updated successfully");
                    }

                    return NotFound("Comment not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating comment"); // Log the error
                return NotFound("Comment not found");
            }
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{commentId}")]
        public IActionResult GetCommentById(int commentId)
        {
            try
            {
                var commentDTO = _commentService.GetCommentById(commentId);

                // Check if the user is the creator of the comment or an admin
                if (commentDTO != null)
                {
                    return Ok(commentDTO);
                }

                return NotFound("Comment not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting comment by ID"); // Log the error
                return NotFound("Comment not found");
            }
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("complaint/{complaintId}")]
        public IActionResult GetCommentsByComplaintId(int complaintId)
        {
            try
            {
                // Retrieve the associated complaint
                var associatedComplaint = _complaintService.GetComplaintById(complaintId);

                // Check if the user is the creator of the complaint or an admin
                if (associatedComplaint != null )
                {
                    var commentDTOs = _commentService.GetCommentsByComplaintId(complaintId);

                    // Filter comments to include only those related to the specified complaint
                    var filteredComments = commentDTOs.ToList();

                    return Ok(filteredComments);
                }

                return BadRequest("No Comments Associated with  gievn ComplaintId "); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting comments by complaint ID"); // Log the error
                return Forbid(); // User is not authorized to access comments for this complaint
            }
        }
    }
}
