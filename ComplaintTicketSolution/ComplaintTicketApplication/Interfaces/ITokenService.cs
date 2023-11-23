using ComplaintTicketApplication.Models.DTOs;
using ComplaintTicketApplication.Models.DTOs;

namespace ComplaintTicketApplication.Interfaces
{
    public interface ITokenService
    {
        string GetToken(UserDTO user);
    }
}