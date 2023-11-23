using ComplaintTicketApplication.Models.DTOs;
using ComplaintTicketApplication.Models.DTOs;

namespace ComplaintTicketApplication.Interfaces
{
    public interface IUserService
    {
        UserDTO Login(UserDTO userDTO);
        UserDTO Register(UserDTO userDTO);
    }
}