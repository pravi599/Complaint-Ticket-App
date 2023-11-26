using System;
using System.Collections.Generic;
using ComplaintTicketApp.Exceptions;
using ComplaintTicketApp.Interfaces;
using ComplaintTicketApp.Models;
using ComplaintTicketApp.Models.DTOs;
using ComplaintTicketApp.Services;
using Moq;
using NUnit.Framework;

namespace ComplaintTicketTest
{
    [TestFixture]
    public class CommentServiceTest
    {
        [Test]
        public void AddComment_ValidData_ReturnsTrue()
        {
            // Arrange
            var mockCommentRepository = new Mock<IRepository<int, Comment>>();
            var commentService = new CommentService(mockCommentRepository.Object);

            var commentDTO = new CommentDTO
            {
                Text = "Test Comment",
                CreatedAt = DateTime.UtcNow,
                ComplaintId = 1,
                Username = "TestUser"
            };

            var mockAddedComment = new Comment
            {
                CommentId = 1,
                Text = commentDTO.Text,
                CreatedAt = commentDTO.CreatedAt,
                ComplaintId = commentDTO.ComplaintId,
                Username = commentDTO.Username
            };

            mockCommentRepository.Setup(repo => repo.Add(It.IsAny<Comment>())).Returns(mockAddedComment);

            // Act
            var result = commentService.AddComment(commentDTO);

            // Assert
            Assert.True(result);
        }

        [Test]
        public void AddComment_NullDTO_ReturnsFalse()
        {
            // Arrange
            var mockCommentRepository = new Mock<IRepository<int, Comment>>();
            var commentService = new CommentService(mockCommentRepository.Object);

            // Act
            var result = commentService.AddComment(null);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void RemoveComment_ValidCommentId_ReturnsTrue()
        {
            // Arrange
            var mockCommentRepository = new Mock<IRepository<int, Comment>>();
            var commentService = new CommentService(mockCommentRepository.Object);

            int commentId = 1;

            var mockComment = new Comment
            {
                CommentId = commentId,
                Text = "Test Comment",
                CreatedAt = DateTime.UtcNow,
                ComplaintId = 1,
                Username = "TestUser"
            };

            mockCommentRepository.Setup(repo => repo.GetById(commentId)).Returns(mockComment);

            // Act
            var result = commentService.RemoveComment(commentId);

            // Assert
            Assert.True(result);
        }

        [Test]
        public void RemoveComment_CommentNotFound_ReturnsFalse()
        {
            // Arrange
            var mockCommentRepository = new Mock<IRepository<int, Comment>>();
            var commentService = new CommentService(mockCommentRepository.Object);

            int commentId = 1;

            mockCommentRepository.Setup(repo => repo.GetById(commentId)).Returns((Comment)null);

            // Act
            var result = commentService.RemoveComment(commentId);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void UpdateComment_ValidData_ReturnsTrue()
        {
            // Arrange
            var mockCommentRepository = new Mock<IRepository<int, Comment>>();
            var commentService = new CommentService(mockCommentRepository.Object);

            var commentDTO = new CommentDTO
            {
                CommentId = 1,
                Text = "Updated Comment",
                CreatedAt = DateTime.UtcNow,
                ComplaintId = 1,
                Username = "TestUser"
            };

            var mockExistingComment = new Comment
            {
                CommentId = commentDTO.CommentId,
                Text = "Original Comment",
                CreatedAt = DateTime.UtcNow.AddHours(-1),
                ComplaintId = commentDTO.ComplaintId,
                Username = "TestUser"
            };

            mockCommentRepository.Setup(repo => repo.GetById(commentDTO.CommentId)).Returns(mockExistingComment);

            // Act
            var result = commentService.UpdateComment(commentDTO);

            // Assert
            Assert.True(result);
        }

        [Test]
        public void UpdateComment_NullDTO_ReturnsFalse()
        {
            // Arrange
            var mockCommentRepository = new Mock<IRepository<int, Comment>>();
            var commentService = new CommentService(mockCommentRepository.Object);

            // Act
            var result = commentService.UpdateComment(null);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void UpdateComment_CommentNotFound_ReturnsFalse()
        {
            // Arrange
            var mockCommentRepository = new Mock<IRepository<int, Comment>>();
            var commentService = new CommentService(mockCommentRepository.Object);

            var commentDTO = new CommentDTO
            {
                CommentId = 1,
                Text = "Updated Comment",
                CreatedAt = DateTime.UtcNow,
                ComplaintId = 1,
                Username = "TestUser"
            };

            mockCommentRepository.Setup(repo => repo.GetById(commentDTO.CommentId)).Returns((Comment)null);

            // Act
            var result = commentService.UpdateComment(commentDTO);

            // Assert
            Assert.False(result);
        }

        // Add more test cases as needed.

        private static CommentDTO MapCommentToDTO(Comment comment)
        {
            return new CommentDTO
            {
                CommentId = comment.CommentId,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt,
                ComplaintId = comment.ComplaintId,
                Username = comment.Username
            };
        }

        private static IEnumerable<CommentDTO> MapCommentsToDTOs(IEnumerable<Comment> comments)
        {
            var commentDTOs = new List<CommentDTO>();
            foreach (var comment in comments)
            {
                commentDTOs.Add(MapCommentToDTO(comment));
            }
            return commentDTOs;
        }
    }
}

