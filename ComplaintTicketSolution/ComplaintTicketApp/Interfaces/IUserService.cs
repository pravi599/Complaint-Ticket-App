using ComplaintTicketApp.Models.DTOs;

namespace ComplaintTicketApp.Interfaces
{
    public interface IUserService
    {
        UserDTO Login(UserDTO userDTO);
        UserDTO Register(UserDTO userDTO);
    }
}