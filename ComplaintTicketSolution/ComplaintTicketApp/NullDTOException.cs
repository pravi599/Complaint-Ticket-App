using ComplaintTicketApp.Models;
using System.Runtime.Serialization;

namespace ComplaintTicketApplication.Exceptions
{
    [Serializable]
    internal class NullDTOException : Exception
    {
        string message;
        public NullDTOException()
        {
            message = "OrganizationDTO cannot be null.";
        }
        public override string Message => message;
    }
}