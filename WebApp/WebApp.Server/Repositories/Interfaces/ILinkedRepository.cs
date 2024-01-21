namespace WebAPI.Services.Interfaces
{
    public interface ILinkedRepository<T> : IGenericRepository<T>
    {
        Task<bool> CascadeDeleteAsync(T entity);
    }
}
