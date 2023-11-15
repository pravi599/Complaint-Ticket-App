using ComplaintTicketApp.Models.DTOs;

namespace ComplaintTicketApp.Interfaces
{
    public interface ITokenService
    {
        string GetToken(UserDTO user);
    }
}
