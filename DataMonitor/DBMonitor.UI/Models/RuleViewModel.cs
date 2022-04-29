using System.Diagnostics.CodeAnalysis;

using DBMonitor.BLL;

namespace DBMonitor.UI.Models
{
    public class RuleViewModel
    {
        public int Id { get; set; }
        [AllowNull]
        public string QueryText { get; set; }
        [AllowNull]
        public string AddedByUser { get; set; }
        [AllowNull]
        public string RunAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? LastLaunchDate { get; }

        public bool IsActive => !DeletedAt.HasValue;
        public string Name { get; set; }
        public string Description { get; set; }

        public RuleViewModel(Rule rule)
        {
            Id = rule.Id;
            RunAt = rule.RunAt;
            QueryText = rule.QueryText;
            AddedByUser = rule.AddedByUser;
            Description = rule.Description;
            Name = rule.Name;
            DeletedAt = rule.DeletedAt;
            LastLaunchDate = rule.LastLaunch;
        }


    }
}
