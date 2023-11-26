using ComplaintTicketApp.Models;
using System.Runtime.Serialization;

namespace ComplaintTicketApp.Exceptions
{
    [Serializable]
    public class NullDTOException : Exception
    { string message;
        public NullDTOException()
        {
            message = "DTO cannot be null.";
        }
        public override string Message => message;
    }
}