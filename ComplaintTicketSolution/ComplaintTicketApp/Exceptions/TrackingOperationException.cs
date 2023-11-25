using System.Runtime.Serialization;

namespace ComplaintTicketApp.Exceptions
{
    [Serializable]
    public class TrackingOperationException : Exception
    {
        string message;
        public TrackingOperationException()
        {
            message = "Error adding tracking.";
        }

        public override string Message => Message;
    }
}