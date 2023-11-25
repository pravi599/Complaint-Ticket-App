using Microsoft.AspNetCore.Mvc;
using ComplaintTicketApp.Interfaces;
using ComplaintTicketApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using ComplaintTicketApp.Exceptions;

namespace ComplaintTicketApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        private readonly ILogger<OrganizationController> _logger;

        public OrganizationController(IOrganizationService organizationService, 
            ILogger<OrganizationController> logger)
        {
            _organizationService = organizationService;
            _logger = logger;

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddOrganization([FromBody] OrganizationDTO organizationDTO)
        {
            try
            {
                var result = _organizationService.AddOrganization(organizationDTO);

                if (result)
                {
                    _logger.LogInformation("Organization added successfully");
                    return Ok(organizationDTO);
                }

                return BadRequest("Failed to add organization");
            }
            
            catch (NullDTOException ex)
            {
                return BadRequest($"Failed to add organization. {ex.Message}");
            }
            catch (DuplicateOrganizationException ex)
            {
                return BadRequest($"Failed to add organization. {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log or handle other unexpected exceptions
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{organizationId}")]
        public IActionResult RemoveOrganization(int organizationId)
        {
            try
            {
                var result = _organizationService.RemoveOrganization(organizationId);

                if (result)
                {
                    return Ok("Organization removed successfully");
                }

                return NotFound("Organization not found");
            }
            catch (OrganizationNotFoundException ex)
            {
                return NotFound($"Failed to remove organization. {ex.Message}");
            }
            catch (Exception)
            {
                // Log or handle other unexpected exceptions
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult UpdateOrganization([FromBody] OrganizationDTO organizationDTO)
        {
            try
            {
                var result = _organizationService.UpdateOrganization(organizationDTO);

                if (result)
                {
                    _logger.LogInformation("Organization updated successfully");
                    return Ok(result);
                }

                return NotFound("Organization not found");
            }
            catch (NullDTOException ex)
            {
                return BadRequest($"Failed to update organization. {ex.Message}");
            }
            catch (OrganizationNotFoundException ex)
            {
                return NotFound($"Failed to update organization. {ex.Message}");
            }
            catch (Exception)
            {
                // Log or handle other unexpected exceptions
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{organizationId}")]
        public IActionResult GetOrganizationById(int organizationId)
        {
            try
            {
                var organizationDTO = _organizationService.GetOrganizationById(organizationId);

                if (organizationDTO != null)
                {
                    _logger.LogInformation("Organization Listed");
                    return Ok(organizationDTO);
                }
                return NotFound("Organization not found");
            }
            catch (OrganizationNotFoundException ex)
            {
                return NotFound($"Failed to get organization. {ex.Message}");
            }
            catch (Exception)
            {
                // Log or handle other unexpected exceptions
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        public IActionResult GetAllOrganizations()
        {
            try
            {
                var organizationDTOs = _organizationService.GetAllOrganizations();

                _logger.LogInformation("OrganizationController Listed");

                return Ok(organizationDTOs);
            }
            catch (Exception)
            {
                // Log or handle other unexpected exceptions
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
