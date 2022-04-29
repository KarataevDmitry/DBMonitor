using DBMonitor.BLL;

namespace DBMonitor.APIClient
{
    public interface IRuleAPIService
    {
        Task<Rule> CreateRule(Rule rule);
        Task<Rule> GetRule(int id);
        Task<IEnumerable<Rule>> GetRules();
        Task UpdateRule(int id, Rule rule);
        Task DeleteRule(int id);
    }
}