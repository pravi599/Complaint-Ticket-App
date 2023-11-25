// OrganizationServiceTest.cs
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
    public class OrganizationServiceTest
    {
        [Test]
        public void AddOrganization_ValidDTO_ReturnsTrue()
        {
            // Arrange
            var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();
            var organizationService = new OrganizationService(organizationRepositoryMock.Object);

            var organizationDTO = new OrganizationDTO();

            organizationRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Organization>());
            organizationRepositoryMock.Setup(repo => repo.Add(It.IsAny<Organization>())).Returns(new Organization());

            // Act
            var result = organizationService.AddOrganization(organizationDTO);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void AddOrganization_NullDTO_ThrowsNullDTOException()
        {
            // Arrange
            var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();
            var organizationService = new OrganizationService(organizationRepositoryMock.Object);

            // Act & Assert
            Assert.Throws<NullDTOException>(() => organizationService.AddOrganization(null));
        }

        [Test]
        public void AddOrganization_DuplicateOrganization_ThrowsDuplicateOrganizationException()
        {
            // Arrange
            var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();
            var organizationService = new OrganizationService(organizationRepositoryMock.Object);

            var existingOrganization = new Organization { OrganizationName = "TestOrg" };
            organizationRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Organization> { existingOrganization });

            var organizationDTO = new OrganizationDTO { OrganizationName = "TestOrg" };

            // Act & Assert
            Assert.Throws<DuplicateOrganizationException>(() => organizationService.AddOrganization(organizationDTO));
        }

        [Test]
        public void RemoveOrganization_ValidId_ReturnsTrue()
        {
            // Arrange
            var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();
            var organizationService = new OrganizationService(organizationRepositoryMock.Object);

            organizationRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(new Organization());

            // Act
            var result = organizationService.RemoveOrganization(1);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void RemoveOrganization_InvalidId_ThrowsOrganizationNotFoundException()
        {
            // Arrange
            var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();
            var organizationService = new OrganizationService(organizationRepositoryMock.Object);

            organizationRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Organization)null);

            // Act & Assert
            Assert.Throws<OrganizationNotFoundException>(() => organizationService.RemoveOrganization(1));
        }

        [Test]
        public void UpdateOrganization_ValidDTO_ReturnsTrue()
        {
            // Arrange
            var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();
            var organizationService = new OrganizationService(organizationRepositoryMock.Object);

            var organizationDTO = new OrganizationDTO
            {
                OrganizationId = 1,
                OrganizationName = "UpdatedOrg",
                Description = "Updated Description",
                ContactEmail = "updated@example.com",
                ContactPhone = "987-654-3210"
            };

            organizationRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(new Organization());

            // Act
            var result = organizationService.UpdateOrganization(organizationDTO);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateOrganization_NullDTO_ThrowsNullDTOException()
        {
            // Arrange
            var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();
            var organizationService = new OrganizationService(organizationRepositoryMock.Object);

            // Act & Assert
            Assert.Throws<NullDTOException>(() => organizationService.UpdateOrganization(null));
        }

        [Test]
        public void UpdateOrganization_InvalidId_ThrowsOrganizationNotFoundException()
        {
            // Arrange
            var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();
            var organizationService = new OrganizationService(organizationRepositoryMock.Object);

            organizationRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Organization)null);

            var organizationDTO = new OrganizationDTO { OrganizationId = 1 };

            // Act & Assert
            Assert.Throws<OrganizationNotFoundException>(() => organizationService.UpdateOrganization(organizationDTO));
        }

        [Test]
        public void GetOrganizationById_ValidId_ReturnsOrganizationDTO()
        {
            // Arrange
            var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();
            var organizationService = new OrganizationService(organizationRepositoryMock.Object);

            organizationRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(new Organization());

            // Act
            var result = organizationService.GetOrganizationById(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<OrganizationDTO>(result);
        }

        [Test]
        public void GetOrganizationById_InvalidId_ThrowsOrganizationNotFoundException()
        {
            // Arrange
            var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();
            var organizationService = new OrganizationService(organizationRepositoryMock.Object);

            organizationRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Organization)null);

            // Act & Assert
            Assert.Throws<OrganizationNotFoundException>(() => organizationService.GetOrganizationById(1));
        }

        [Test]
        public void GetAllOrganizations_ReturnsOrganizationDTOList()
        {
            // Arrange
            var organizationRepositoryMock = new Mock<IRepository<int, Organization>>();
            var organizationService = new OrganizationService(organizationRepositoryMock.Object);

            organizationRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Organization> { new Organization() });

            // Act
            var result = organizationService.GetAllOrganizations();

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<IEnumerable<OrganizationDTO>>(result);
        }

    }
}


