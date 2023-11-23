using System.Collections.Generic;
using ComplaintTicketApp.Exceptions;
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
            // Check for null DTO
            if (organizationDTO == null)
            {
                throw new NullDTOException();
            }
            // Check for duplicate organization name
            if (_organizationRepository.GetAll().Any(o => o.OrganizationName == organizationDTO.OrganizationName))
            {
                throw new DuplicateOrganizationException();
            }


            var organization = new Organization
            {
                OrganizationName = organizationDTO.OrganizationName,
                Description = organizationDTO.Description,
                ContactEmail = organizationDTO.ContactEmail,
                ContactPhone = organizationDTO.ContactPhone
            };

            // Add the organization to the repository
            var result = _organizationRepository.Add(organization);

            return result != null; // Return true if the addition was successful
        }

        public bool RemoveOrganization(int organizationId)
        {
            var organization = _organizationRepository.GetById(organizationId);

            if (organization == null)
            {
                // Handle not found organization
                throw new OrganizationNotFoundException();
            }

            // Remove the organization from the repository
            _organizationRepository.Delete(organization.OrganizationId);

            return true; // Return true if the removal was successful
        }

        public bool UpdateOrganization(OrganizationDTO organizationDTO)
        {
            // Check for null DTO
            if (organizationDTO == null)
            {
                throw new NullDTOException();
            }

            var existingOrganization = _organizationRepository.GetById(organizationDTO.OrganizationId);

            if (existingOrganization == null)
            {
                // Handle not found organization
                throw new OrganizationNotFoundException();
            }

            // Update properties based on the DTO
            existingOrganization.OrganizationName = organizationDTO.OrganizationName;
            existingOrganization.Description = organizationDTO.Description;
            existingOrganization.ContactEmail = organizationDTO.ContactEmail;
            existingOrganization.ContactPhone = organizationDTO.ContactPhone;

            // Perform the update operation in the repository
            _organizationRepository.Update(existingOrganization);

            return true; // Return true if the update was successful
        }

        public OrganizationDTO GetOrganizationById(int organizationId)
        {
            var organization = _organizationRepository.GetById(organizationId);
            if (organization == null)
            {
                // Handle not found organization
                throw new OrganizationNotFoundException();
            }
            // Map the Organization entity to an OrganizationDTO
            var organizationDTO = MapOrganizationToDTO(organization);

            return organizationDTO;
        }

        public IEnumerable<OrganizationDTO> GetAllOrganizations()
        {
            var organizations = _organizationRepository.GetAll();
            var organizationDTOs = MapOrganizationsToDTOs(organizations);
            return organizationDTOs;
        }

        // Add mapping methods
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
            };
        }


        private IEnumerable<OrganizationDTO> MapOrganizationsToDTOs(IEnumerable<Organization> organizations)
        {
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
