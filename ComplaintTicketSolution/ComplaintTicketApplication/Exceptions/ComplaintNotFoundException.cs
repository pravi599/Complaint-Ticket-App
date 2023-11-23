using System.Runtime.Serialization;

namespace ComplaintTicketApplication.Exceptions
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