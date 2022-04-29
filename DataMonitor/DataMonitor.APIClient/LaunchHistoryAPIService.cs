using DBMonitor.BLL;

using Microsoft.Extensions.Configuration;

using RestSharp;

namespace DBMonitor.APIClient
{
    public class LaunchHistoryAPIService : ILaunchHistoryAPIService
    {
        private const string JSON_MIME_TYPE = "application/json";
        private readonly RestClient _client;
        private readonly IConfiguration _configuration;

        public LaunchHistoryAPIService(IConfiguration configuration)
        {

            _configuration = configuration;
            _client = new RestClient(_configuration["API_URL"]);

        }
        public async Task<LaunchHistory> Create(LaunchHistoryDTO rule)
        {
            var req = new RestRequest($"api/LaunchHistory/Create");
            req.AddBody(rule, JSON_MIME_TYPE);

            return await _client.PostAsync<LaunchHistory>(req);
        }
        public LaunchHistoryAPIService(string apiUrl)
        {
            _client = new RestClient(apiUrl);
        }
        public async Task<IEnumerable<LaunchHistory>> GetLaunchHistoriesAsync()
        {
            var req = new RestRequest("api/LaunchHistory/GetAll");
            try
            {
                var data = await _client.GetAsync<IEnumerable<LaunchHistory>>(req);
                return data;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<LaunchHistory> GetLaunchHistoryAsync(int id)
        {
            var req = new RestRequest($"api/LaunchHistory/Get/{id}");

            try
            {
                var data = await _client.GetAsync<LaunchHistory>(req);
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateLaunchHistoryAsync(int id, LaunchHistory launchHistory)
        {
            var request = new RestRequest("api/LaunchHistory/Edit");
            request.AddBody(launchHistory, JSON_MIME_TYPE);

            try
            {
                var response = await _client.PostAsync(request);
                return;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task DeleteLaunchHistoryAsync(int id)
        {
            var rq = new RestRequest($"api/LaunchHistory/{id}");
            try
            {
                await _client.DeleteAsync(rq);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
