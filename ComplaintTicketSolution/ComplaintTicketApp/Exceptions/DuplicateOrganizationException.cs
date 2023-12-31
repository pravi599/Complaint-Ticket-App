﻿using System.Runtime.Serialization;

namespace ComplaintTicketApp.Exceptions
{

    public class DuplicateOrganizationException : Exception
    {
        string message;
        public DuplicateOrganizationException()
        {
            message = "Organization with the same name already exists.";
        }

        public override string Message => message;
    }
}