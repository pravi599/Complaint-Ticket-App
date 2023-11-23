namespace ComplaintTicketApplication.Exceptions
{
    public class PriorityNotFoundException : Exception
    {
        string message;
        public PriorityNotFoundException()
        {
            message = "priority not found, 500, \"Internal server error\" ";
        }
        public override string Message => message;
    }
}
