using DBMonitor.BLL;

namespace DBMonitor.APIClient
{
    public class LaunchHistoryDTO
    {
        public int RuleId { get; set; }
        public string Name { get; set; }
        public LaunchResult Result { get; set; }
        public DateTime LaunchedAt { get; set; }
        public TimeSpan ExecutionTime { get; set; }
    }
}