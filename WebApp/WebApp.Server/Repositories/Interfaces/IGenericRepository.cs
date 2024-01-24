namespace WebAPI.Services.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetAsync(int id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
