namespace WebAPI.Services.Interfaces
{
    public interface ILinkedRepository<T> : IGenericRepository<T>
    {
        Task CascadeDeleteAsync(int id);
    }
}
