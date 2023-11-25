using System.Runtime.Serialization;
using System.Security.Policy;

namespace ComplaintTicketApp.Exceptions
{
    [Serializable]
    public class TrackingUpdateException : Exception
    {
        string message;
        public TrackingUpdateException()
        {
            message = "Failed to update tracking status.";
        }
        public override string Message => Message;
    }
}