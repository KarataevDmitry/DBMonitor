using System.Collections.Concurrent;

using DBMonitor.APIClient;
using DBMonitor.BLL;

using Quartz;

namespace DBMonitor.RulesRunner.Jobs
{
    [DisallowConcurrentExecution]
    public class RuleEnqueueJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Rule exec job fired");
            var ruleQueue = (ConcurrentQueue<Rule>)context.JobDetail.JobDataMap["Queue"];
            var apiService = (IDataMonitorAPIClient)context.JobDetail.JobDataMap["API"];
            var sched = (IScheduler)context.JobDetail.JobDataMap["Scheduler"];
            var connectionString = (string)context.JobDetail.JobDataMap["ConnectionString"];
            var rules = (await apiService.RuleAPIService.GetRules()).Where(x => !x.DeletedAt.HasValue).ToList();
            foreach (var rule in rules)
            {
                ruleQueue.Enqueue(rule);
                var trigger = TriggerBuilder
                            .Create()
                            .StartNow()
                            .WithCronSchedule(rule.RunAt)
                            .Build();
                var job = JobBuilder.Create<QueryExecuteJob>()
                    .WithIdentity("queryExec")
                    .UsingJobData(new JobDataMap((IDictionary<string, object>)new Dictionary<string, object>() { { "API", apiService }, { "Rule", rule }, { "ConnectionString", connectionString } }))
                    .Build();
            }
        }
    }
}
