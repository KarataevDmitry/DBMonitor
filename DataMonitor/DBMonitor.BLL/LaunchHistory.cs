using System.Diagnostics.CodeAnalysis;

namespace DBMonitor.BLL
{
    public class LaunchHistory
    {
        [NotNull]
        public int Id { get; set; }
        public virtual Rule Rule { get; set; }
        public int RuleId { get; set; }
        public LaunchResult Result { get; set; }
        public DateTime LaunchedAt { get; set; }
        public TimeSpan ExecutionTime { get; set; }
    }
}
