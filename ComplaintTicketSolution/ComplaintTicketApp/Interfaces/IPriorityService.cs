using System.Collections.Generic;
using ComplaintTicketApp.Models;

namespace ComplaintTicketApp.Interfaces
{
    public interface IPriorityService
    {
        // Get a priority by its ID
        Priority GetPriorityById(int priorityId);

        // Get all priority levels
        IEnumerable<Priority> GetAllPriorities();

        // Add a new priority
        Priority AddPriority(Priority priority);

        // Update an existing priority
        Priority UpdatePriority(Priority priority);

        // Remove a priority by its ID
        Priority RemovePriority(int priorityId);

        // Other methods as needed
    }
}
