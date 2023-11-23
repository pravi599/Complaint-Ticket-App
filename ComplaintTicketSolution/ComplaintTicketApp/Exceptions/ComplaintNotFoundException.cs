using System.Runtime.Serialization;

namespace ComplaintTicketApp.Exceptions
{
    [Serializable]
    internal class ComplaintNotFoundException : Exception
    {
        string message;
        public ComplaintNotFoundException()
        {
            message = "Complaint not found.";
        }

        public override string Message => message;
    }
}