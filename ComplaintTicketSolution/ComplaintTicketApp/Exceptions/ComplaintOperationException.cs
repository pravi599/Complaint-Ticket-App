using System.Runtime.Serialization;

namespace ComplaintTicketApp.Exceptions
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