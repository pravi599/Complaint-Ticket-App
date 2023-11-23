using System.Collections.Generic;
using ComplaintTicketApp.Interfaces;
using ComplaintTicketApp.Models;
using ComplaintTicketApp.Repositories;

namespace ComplaintTicketApp.Services
{
    public class PriorityService : IPriorityService
    {
        private readonly IRepository<int, Priority> _priorityRepository;

        public PriorityService(IRepository<int, Priority> priorityRepository)
        {
            _priorityRepository = priorityRepository;
        }

        public Priority GetPriorityById(int priorityId)
        {
            return _priorityRepository.GetById(priorityId);
        }

        public IEnumerable<Priority> GetAllPriorities()
        {
            return _priorityRepository.GetAll();
        }

        public Priority AddPriority(Priority priority)
        {
            return _priorityRepository.Add(priority);
        }

        public Priority UpdatePriority(Priority priority)
        {
            return _priorityRepository.Update(priority);
        }

        public Priority RemovePriority(int priorityId)
        {
            return _priorityRepository.Delete(priorityId);
        }

        // You can add more methods as needed for specific business logic or operations
    }
}
