using ComplaintTicketApp.Exceptions;
using ComplaintTicketApp.Interfaces;
using ComplaintTicketApp.Models.DTOs;
using ComplaintTicketApp.Models;
using ComplaintTicketApp.Services;
using Moq;
namespace ComplaintTicketTest
    {
    [TestFixture]
    public class ComplaintServiceTest
    {
    [Test]
    public void AddComplaint_ValidDTO_ReturnsTrue()
    {
        // Arrange
        var complaintRepositoryMock = new Mock<IRepository<int, Complaint>>();
        var priorityRepositoryMock = new Mock<IRepository<int, Priority>>();
        var trackingRepositoryMock = new Mock<IRepository<int, Tracking>>();
        var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();

        var complaintService = new ComplaintService(
            complaintRepositoryMock.Object,
            priorityRepositoryMock.Object,
            trackingRepositoryMock.Object,
            organizationRepositoryMock.Object);

        var complaintDTO = new ComplaintDTO
        {
            OrganizationId = 1,
            OrganizationName = "TestOrg",
            Category = "TestCategory",
            Description = "Test Description",
            Username = "TestUser",
            FilePath = "TestFilePath"
        };

        organizationRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(new Organization());
        complaintRepositoryMock.Setup(repo => repo.Add(It.IsAny<Complaint>())).Returns(new Complaint());
        priorityRepositoryMock.Setup(repo => repo.Add(It.IsAny<Priority>())).Returns(new Priority());
        trackingRepositoryMock.Setup(repo => repo.Add(It.IsAny<Tracking>())).Returns(new Tracking());

        // Act
        var result = complaintService.Add(complaintDTO);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void AddComplaint_OrganizationNotFound_ThrowsOrganizationNotFoundException()
    {
        // Arrange
        var complaintRepositoryMock = new Mock<IRepository<int, Complaint>>();
        var priorityRepositoryMock = new Mock<IRepository<int, Priority>>();
        var trackingRepositoryMock = new Mock<IRepository<int, Tracking>>();
        var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();

        var complaintService = new ComplaintService(
            complaintRepositoryMock.Object,
            priorityRepositoryMock.Object,
            trackingRepositoryMock.Object,
            organizationRepositoryMock.Object);

        var complaintDTO = new ComplaintDTO
        {
            OrganizationId = 1,
            OrganizationName = "TestOrg",
            Category = "TestCategory",
            Description = "Test Description",
            Username = "TestUser",
            FilePath = "TestFilePath"
        };

        organizationRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Organization)null);

        // Act & Assert
        Assert.Throws<OrganizationNotFoundException>(() => complaintService.Add(complaintDTO));
    }

    [Test]
    public void GetAllComplaints_ReturnsComplaintDTOList()
    {
        // Arrange
        var complaintRepositoryMock = new Mock<IRepository<int, Complaint>>();
        var priorityRepositoryMock = new Mock<IRepository<int, Priority>>();
        var trackingRepositoryMock = new Mock<IRepository<int, Tracking>>();
        var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();

        var complaintService = new ComplaintService(
            complaintRepositoryMock.Object,
            priorityRepositoryMock.Object,
            trackingRepositoryMock.Object,
            organizationRepositoryMock.Object);

        complaintRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Complaint> { new Complaint() });

        // Act
        var result = complaintService.GetAllComplaints();

        // Assert
        Assert.NotNull(result);
        Assert.IsInstanceOf<IEnumerable<ComplaintDTO>>(result);
    }

    [Test]
    public void GetComplaintById_ValidId_ReturnsComplaintDTO()
    {
        // Arrange
        var complaintRepositoryMock = new Mock<IRepository<int, Complaint>>();
        var priorityRepositoryMock = new Mock<IRepository<int, Priority>>();
        var trackingRepositoryMock = new Mock<IRepository<int, Tracking>>();
        var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();

        var complaintService = new ComplaintService(
            complaintRepositoryMock.Object,
            priorityRepositoryMock.Object,
            trackingRepositoryMock.Object,
            organizationRepositoryMock.Object);

        complaintRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(new Complaint());

        // Act
        var result = complaintService.GetComplaintById(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsInstanceOf<ComplaintDTO>(result);
    }

    [Test]
    public void GetComplaintById_InvalidId_ThrowsComplaintNotFoundException()
    {
        // Arrange
        var complaintRepositoryMock = new Mock<IRepository<int, Complaint>>();
        var priorityRepositoryMock = new Mock<IRepository<int, Priority>>();
        var trackingRepositoryMock = new Mock<IRepository<int, Tracking>>();
        var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();

        var complaintService = new ComplaintService(
            complaintRepositoryMock.Object,
            priorityRepositoryMock.Object,
            trackingRepositoryMock.Object,
            organizationRepositoryMock.Object);

        complaintRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Complaint)null);

        // Act & Assert
        Assert.Throws<ComplaintNotFoundException>(() => complaintService.GetComplaintById(1));
    }

    [Test]
    public void RemoveComplaint_ValidId_ReturnsTrue()
    {
        // Arrange
        var complaintRepositoryMock = new Mock<IRepository<int, Complaint>>();
        var priorityRepositoryMock = new Mock<IRepository<int, Priority>>();
        var trackingRepositoryMock = new Mock<IRepository<int, Tracking>>();
        var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();

        var complaintService = new ComplaintService(
            complaintRepositoryMock.Object,
            priorityRepositoryMock.Object,
            trackingRepositoryMock.Object,
            organizationRepositoryMock.Object);

        complaintRepositoryMock.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(new Complaint());

        // Act
        var result = complaintService.Remove(1);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void RemoveComplaint_InvalidId_ThrowsComplaintNotFoundException()
    {
        // Arrange
        var complaintRepositoryMock = new Mock<IRepository<int, Complaint>>();
        var priorityRepositoryMock = new Mock<IRepository<int, Priority>>();
        var trackingRepositoryMock = new Mock<IRepository<int, Tracking>>();
        var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();

        var complaintService = new ComplaintService(
            complaintRepositoryMock.Object,
            priorityRepositoryMock.Object,
            trackingRepositoryMock.Object,
            organizationRepositoryMock.Object);

        complaintRepositoryMock.Setup(repo => repo.Delete(It.IsAny<int>())).Returns((Complaint)null);

        // Act & Assert
        Assert.Throws<ComplaintNotFoundException>(() => complaintService.Remove(1));
    }

    [Test]
    public void UpdateComplaint_ValidDTO_ReturnsComplaintDTO()
    {
        // Arrange
        var complaintRepositoryMock = new Mock<IRepository<int, Complaint>>();
        var priorityRepositoryMock = new Mock<IRepository<int, Priority>>();
        var trackingRepositoryMock = new Mock<IRepository<int, Tracking>>();
        var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();

        var complaintService = new ComplaintService(
            complaintRepositoryMock.Object,
            priorityRepositoryMock.Object,
            trackingRepositoryMock.Object,
            organizationRepositoryMock.Object);

        var complaintDTO = new ComplaintDTO
        {
            ComplaintId = 1,
            OrganizationId = 1,
            OrganizationName = "TestOrg",
            Category = "TestCategory",
            Description = "Updated Description",
            Username = "UpdatedUser",
            FilePath = "UpdatedFilePath"
        };

        complaintRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(new Complaint());
        complaintRepositoryMock.Setup(repo => repo.Update(It.IsAny<Complaint>())).Returns(new Complaint());

        // Act
        var result = complaintService.Update(complaintDTO);

        // Assert
        Assert.NotNull(result);
        Assert.IsInstanceOf<ComplaintDTO>(result);
    }

    [Test]
    public void UpdateComplaint_InvalidId_ThrowsComplaintNotFoundException()
    {
        // Arrange
        var complaintRepositoryMock = new Mock<IRepository<int, Complaint>>();
        var priorityRepositoryMock = new Mock<IRepository<int, Priority>>();
        var trackingRepositoryMock = new Mock<IRepository<int, Tracking>>();
        var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();

        var complaintService = new ComplaintService(
            complaintRepositoryMock.Object,
            priorityRepositoryMock.Object,
            trackingRepositoryMock.Object,
            organizationRepositoryMock.Object);

        var complaintDTO = new ComplaintDTO { ComplaintId = 1 };

        complaintRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Complaint)null);

        // Act & Assert
        Assert.Throws<ComplaintNotFoundException>(() => complaintService.Update(complaintDTO));
    }

    // Add more test methods as needed
}
}