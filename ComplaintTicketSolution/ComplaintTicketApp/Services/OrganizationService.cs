using System.Collections.Generic;
using ComplaintTicketApp.Interfaces;
using ComplaintTicketApp.Models;
using ComplaintTicketApp.Models.DTOs;
using ComplaintTicketApp.Repositories;

namespace ComplaintTicketApp.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IRepository<int, Organization> _organizationRepository;

        public OrganizationService(IRepository<int, Organization> organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public bool AddOrganization(OrganizationDTO organizationDTO)
        {
            if (organizationDTO == null)
            {
                // Handle null DTO
                return false;
            }

            // Perform additional validation if needed

            var organization = new Organization
            {
                OrganizationName = organizationDTO.OrganizationName,
                Description = organizationDTO.Description,
                ContactEmail = organizationDTO.ContactEmail,
                ContactPhone = organizationDTO.ContactPhone
                // Map other properties as needed
            };

            // Add the organization to the repository
            var result = _organizationRepository.Add(organization);

            return result != null; // Return true if the addition was successful
        }

        public bool RemoveOrganization(int organizationId)
        {
            // Perform additional validation if needed

            // Assuming you have a unique identifier for organizations (e.g., OrganizationId)
            var organization = _organizationRepository.GetById(organizationId);

            if (organization == null)
            {
                // Handle not found organization
                return false;
            }

            // Perform additional validation or business logic if needed before removal

            // Remove the organization from the repository
            _organizationRepository.Delete(organization.OrganizationId);

            return true; // Return true if the removal was successful
        }

        public bool UpdateOrganization(OrganizationDTO organizationDTO)
        {
            if (organizationDTO == null)
            {
                // Handle null DTO
                return false;
            }

            // Perform additional validation if needed

            // Assuming you have a unique identifier for organizations (e.g., OrganizationId)
            var existingOrganization = _organizationRepository.GetById(organizationDTO.OrganizationId);

            if (existingOrganization == null)
            {
                // Handle not found organization
                return false;
            }

            // Update properties based on the DTO
            existingOrganization.OrganizationName = organizationDTO.OrganizationName;
            existingOrganization.Description = organizationDTO.Description;
            existingOrganization.ContactEmail = organizationDTO.ContactEmail;
            existingOrganization.ContactPhone = organizationDTO.ContactPhone;
            // Update other properties as needed

            // Perform the update operation in the repository
            _organizationRepository.Update(existingOrganization);

            return true; // Return true if the update was successful
        }

        public OrganizationDTO GetOrganizationById(int organizationId)
        {
            // Assuming you have a unique identifier for organizations (e.g., OrganizationId)
            var organization = _organizationRepository.GetById(organizationId);

            // Map the Organization entity to an OrganizationDTO based on your application's requirements
            var organizationDTO = MapOrganizationToDTO(organization);

            return organizationDTO;
        }

        public IEnumerable<OrganizationDTO> GetAllOrganizations()
        {
            // You may need to map the Organization entities to DTOs based on your application's requirements
            // Here, I assume you have an OrganizationDTO class with similar properties as Organization
            var organizations = _organizationRepository.GetAll();
            var organizationDTOs = MapOrganizationsToDTOs(organizations);
            return organizationDTOs;
        }

        // Add mapping methods based on your application's requirements
        private OrganizationDTO MapOrganizationToDTO(Organization organization)
        {
            if (organization == null)
            {
                return null;
            }

            return new OrganizationDTO
            {
                OrganizationId = organization.OrganizationId,
                OrganizationName = organization.OrganizationName,
                Description = organization.Description,
                ContactEmail = organization.ContactEmail,
                ContactPhone = organization.ContactPhone
                // Map other properties as needed
            };
        }


        private IEnumerable<OrganizationDTO> MapOrganizationsToDTOs(IEnumerable<Organization> organizations)
        {
            // Implement mapping logic here
            // Map properties from Organization to OrganizationDTO
            var organizationDTOs = new List<OrganizationDTO>();
            foreach (var organization in organizations)
            {
                organizationDTOs.Add(MapOrganizationToDTO(organization));
            }
            return organizationDTOs;
        }
    }
}
