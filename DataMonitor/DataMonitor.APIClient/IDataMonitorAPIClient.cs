namespace DBMonitor.APIClient
{
    public interface IDataMonitorAPIClient
    {
        public IRuleAPIService RuleAPIService { get; }
        public ILaunchHistoryAPIService LaunchHistoryAPIService { get; }

    }
}