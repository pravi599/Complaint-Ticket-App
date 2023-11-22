/*using Microsoft.AspNetCore.Mvc;
using ComplaintTicketApp.Interfaces;
using ComplaintTicketApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace ComplaintTicketApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public IActionResult AddComment([FromBody] CommentDTO commentDTO)
        {
            // Set the user ID of the comment to the current authenticated user
            commentDTO.Username = User.FindFirst("sub").Value;

            var result = _commentService.AddComment(commentDTO);

            if (result)
            {
                return Ok("Comment added successfully");
            }

            return BadRequest("Failed to add comment");
        }

        [HttpDelete("{commentId}")]
        public IActionResult RemoveComment(int commentId)
        {
            var commentDTO = _commentService.GetCommentById(commentId);

            if (commentDTO != null && commentDTO.Username == User.FindFirst("sub").Value)
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

        [HttpPut]
        public IActionResult UpdateComment([FromBody] CommentDTO commentDTO)
        {
            var existingComment = _commentService.GetCommentById(commentDTO.CommentId);

            if (existingComment != null && existingComment.Username == User.FindFirst("sub").Value)
            {
                var result = _commentService.UpdateComment(commentDTO);

                if (result)
                {
                    return Ok("Comment updated successfully");
                }

                return NotFound("Comment not found");
            }

            return Forbid(); // User is not authorized to update this comment
        }

        [HttpGet("{commentId}")]
        public IActionResult GetCommentById(int commentId)
        {
            var commentDTO = _commentService.GetCommentById(commentId);

            if (commentDTO != null && commentDTO.Username == User.FindFirst("sub").Value)
            {
                return Ok(commentDTO);
            }

            return NotFound("Comment not found");
        }

        [HttpGet("complaint/{complaintId}")]
        public IActionResult GetCommentsByComplaintId(int complaintId)
        {
            var commentDTOs = _commentService.GetCommentsByComplaintId(complaintId);

            // Filter comments to include only those created by the current authenticated user
            var filteredComments = commentDTOs
                .Where(comment => comment.Username == User.FindFirst("sub").Value)
                .ToList();

            return Ok(filteredComments);
        }
    }
}
*/