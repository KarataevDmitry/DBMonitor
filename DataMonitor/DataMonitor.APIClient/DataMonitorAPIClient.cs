namespace DBMonitor.APIClient
{
    public class DataMonitorAPIClient : IDataMonitorAPIClient
    {
        public IRuleAPIService RuleAPIService { get; }
        public ILaunchHistoryAPIService LaunchHistoryAPIService { get; }

        public DataMonitorAPIClient(string apiUrl) :
            this(new RuleAPIService(apiUrl),
                new LaunchHistoryAPIService(apiUrl))
        { }
        public DataMonitorAPIClient(IRuleAPIService ruleAPIService, ILaunchHistoryAPIService launchHistoryAPIService)
        {
            RuleAPIService = ruleAPIService;
            LaunchHistoryAPIService = launchHistoryAPIService;
        }

    }
}
