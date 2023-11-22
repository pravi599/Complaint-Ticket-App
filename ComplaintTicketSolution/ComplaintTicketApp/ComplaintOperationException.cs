using System.Runtime.Serialization;

namespace ComplaintTicketApplication.Exceptions
{
    [Serializable]
    internal class ComplaintOperationException : Exception
    {
        string message;
        public ComplaintOperationException()
        {
            message = "Error adding complaint";
        }
        public override string Message => message;
    }
}