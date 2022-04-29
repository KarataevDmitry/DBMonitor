using DBMonitor.BLL;

using Microsoft.Extensions.Configuration;

using RestSharp;

namespace DBMonitor.APIClient
{
    public class RuleAPIService : IRuleAPIService
    {
        private const string JSON_MIME_TYPE = "application/json";
        private readonly RestClient _client;
        private readonly IConfiguration _configuration;

        public RuleAPIService(string apiUrl)
        {
            _client = new RestClient(apiUrl);
        }
        public RuleAPIService(IConfiguration configuration)
        {

            _configuration = configuration;
            _client = new RestClient(_configuration["API_URL"]);

        }
        public async Task<IEnumerable<Rule>> GetRules()
        {
            var req = new RestRequest("api/Rule/GetAll");
            var data = await _client.GetAsync<IEnumerable<Rule>>(req);
            return data;
        }
        public async Task<Rule> CreateRule(Rule rule)
        {
            var req = new RestRequest($"api/Rule/Create");
            req.AddBody(rule, JSON_MIME_TYPE);

            return await _client.PostAsync<Rule>(req);
        }
        public async Task<Rule> GetRule(int id)
        {
            var req = new RestRequest($"api/Rule/Get/{id}");

            return await _client.GetAsync<Rule>(req);
        }

        public async Task UpdateRule(int id, Rule rule)
        {

            var request = new RestRequest("api/Rule/Edit");
            request.AddBody(rule, JSON_MIME_TYPE);

            try
            {
                var response = await _client.PostAsync(request);
                return;
            }
            catch
            {
                throw;
            }



        }
        public async Task DeleteRule(int id)
        {
            var rq = new RestRequest($"api/Rule/Delete/{id}");
            await _client.DeleteAsync(rq);
        }
    }
}
