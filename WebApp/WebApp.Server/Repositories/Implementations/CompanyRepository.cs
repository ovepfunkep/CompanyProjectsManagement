using System.Data.Entity;

using DataAccess.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementations
{
    public class CompanyRepository(AppDbContext dbContext) : GenericRepository<Company>(dbContext), ILinkedRepository<Company>
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task CascadeDeleteAsync(int id)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                var linkedEmployees = _dbContext.Set<Employee>()
                                                                 .Where(e => e.CompanyID == id);

                var linkedProjects = _dbContext.Set<Project>()
                                                               .Where(p => p.ContractorCompanyID == id || 
                                                                                 p.CustomerCompanyID == id ||
                                                                                 linkedEmployees.Any(e => e.ID == p.ManagerID));
                _dbContext.Set<Project>().RemoveRange(linkedProjects);
                _dbContext.Set<Employee>().RemoveRange(linkedEmployees);
                _dbContext.Set<Company>().Remove(_dbContext.Set<Company>().Find(id));
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
