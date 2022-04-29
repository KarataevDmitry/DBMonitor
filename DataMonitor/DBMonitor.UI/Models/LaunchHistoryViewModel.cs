using System.Diagnostics.CodeAnalysis;

using DBMonitor.BLL;

namespace DBMonitor.UI.Models
{
    public class LaunchHistoryViewModel
    {
        [AllowNull]
        public RuleViewModel Rule { get; set; }
        public LaunchResult Result { get; set; }
        public DateTime LaunchedAt { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public LaunchHistoryViewModel(LaunchHistory lh)
        {
            Rule = new RuleViewModel(lh.Rule);
            Result = lh.Result;
            LaunchedAt = lh.LaunchedAt;
            ExecutionTime = lh.ExecutionTime;
        }
    }
}
