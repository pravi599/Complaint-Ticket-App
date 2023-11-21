using ComplaintTicketApplication.Contexts;
using ComplaintTicketApplication.Interfaces;
using ComplaintTicketApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;



namespace ComplaintTicketApplication.Repositories
{
    public class OrganizationRepository : IRepository<int, Organization>
    {
        private readonly ComplaintTicketContext _dbContext;
        public OrganizationRepository(ComplaintTicketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Organization Add(Organization entity)
        {
            _dbContext.Organizations.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Organization Delete(int OrganizationId)
        {
            var organization = GetById(OrganizationId);
            if (organization != null)
            {
                _dbContext.Organizations.Remove(organization);
                _dbContext.SaveChanges();
                return organization;
            }
            return null;
        }

        public IList<Organization> GetAll()
        {


            if (_dbContext.Organizations.Count() == 0)
                return null;
            return _dbContext.Organizations.ToList();
        }

        public Organization GetById(int key)
        {
            var organization = _dbContext.Organizations.SingleOrDefault(u => u.OrganizationId == key);
            return organization;
        }

        public Organization Update(Organization entity)
        {
            var organization = GetById(entity.OrganizationId);
            if (organization != null)
            {
                _dbContext.Entry(organization).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return organization;

            }
            return null;
        }
    }
}