// See https://aka.ms/new-console-template for more information

using System.Collections.Concurrent;

using DBMonitor.APIClient;
using DBMonitor.BLL;
using DBMonitor.RulesRunner.Jobs;

using Microsoft.Extensions.Configuration;

using Quartz;
using Quartz.Impl;
using Quartz.Logging;
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
var exitEvent = new ManualResetEvent(false);
var APIService = new DataMonitorAPIClient(config["API_URL"]);

Console.CancelKeyPress += (sender, eventArgs) =>
{
    eventArgs.Cancel = true;
    exitEvent.Set();
};
LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

var sf = new StdSchedulerFactory();
var sched = await sf.GetScheduler();
ConcurrentQueue<Rule> rQueue = new ConcurrentQueue<Rule>();
var rules = await APIService.RuleAPIService.GetRules();
var rulesToExecute = rules.Where(x => !x.DeletedAt.HasValue).ToList();
await sched.Start();
foreach (var rule in rulesToExecute)
{

    var d = new Dictionary<string, object>()
    {
        { "Queue", rQueue },
        { "API", APIService },
        {"ConnectionString", config.GetConnectionString("DatabaseConnection")},
        {"Scheduler", sched }
    };

    var jobEnq = JobBuilder.Create<RuleEnqueueJob>()
        .WithIdentity("ruleEnque")
        .UsingJobData(new JobDataMap((IDictionary<string, object>)d))
        .Build();
    var trigger = TriggerBuilder
        .Create()
        .WithSimpleSchedule((s) => s.WithIntervalInSeconds(60))
        .Build();


    await sched.ScheduleJob(jobEnq, trigger);
    exitEvent.WaitOne();

}



internal class ConsoleLogProvider : ILogProvider
{
    public Logger GetLogger(string name)
    {
        return (level, func, exception, parameters) =>
        {
            if (level >= LogLevel.Info && func != null)
            {
                Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + level + "] " + func(), parameters);
            }
            return true;
        };
    }

    public IDisposable OpenNestedContext(string message) => throw new NotImplementedException();

    public IDisposable OpenMappedContext(string key, object value, bool destructure = false) => throw new NotImplementedException();
}










