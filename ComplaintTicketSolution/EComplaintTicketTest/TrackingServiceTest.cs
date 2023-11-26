using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TrackingServiceTest
    {
        [Test]
        public void AddTracking_ValidData_ReturnsTrackingDTO()
        {
            // Arrange
            var mockTrackingRepository = new Mock<IRepository<int, Tracking>>();
            var mockComplaintRepository = new Mock<IRepository<int, Complaint>>();
            var trackingService = new TrackingService(mockTrackingRepository.Object, mockComplaintRepository.Object);

            var trackingDTO = new TrackingDTO
            {
                ComplaintId = 1,
                UpdateDate = DateTime.Now,
                Status = "InProgress"
            };

            var mockAddedTracking = new Tracking
            {
                TrackingId = 1,
                ComplaintId = trackingDTO.ComplaintId,
                UpdateDate = trackingDTO.UpdateDate,
                Status = trackingDTO.Status
            };

            mockTrackingRepository.Setup(repo => repo.Add(It.IsAny<Tracking>())).Returns(mockAddedTracking);

            // Act
            var result = trackingService.AddTracking(trackingDTO);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(mockAddedTracking.TrackingId, result.TrackingId);
            Assert.AreEqual(mockAddedTracking.ComplaintId, result.ComplaintId);
            Assert.AreEqual(mockAddedTracking.UpdateDate, result.UpdateDate);
            Assert.AreEqual(mockAddedTracking.Status, result.Status);
        }
        [Test]
        public void AddTracking_RepositoryThrowsException_ThrowsTrackingAddException()
        {
            // Arrange
            var mockTrackingRepository = new Mock<IRepository<int, Tracking>>();
            var mockComplaintRepository = new Mock<IRepository<int, Complaint>>();
            var trackingService = new TrackingService(mockTrackingRepository.Object, mockComplaintRepository.Object);

            var trackingDTO = new TrackingDTO
            {
                ComplaintId = 1,
                UpdateDate = DateTime.Now,
                Status = "InProgress"
            };

            mockTrackingRepository.Setup(repo => repo.Add(It.IsAny<Tracking>())).Throws(new Exception("Simulated exception"));

            // Act and Assert
            Assert.Throws<TrackingOperationException>(() => trackingService.AddTracking(trackingDTO));
        }
        [Test]
        public void UpdateTrackingStatus_TrackingNotFound_ThrowsTrackingNotFoundException()
        {
            // Arrange
            var mockTrackingRepository = new Mock<IRepository<int, Tracking>>();
            var mockComplaintRepository = new Mock<IRepository<int, Complaint>>();
            var trackingService = new TrackingService(mockTrackingRepository.Object, mockComplaintRepository.Object);

            int trackingId = 1;
            string newStatus = "Resolved";

            mockTrackingRepository.Setup(repo => repo.GetById(trackingId)).Returns((Tracking)null);

            // Act and Assert
            Assert.Throws<TrackingNotFoundException>(() => trackingService.UpdateTrackingStatus(trackingId, newStatus));
        }
        [Test]
        public void RemoveTracking_ValidTrackingId_ReturnsTrue()
        {
            // Arrange
            var mockTrackingRepository = new Mock<IRepository<int, Tracking>>();
            var mockComplaintRepository = new Mock<IRepository<int, Complaint>>();
            var trackingService = new TrackingService(mockTrackingRepository.Object, mockComplaintRepository.Object);

            int trackingId = 1;

            var mockRemovedTracking = new Tracking
            {
                TrackingId = trackingId,
                ComplaintId = 1,
                UpdateDate = DateTime.Now,
                Status = "Resolved"
            };

            mockTrackingRepository.Setup(repo => repo.Delete(trackingId)).Returns(mockRemovedTracking);

            // Act
            var result = trackingService.RemoveTracking(trackingId);

            // Assert
            Assert.True(result);
        }
        [Test]
        public void RemoveTracking_TrackingNotFound_ThrowsTrackingNotFoundException()
        {
            // Arrange
            var mockTrackingRepository = new Mock<IRepository<int, Tracking>>();
            var mockComplaintRepository = new Mock<IRepository<int, Complaint>>();
            var trackingService = new TrackingService(mockTrackingRepository.Object, mockComplaintRepository.Object);

            int trackingId = 1;

            mockTrackingRepository.Setup(repo => repo.Delete(trackingId)).Returns((Tracking)null);

            // Act and Assert
            Assert.Throws<TrackingNotFoundException>(() => trackingService.RemoveTracking(trackingId));
        }
        [Test]
        public void GetAllTrackings_ValidData_ReturnsListofTrackingDTOs()
        {
            // Arrange
            var mockTrackingRepository = new Mock<IRepository<int, Tracking>>();
            var mockComplaintRepository = new Mock<IRepository<int, Complaint>>();
            var trackingService = new TrackingService(mockTrackingRepository.Object, mockComplaintRepository.Object);

            var mockTrackingList = new List<Tracking>
            {
                new Tracking { TrackingId = 1, ComplaintId = 1, UpdateDate = DateTime.Now, Status = "InProgress" },
                new Tracking { TrackingId = 2, ComplaintId = 2, UpdateDate = DateTime.Now, Status = "Resolved" }
            };

            mockTrackingRepository.Setup(repo => repo.GetAll()).Returns(mockTrackingList);

            // Act
            var result = trackingService.GetAllTrackings();

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<IList<TrackingDTO>>(result);
            Assert.AreEqual(mockTrackingList.Count, result.Count);
            Assert.AreEqual(mockTrackingList[0].TrackingId, result[0].TrackingId);
            Assert.AreEqual(mockTrackingList[0].ComplaintId, result[0].ComplaintId);
            Assert.AreEqual(mockTrackingList[0].UpdateDate, result[0].UpdateDate);
            Assert.AreEqual(mockTrackingList[0].Status, result[0].Status);
        }
        [Test]
        public void GetAllTrackings_EmptyList_ThrowsTrackingNotFoundException()
        {
            // Arrange
            var mockTrackingRepository = new Mock<IRepository<int, Tracking>>();
            var mockComplaintRepository = new Mock<IRepository<int, Complaint>>();
            var trackingService = new TrackingService(mockTrackingRepository.Object, mockComplaintRepository.Object);

            mockTrackingRepository.Setup(repo => repo.GetAll()).Returns(new List<Tracking>());

            // Act and Assert
            Assert.Throws<TrackingNotFoundException>(() => trackingService.GetAllTrackings());
        }
        [Test]
        public void UpdateTrackingStatusByComplaintId_ValidData_ReturnsUpdatedTrackingDTO()
        {
            // Arrange
            var mockTrackingRepository = new Mock<IRepository<int, Tracking>>();
            var mockComplaintRepository = new Mock<IRepository<int, Complaint>>();
            var trackingService = new TrackingService(mockTrackingRepository.Object, mockComplaintRepository.Object);

            int complaintId = 1;
            string newStatus = "Resolved";

            var mockExistingTracking = new Tracking
            {
                TrackingId = 1,
                ComplaintId = complaintId,
                UpdateDate = DateTime.Now.AddHours(-1),
                Status = "InProgress"
            };

            var mockUpdatedTracking = new Tracking
            {
                TrackingId = 1,
                ComplaintId = complaintId,
                UpdateDate = DateTime.Now,
                Status = newStatus
            };

            mockTrackingRepository.Setup(repo => repo.GetAll())
                .Returns(new List<Tracking> { mockExistingTracking });

            mockTrackingRepository.Setup(repo => repo.Update(It.IsAny<Tracking>())).Returns(mockUpdatedTracking);

            // Act
            var result = trackingService.UpdateTrackingStatusByComplaintId(complaintId, newStatus);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(mockUpdatedTracking.TrackingId, result.TrackingId);
            Assert.AreEqual(mockUpdatedTracking.ComplaintId, result.ComplaintId);
            Assert.AreEqual(mockUpdatedTracking.UpdateDate, result.UpdateDate);
            Assert.AreEqual(mockUpdatedTracking.Status, result.Status);
        }

        [Test]
        public void UpdateTrackingStatusByComplaintId_ComplaintNotFound_ThrowsComplaintNotFoundException()
        {
            // Arrange
            var mockTrackingRepository = new Mock<IRepository<int, Tracking>>();
            var mockComplaintRepository = new Mock<IRepository<int, Complaint>>();
            var trackingService = new TrackingService(mockTrackingRepository.Object, mockComplaintRepository.Object);

            int complaintId = 1;
            string newStatus = "Resolved";

            mockTrackingRepository.Setup(repo => repo.GetAll()).Returns(new List<Tracking>());

            // Act and Assert
            Assert.Throws<ComplaintNotFoundException>(() => trackingService.UpdateTrackingStatusByComplaintId(complaintId, newStatus));
        }
        // Add more test cases for other methods (UpdateTrackingStatus, GetTrackingById, GetAllTrackings, RemoveTracking, GetTrackingByComplaintId, UpdateTrackingStatusByComplaintId) following a similar pattern.
    }
}
