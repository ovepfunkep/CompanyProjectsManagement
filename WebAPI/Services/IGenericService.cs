namespace WebAPI.Repositories
{
    public interface IGenericService<T>
    {
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetAsync(int id);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }

}
