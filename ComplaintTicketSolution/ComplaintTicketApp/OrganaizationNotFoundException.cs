using ComplaintTicketApplication.Models;
using System.Runtime.Serialization;

namespace ComplaintTicketApplication.Exceptions
{
    [Serializable]
    internal class OrganizationNotFoundException : Exception
    {
        string message;
        public OrganizationNotFoundException()
        {
            message = $"Organization with Given ID not found.";
        }
        public override string Message => message;

    }
}