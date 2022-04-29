using DBMonitor.APIClient;
using DBMonitor.UI.Models;

using Microsoft.AspNetCore.Mvc;

namespace DBMonitor.UI.Controllers
{
    [Route("[controller]")]
    public class LaunchHistoryController : Controller
    {
        private readonly ILogger<LaunchHistoryController> _logger;
        private readonly IDataMonitorAPIClient _client;

        public LaunchHistoryController(ILogger<LaunchHistoryController> logger, IDataMonitorAPIClient client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {

            var data = await _client.LaunchHistoryAPIService.GetLaunchHistoriesAsync();
            return View(new LaunchHistoryListViewModel()
            {
                LaunchHistoryList = data.Select(x => new LaunchHistoryViewModel(x)).ToList()
            });

        }
    }
}
