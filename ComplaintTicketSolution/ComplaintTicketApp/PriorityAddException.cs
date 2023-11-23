using ComplaintTicketApplication.Models;
using System.Runtime.Serialization;

namespace ComplaintTicketApplication.Exceptions
{
    [Serializable]
    internal class PriorityAddException : Exception
    {
        string message;
        public PriorityAddException()
        {
            message = $"Unable to add the priority.";
        }
        public override string Message => message;

    }
}