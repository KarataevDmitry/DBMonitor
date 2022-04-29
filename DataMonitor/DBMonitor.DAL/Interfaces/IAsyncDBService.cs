
namespace DBMonitor.DAL.Interfaces
{
    public interface IAsyncDBService<T> where T : class
    {
        Task<int> AddAsync(T item);
        Task DeleteAsync(int Id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(int Id);
        Task SaveAsync();
        Task<T> EditAsync(T changed);
    }
}