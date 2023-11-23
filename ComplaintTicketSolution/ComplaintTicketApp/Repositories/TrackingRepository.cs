using ComplaintTicketApp.Contexts;
using ComplaintTicketApp.Interfaces;
using ComplaintTicketApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ComplaintTicketApp.Repositories
{
    public class TrackingRepository : IRepository<int, Tracking>
    {
        private readonly ComplaintTicketContext _dbContext;
        public TrackingRepository(ComplaintTicketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Tracking Add(Tracking entity)
        {
            if (entity.Complaint != null)
            {
                entity.ComplaintId = entity.Complaint.ComplaintId;
            }

            _dbContext.Trackings.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Tracking Delete(int TrackingId)
        {
            var tracking = GetById(TrackingId);
            if (tracking != null)
            {
                _dbContext.Trackings.Remove(tracking);
                _dbContext.SaveChanges();
                return tracking;
            }
            return null;
        }

        public IList<Tracking> GetAll()
        {
            if (_dbContext.Trackings.Count() == 0)
                return null;
            return _dbContext.Trackings.ToList();
        }

        public Tracking GetById(int key)
        {
            var tracking = _dbContext.Trackings.SingleOrDefault(u => u.TrackingId == key);
            return tracking;
        }

        public Tracking Update(Tracking entity)
        {
            var tracking = GetById(entity.TrackingId);
            if (tracking != null)
            {
                _dbContext.Entry(tracking).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return tracking;

            }
            return null;
        }
    }
}