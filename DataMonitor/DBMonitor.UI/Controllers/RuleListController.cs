using DBMonitor.APIClient;
using DBMonitor.BLL;
using DBMonitor.UI.Models;

using Microsoft.AspNetCore.Mvc;

namespace DBMonitor.UI.Controllers
{
    [Route("[controller]")]
    public class RuleListController : Controller
    {
        private readonly ILogger<RuleListController> _logger;
        private readonly IDataMonitorAPIClient _client;

        public RuleListController(ILogger<RuleListController> logger, IDataMonitorAPIClient client)
        {
            _logger = logger;
            _client = client;
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {

            var data = await _client.RuleAPIService.GetRules();
            return View(new RuleListViewModel()
            {
                RuleList = data.Select(x => new RuleViewModel(x)).ToList()
            });

        }
        [HttpGet("Edit/{id}")]
        public async Task<ActionResult> EditRule(int id)
        {

            var data = await _client.RuleAPIService.GetRule(id);
            return View(new RuleViewModel(data));
        }
        [HttpGet("Create")]
        public async Task<ActionResult> CreateRule() => View(new RuleViewModel(new Rule()));
        [HttpPost("Create")]
        public async Task<ActionResult> CreateRule([FromForm] Rule model)
        {
            try
            {

                await _client.RuleAPIService.CreateRule(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost("Edit/{id}")]
        public async Task<ActionResult> EditRule([FromForm] Rule model)
        {
            try
            {

                await _client.RuleAPIService.UpdateRule(model.Id, model);

                return RedirectToAction("Index");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("Delete/{id:int}")]
        public async Task<ActionResult> DeleteRule([FromRoute] int id)
        {
            try
            {
                await _client.RuleAPIService.DeleteRule(id);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return RedirectToAction("Index");

        }

    }
}
