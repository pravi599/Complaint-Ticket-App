/*using ComplaintTicketApp.Contexts;
using ComplaintTicketApp.Interfaces;
using ComplaintTicketApp.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace ComplaintTicketApp.Repositories
{
    public class AttachmentRepository : IRepository<int, Attachment>
    {
        private readonly ComplaintTicketContext _dbContext;
        public AttachmentRepository(ComplaintTicketContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Attachment GetById(int Key)
        {
            var attachment = _dbContext.Attachments.SingleOrDefault(u => u.AttachmentId == Key);
            return attachment;
        }

        public IList<Attachment> GetAll()
        {
            if (_dbContext.Attachments.Count() == 0)
                return null;
            return _dbContext.Attachments.ToList();
        }

        public Attachment Add(Attachment entity)
        {
            _dbContext.Attachments.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Attachment Update(Attachment entity)
        {
            var attachment = GetById(entity.AttachmentId);
            if (attachment != null)
            {
                _dbContext.Entry(attachment).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return attachment;
            }
            return null;
        }
        public Attachment Delete(int AttachmentId)
        {
            var attachment = GetById(AttachmentId);
            if (attachment != null)
            {
                _dbContext.Attachments.Remove(attachment);
                _dbContext.SaveChanges();
                return attachment;
            }
            return null;
        }
    }
}
*/
