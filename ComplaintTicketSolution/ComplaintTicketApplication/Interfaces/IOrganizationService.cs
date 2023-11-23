using ComplaintTicketApplication.Models.DTOs;
using System.Collections.Generic;

namespace ComplaintTicketApplication.Interfaces
{
    public interface IOrganizationService
    {
        bool AddOrganization(OrganizationDTO organizationDTO);
        bool RemoveOrganization(int organizationId);
        bool UpdateOrganization(OrganizationDTO organizationDTO);
        OrganizationDTO GetOrganizationById(int organizationId);
        IEnumerable<OrganizationDTO> GetAllOrganizations();
    }
}