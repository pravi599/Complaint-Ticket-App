using ComplaintTicketApp.Contexts;
using ComplaintTicketApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketApp.Repositories
{
    public class ComplaintRepository : IRepository<int, Complaint>
    {
        private readonly ComplaintTicketContext _dbContext;
        public ComplaintRepository(ComplaintTicketContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Complaint GetById(int key)
        {
            var complaint = _dbContext.Complaints.SingleOrDefault(u => u.ComplaintId == key);
            return complaint;
        }

        public IList<Complaint> GetAll()
        {
            if (_dbContext.Complaints.Count() == 0)
                return null;
            return _dbContext.Complaints.ToList();
        }

        public Complaint Add(Complaint entity)
        {
            _dbContext.Complaints.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Complaint Update(Complaint entity)
        {
            var complaint = GetById(entity.ComplaintId);
            if (complaint != null)
            {
                _dbContext.Entry(complaint).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return complaint;

            }
            return null;
        }

        public Complaint Delete(int ComplaintId)
        {
            var complaint = GetById(ComplaintId);
            if (complaint != null)
            {
                // Delete associated Tracking and Priority
                var tracking = _dbContext.Trackings.SingleOrDefault(t => t.ComplaintId == ComplaintId);
                var priority = _dbContext.Priorities.SingleOrDefault(p => p.ComplaintId == ComplaintId);

                if (tracking != null)
                {
                    _dbContext.Trackings.Remove(tracking);
                }

                if (priority != null)
                {
                    _dbContext.Priorities.Remove(priority);
                }

                _dbContext.Complaints.Remove(complaint);
                _dbContext.SaveChanges();

                return complaint;
            }
            return null;
        }
        public void DeleteByComplaintId(int complaintId)
        {
            var tracking = _dbContext.Trackings.SingleOrDefault(t => t.ComplaintId == complaintId);

            if (tracking != null)
            {
                _dbContext.Trackings.Remove(tracking);
                _dbContext.SaveChanges();
            }
        }
    }
}

