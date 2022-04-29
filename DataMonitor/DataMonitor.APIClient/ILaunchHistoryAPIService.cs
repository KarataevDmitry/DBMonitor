using DBMonitor.BLL;

namespace DBMonitor.APIClient
{
    public interface ILaunchHistoryAPIService
    {
        Task<LaunchHistory> Create(LaunchHistoryDTO rule);
        Task<LaunchHistory> CreateOrUpdate(LaunchHistoryDTO rule);
        Task DeleteLaunchHistoryAsync(int id);
        Task<IEnumerable<LaunchHistory>> GetLaunchHistoriesAsync();
        Task<LaunchHistory> GetLaunchHistoryAsync(int id);
        Task UpdateLaunchHistoryAsync(int id, LaunchHistory rule);

    }
}