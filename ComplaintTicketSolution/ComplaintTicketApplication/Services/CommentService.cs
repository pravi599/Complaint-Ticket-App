using System;
using System.Collections.Generic;
using ComplaintTicketApplication.Interfaces;
using ComplaintTicketApplication.Models;
using ComplaintTicketApplication.Models.DTOs;
using ComplaintTicketApplication.Repositories;
using ComplaintTicketApplication.Interfaces;
using ComplaintTicketApplication.Models.DTOs;
using ComplaintTicketApplication.Models;

namespace ComplaintTicketApplication.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<int, Comment> _commentRepository;

        public CommentService(IRepository<int, Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public bool AddComment(CommentDTO commentDTO)
        {
            if (commentDTO == null)
            {
                // Handle null DTO
                return false;
            }

            // Perform additional validation if needed

            var comment = new Comment
            {
                Text = commentDTO.Text,
                CreatedAt = DateTime.UtcNow,
                ComplaintId = commentDTO.ComplaintId,
                Username = commentDTO.Username
                // Map other properties as needed
            };

            // Add the comment to the repository
            var result = _commentRepository.Add(comment);

            return result != null; // Return true if the addition was successful
        }

        public bool RemoveComment(int commentId)
        {
            // Perform additional validation if needed

            // Assuming you have a unique identifier for comments (e.g., CommentId)
            var comment = _commentRepository.GetById(commentId);

            if (comment == null)
            {
                // Handle not found comment
                return false;
            }

            // Perform additional validation or business logic if needed before removal

            // Remove the comment from the repository
            _commentRepository.Delete(comment.CommentId);

            return true; // Return true if the removal was successful
        }

        public bool UpdateComment(CommentDTO commentDTO)
        {
            if (commentDTO == null)
            {
                // Handle null DTO
                return false;
            }

            // Perform additional validation if needed

            // Assuming you have a unique identifier for comments (e.g., CommentId)
            var existingComment = _commentRepository.GetById(commentDTO.CommentId);

            if (existingComment == null)
            {
                // Handle not found comment
                return false;
            }

            // Update properties based on the DTO
            existingComment.Text = commentDTO.Text;
            existingComment.CreatedAt = DateTime.UtcNow; // Update the creation timestamp
            existingComment.ComplaintId = commentDTO.ComplaintId;
            existingComment.Username = commentDTO.Username;
            // Update other properties as needed

            // Perform the update operation in the repository
            _commentRepository.Update(existingComment);

            return true; // Return true if the update was successful
        }

        public CommentDTO GetCommentById(int commentId)
        {
            // Assuming you have a unique identifier for comments (e.g., CommentId)
            var comment = _commentRepository.GetById(commentId);

            // Map the Comment entity to a CommentDTO based on your application's requirements
            var commentDTO = MapCommentToDTO(comment);

            return commentDTO;
        }

        public IEnumerable<CommentDTO> GetCommentsByComplaintId(int complaintId)
        {
            // Assuming you have a method in the repository to get all comments
            var allComments = _commentRepository.GetAll();

            // Filter comments by complaintId
            var comments = allComments.Where(c => c.ComplaintId == complaintId);

            // Map the Comment entities to CommentDTOs based on your application's requirements
            var commentDTOs = MapCommentsToDTOs(comments);

            return commentDTOs;
        }


        // Add mapping methods based on your application's requirements
        private CommentDTO MapCommentToDTO(Comment comment)
        {
            if (comment == null)
            {
                return null;
            }

            return new CommentDTO
            {
                CommentId = comment.CommentId,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt,
                ComplaintId = comment.ComplaintId,
                Username = comment.Username
                // Map other properties as needed
            };
        }

        private IEnumerable<CommentDTO> MapCommentsToDTOs(IEnumerable<Comment> comments)
        {
            // Implement mapping logic here
            // Map properties from Comment to CommentDTO
            var commentDTOs = new List<CommentDTO>();
            foreach (var comment in comments)
            {
                commentDTOs.Add(MapCommentToDTO(comment));
            }
            return commentDTOs;
        }
    }
}