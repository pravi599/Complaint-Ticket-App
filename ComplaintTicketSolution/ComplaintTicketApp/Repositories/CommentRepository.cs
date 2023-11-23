using ComplaintTicketApp.Contexts;
using ComplaintTicketApp.Interfaces;
using ComplaintTicketApp.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace ComplaintTicketApp.Repositories
{
    public class CommentRepository : IRepository<int, Comment>
    {
        private readonly ComplaintTicketContext _dbContext;
        public CommentRepository(ComplaintTicketContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Comment GetById(int Key)
        {
            var comment = _dbContext.Comments.SingleOrDefault(u => u.CommentId == Key);
            return comment;
        }

        public IList<Comment> GetAll()
        {
            if (_dbContext.Comments.Count() == 0)
                return null;
            return _dbContext.Comments.ToList();
        }

        public Comment Add(Comment entity)
        {
            _dbContext.Comments.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Comment Update(Comment entity)
        {
            var comment = GetById(entity.CommentId);
            if (comment != null)
            {
                _dbContext.Entry(comment).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return comment;

            }
            return null;
        }
        public Comment Delete(int CommentId)
        {
            var comment = GetById(CommentId);
            if (comment != null)
            {
                _dbContext.Comments.Remove(comment);
                _dbContext.SaveChanges();
                return comment;
            }
            return null;
        }
    }
}
