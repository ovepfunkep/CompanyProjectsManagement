using System.Data.Entity;

using DataAccess.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementations
{
    public class CompanyRepository(AppDbContext dbContext) : GenericRepository<Company>(dbContext), ILinkedRepository<Company>
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<bool> CascadeDeleteAsync(Company company)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                var linkedEmployees = _dbContext.Set<Employee>()
                                                                 .Where(e => e.CompanyID == company.ID);

                var linkedProjects = _dbContext.Set<Project>()
                                                               .Where(p => p.ContractorCompanyID == company.ID || 
                                                                                 p.CustomerCompanyID == company.ID ||
                                                                                 linkedEmployees.Any(e => e.ID == p.ManagerID));
                _dbContext.Set<Project>().RemoveRange(linkedProjects);
                _dbContext.Set<Employee>().RemoveRange(linkedEmployees);
                _dbContext.Set<Company>().Remove(company);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();

                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
