using System.Data.Entity;

using DataAccess.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementations
{
    public class EmployeeRepository(AppDbContext dbContext) : GenericRepository<Employee>(dbContext), ILinkedRepository<Employee>
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<bool> CascadeDeleteAsync(Employee employee)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                // Delete related projects directly in the database
                await _dbContext.Database.ExecuteSqlCommandAsync("DELETE FROM Projects WHERE ManagerID = {0}", employee.ID);
                _dbContext.Set<Employee>().Remove(employee);
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
