/*using ComplaintTicketApp.Contexts;
using ComplaintTicketApp.Interfaces;
using ComplaintTicketApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ComplaintTicketApp.Repositories
{
    public class CategoryRepository : IRepository<int, ComplaintCategory>
    {
        private readonly ComplaintTicketContext _dbContext;
        public CategoryRepository(ComplaintTicketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ComplaintCategory Add(ComplaintCategory entity)
        {
            _dbContext.ComplaintCategories.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public ComplaintCategory Delete(int ComplaintCategoryId)
        {
            var category = GetById(ComplaintCategoryId);
            if (category != null)
            {
                _dbContext.ComplaintCategories.Remove(category);
                _dbContext.SaveChanges();
                return category;
            }
            return null;
        }

        public IList<ComplaintCategory> GetAll()
        {
            if (_dbContext.ComplaintCategories.Count() == 0)
                return null;
            return _dbContext.ComplaintCategories.ToList();
        }

        public ComplaintCategory GetById(int key)
        {
            var category = _dbContext.ComplaintCategories.SingleOrDefault(u => u.ComplaintCategoryId == key);
            return category;
        }

        public ComplaintCategory Update(ComplaintCategory entity)
        {
            var category = GetById(entity.ComplaintCategoryId);
            if (category != null)
            {
                _dbContext.Entry(category).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return category;

            }
            return null;
        }
    }
}
*/
