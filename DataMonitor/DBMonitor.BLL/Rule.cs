namespace DBMonitor.BLL
{
    public class Rule
    {
        public int Id { get; set; }
        public string QueryText { get; set; }
        public string AddedByUser { get; set; }
        public string RunAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? LastLaunch { get; set; } = null;

    }
}