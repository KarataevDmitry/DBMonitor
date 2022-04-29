
using System.Data.SqlClient;
using System.Diagnostics;

using DBMonitor.APIClient;
using DBMonitor.BLL;

using Quartz;

namespace DBMonitor.RulesRunner.Jobs
{
    internal class QueryExecuteJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(async () =>
            {
                await Console.Out.WriteLineAsync("Exec query job fired");
                var RuleToRun = (Rule)context.JobDetail.JobDataMap["Rule"];
                var apiService = (IDataMonitorAPIClient)context.JobDetail.JobDataMap["API"];
                var connectionString = (string)context.JobDetail.JobDataMap["ConnectionString"];
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                await Console.Out.WriteLineAsync("Connect OK");
                var command = new SqlCommand(RuleToRun.QueryText, connection);
                var sw = Stopwatch.StartNew();
                using var reader = command.ExecuteReader();
                await reader.ReadAsync();
                var val = reader.GetInt32(0);
                sw.Stop();
                var lh = new LaunchHistoryDTO()
                {
                    ExecutionTime = sw.Elapsed,
                    LaunchedAt = DateTime.UtcNow,
                    RuleId = RuleToRun.Id
                };
                lh.Result = val == 0 ? LaunchResult.Success : LaunchResult.Failed;
                await apiService.LaunchHistoryAPIService.Create(lh);
                await Console.Out.WriteLineAsync("launch history entry created");

            });
        }
    }
}
