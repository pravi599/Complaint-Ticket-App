// PriorityService.cs
using System.Collections.Generic;
using ComplaintTicketApplication.Exceptions;
using ComplaintTicketApplication.Interfaces;
using ComplaintTicketApplication.Models;
using ComplaintTicketApplication.Repositories;

namespace ComplaintTicketApplication.Services
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
            var priority = _priorityRepository.GetById(priorityId);

            if (priority == null)
            {
                throw new PriorityNotFoundException();
            }

            return priority;
        }

        public IEnumerable<Priority> GetAllPriorities()
        {
            return _priorityRepository.GetAll();
        }

        public Priority AddPriority(Priority priority)
        {
            try
            {
                return _priorityRepository.Add(priority);
            }
            catch (Exception ex)
            {
                // Log or handle the specific exception as needed               
                throw new PriorityAddException();
            }
        }

        public Priority UpdatePriority(Priority priority)
        {
            try
            {
                return _priorityRepository.Update(priority);
            }
            catch (Exception ex)
            {
                // Log or handle the specific exception as needed              
                throw new PriorityUpdateException();
            }
        }

        public Priority RemovePriority(int priorityId)
        {
            try
            {
                return _priorityRepository.Delete(priorityId);
            }
            catch (Exception ex)
            {
                // Log or handle the specific exception as needed                
                throw new PriorityDeleteException();
            }
        }

    }
}
