using ComplaintTicketApp.Models;
using System.Runtime.Serialization;

namespace ComplaintTicketApp.Exceptions
{
    [Serializable]
    public class TrackingNotFoundException : Exception
    {
        string message;
        public TrackingNotFoundException()
        {
            message ="Tracking with Given TrackingId not found.";
        }

        public override string Message => message;
    }
}