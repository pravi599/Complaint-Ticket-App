namespace ComplaintTicketApplication.Exceptions
{
    public class PriorityUpdateException : Exception
    {
        string message;
        public PriorityUpdateException()
        {
            message = "unable to update the priority , 500, \"Internal server error\" ";
        }
        public override string Message => message;
    }
}
