using ComplaintTicketApp.Contexts;
using ComplaintTicketApp.Interfaces;
using ComplaintTicketApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ComplaintTicketApp.Repositories
{
    public class AnalyticsRepository : IRepository<int, Analytics>
    {
        private readonly ComplaintTicketContext _dbContext;
        public AnalyticsRepository(ComplaintTicketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Analytics Add(Analytics entity)
        {
            _dbContext.Analytics.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Analytics Delete(int AnalyticsId)
        {
            var analytics = GetById(AnalyticsId);
            if (analytics != null)
            {
                _dbContext.Analytics.Remove(analytics);
                _dbContext.SaveChanges();
                return analytics;
            }
            return null;
        }

        public IList<Analytics> GetAll()
        {
            if (_dbContext.Analytics.Count() == 0)
                return null;
            return _dbContext.Analytics.ToList();
        }

        public Analytics GetById(int key)
        {
            var analytics = _dbContext.Analytics.SingleOrDefault(u => u.AnalyticsId == key);
            return analytics;
        }

        public Analytics Update(Analytics entity)
        {
            var analytics = GetById(entity.AnalyticsId);
            if (analytics != null)
            {
                _dbContext.Entry(analytics).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return analytics;

            }
            return null;
        }
    }
}

