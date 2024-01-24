using System.Data.Entity;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementations
{
    public class GenericRepository<T>(AppDbContext dbContext) : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAsync() => await _dbContext.Set<T>().ToListAsync();

        public async Task<T> GetAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
