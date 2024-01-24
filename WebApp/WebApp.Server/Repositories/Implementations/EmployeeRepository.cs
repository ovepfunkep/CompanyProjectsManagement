using System.Data.Entity;

using DataAccess.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementations
{
    public class EmployeeRepository(AppDbContext dbContext) : GenericRepository<Employee>(dbContext), ILinkedRepository<Employee>
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task CascadeDeleteAsync(int id)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                var linkedProjects = _dbContext.Set<Project>().Where(p => p.ManagerID == id);
                _dbContext.Set<Project>().RemoveRange(linkedProjects);
                _dbContext.Set<Employee>().Remove(_dbContext.Set<Employee>().Find(id));
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
