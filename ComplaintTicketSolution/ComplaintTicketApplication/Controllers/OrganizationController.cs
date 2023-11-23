using Microsoft.AspNetCore.Mvc;
using ComplaintTicketApplication.Interfaces;
using ComplaintTicketApplication.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using ComplaintTicketApp.Models.DTOs;

namespace ComplaintTicketApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddOrganization([FromBody] OrganizationDTO organizationDTO)
        {
            var result = _organizationService.AddOrganization(organizationDTO);

            if (result)
            {
                return Ok("Organization added successfully");
            }

            return BadRequest("Failed to add organization");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{organizationId}")]
        public IActionResult RemoveOrganization(int organizationId)
        {
            var result = _organizationService.RemoveOrganization(organizationId);

            if (result)
            {
                return Ok("Organization removed successfully");
            }

            return NotFound("Organization not found");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult UpdateOrganization([FromBody] OrganizationDTO organizationDTO)
        {
            var result = _organizationService.UpdateOrganization(organizationDTO);

            if (result)
            {
                return Ok("Organization updated successfully");
            }

            return NotFound("Organization not found");
        }

        [HttpGet("{organizationId}")]
        public IActionResult GetOrganizationById(int organizationId)
        {
            var organizationDTO = _organizationService.GetOrganizationById(organizationId);

            if (organizationDTO != null)
            {
                return Ok(organizationDTO);
            }
            return NotFound("Organization not found");
        }

        [HttpGet]
        public IActionResult GetAllOrganizations()
        {
            var organizationDTOs = _organizationService.GetAllOrganizations();

            return Ok(organizationDTOs);
        }
    }
}