﻿using ComplaintTicketApp.Models;
using System.Runtime.Serialization;

namespace ComplaintTicketApp.Exceptions
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