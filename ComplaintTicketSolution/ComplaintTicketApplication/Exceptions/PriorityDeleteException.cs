namespace ComplaintTicketApplication.Exceptions
{
    public class PriorityDeleteException : Exception
    {
        string message;
        public PriorityDeleteException()
        {
            message = "unable to delete the priority , 500, \"Internal server error\" ";
        }
        public override string Message => message;
    }
}
