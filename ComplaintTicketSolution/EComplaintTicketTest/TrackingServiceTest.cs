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
    public class TrackingServiceTest
    {
        private Mock<IRepository<int, Tracking>> _trackingRepositoryMock;
        private Mock<IRepository<int, Complaint>> _complaintRepositoryMock;
        private TrackingService _trackingService;

        [SetUp]
        public void Setup()
        {
            _trackingRepositoryMock = new Mock<IRepository<int, Tracking>>();
            _complaintRepositoryMock = new Mock<IRepository<int, Complaint>>();
            _trackingService = new TrackingService(_trackingRepositoryMock.Object, _complaintRepositoryMock.Object);
        }

        [Test]
        public void AddTracking_ValidTracking_ReturnsTrackingDTO()
        {
            // Arrange
            SetupTrackingRepositoryAdd();

            var trackingDTO = CreateValidTrackingDTO();

            // Act
            var result = _trackingService.AddTracking(trackingDTO);

            // Assert
            AssertTrackingDTO(trackingDTO, result);
        }

        [Test]
        public void AddTracking_TrackingRepositoryThrowsException_ThrowsTrackingAddException()
        {
            // Arrange
            SetupTrackingRepositoryAddThrowsException();

            var trackingDTO = CreateValidTrackingDTO();

            // Act & Assert
            Assert.Throws<TrackingAddException>(() => _trackingService.AddTracking(trackingDTO));
        }

        [Test]
        public void UpdateTrackingStatus_ValidTracking_ReturnsUpdatedTrackingDTO()
        {
            // Arrange
            SetupTrackingRepositoryUpdate();
            SetupTrackingRepositoryGetById();

            var trackingId = 1;
            var newStatus = "Completed";

            // Act
            var result = _trackingService.UpdateTrackingStatus(trackingId, newStatus);

            // Assert
            AssertUpdatedTrackingDTO(result, newStatus);
        }

        [Test]
        public void UpdateTrackingStatus_TrackingNotFound_ThrowsTrackingNotFoundException()
        {
            // Arrange
            SetupTrackingRepositoryGetByIdNotFound();

            var trackingId = 1;
            var newStatus = "Completed";

            // Act & Assert
            Assert.Throws<TrackingNotFoundException>(() => _trackingService.UpdateTrackingStatus(trackingId, newStatus));
        }

        // Similar methods for other test cases

        private void SetupTrackingRepositoryAdd()
        {
            _trackingRepositoryMock.Setup(repo => repo.Add(It.IsAny<Tracking>()))
                                   .Returns<Tracking>(entity => entity);
        }

        private void SetupTrackingRepositoryAddThrowsException()
        {
            _trackingRepositoryMock.Setup(repo => repo.Add(It.IsAny<Tracking>()))
                                   .Throws<Exception>();
        }

        private void SetupTrackingRepositoryUpdate()
        {
            _trackingRepositoryMock.Setup(repo => repo.Update(It.IsAny<Tracking>()))
                                   .Returns<Tracking>(entity => entity);
        }

        private void SetupTrackingRepositoryGetById()
        {
            _trackingRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>()))
                                   .Returns<int>(id => new Tracking { TrackingId = id, Status = "In Progress" });
        }

        private void SetupTrackingRepositoryGetByIdNotFound()
        {
            _trackingRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>()))
                                   .Returns<int>(id => null);
        }

        private TrackingDTO CreateValidTrackingDTO()
        {
            return new TrackingDTO
            {
                ComplaintId = 1,
                UpdateDate = DateTime.Now,
                Status = "In Progress"
            };
        }

        private void AssertTrackingDTO(TrackingDTO expected, TrackingDTO actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ComplaintId, actual.ComplaintId);
            Assert.AreEqual(expected.UpdateDate, actual.UpdateDate);
            Assert.AreEqual(expected.Status, actual.Status);
        }

        private void AssertUpdatedTrackingDTO(TrackingDTO actual, string expectedStatus)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual.Status);
        }
    }
}
