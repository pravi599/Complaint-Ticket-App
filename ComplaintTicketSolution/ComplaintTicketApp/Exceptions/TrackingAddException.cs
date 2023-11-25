using System.Runtime.Serialization;

namespace ComplaintTicketApp.Exceptions
{
    [Serializable]
    public class TrackingAddException : Exception
    {
        string message;
        public TrackingAddException()
        {
            message = "Failed to add tracking.";
        }
        public override string Message => message;
    }
}