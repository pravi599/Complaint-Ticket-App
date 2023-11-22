using ComplaintTicketApplication.Contexts;
using ComplaintTicketApplication.Interfaces;
using ComplaintTicketApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace ComplaintTicketApplication.Repositories
{
    public class PriorityRepository : IRepository<int, Priority>
    {
        private readonly ComplaintTicketContext _dbContext;
        public PriorityRepository(ComplaintTicketContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Priority GetById(int Key)
        {
            var priority = _dbContext.Priorities.SingleOrDefault(u => u.PriorityId == Key);
            return priority;
        }

        public IList<Priority> GetAll()
        {
            if (_dbContext.Priorities.Count() == 0)
                return null;
            return _dbContext.Priorities.ToList();
        }

        public Priority Add(Priority entity)
        {
            if (entity.Complaint != null)
            {
                entity.ComplaintId = entity.Complaint.ComplaintId;
            }

            _dbContext.Priorities.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Priority Update(Priority entity)
        {
            var priority = GetById(entity.PriorityId);
            if (priority != null)
            {
                _dbContext.Entry(priority).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return priority;
            }
            return null;
        }
        public Priority Delete(int PriorityId)
        {
            var priority = GetById(PriorityId);
            if (priority != null)
            {
                _dbContext.Priorities.Remove(priority);
                _dbContext.SaveChanges();
                return priority;
            }
            return null;
        }
    }
}