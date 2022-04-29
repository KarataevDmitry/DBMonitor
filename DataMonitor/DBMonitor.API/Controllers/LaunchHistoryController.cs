
using DBMonitor.APIClient;
using DBMonitor.BLL;
using DBMonitor.DAL.Interfaces;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DBMonitor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaunchHistoryController : ControllerBase
    {
        private readonly IDBService<LaunchHistory> _launchHistoryDb;
        // GET: api/<LaunchHistoryController>
        [HttpGet("GetAll")]
        public IEnumerable<LaunchHistory> Get()
        {
            var d = _launchHistoryDb.GetAll();
            return d;
        }

        // GET api/<LaunchHistoryController>/5
        [HttpGet("Get/{id}")]
        public IActionResult Get(int id)
        {
            var ent = _launchHistoryDb.Get(id);
            return ent == null ? NotFound(ent) : Ok(ent);
        }

        // POST api/<LaunchHistoryController>
        [HttpPost("Create")]
        public void Create([FromBody] LaunchHistoryDTO value) => _launchHistoryDb.Add(new LaunchHistory() { ExecutionTime = value.ExecutionTime, LaunchedAt = value.LaunchedAt, Result = value.Result, RuleId = value.RuleId });

        // PUT api/<LaunchHistoryController>/5
        [HttpPut("CreateOrUpdate/{id}")]
        public IActionResult Put(int id, [FromBody] LaunchHistoryDTO value)
        {
            var ent = _launchHistoryDb.Get(id);
            var entEdit = value;
            if (entEdit == null)
            {
                return NoContent();
            }

            if (ent == null)
            {
                var createdId = _launchHistoryDb.Add(new LaunchHistory() { ExecutionTime = value.ExecutionTime, LaunchedAt = value.LaunchedAt, Result = value.Result, RuleId = value.RuleId });
                return Created($"/get/{createdId}", entEdit);
            }

            ent.ExecutionTime = entEdit.ExecutionTime;
            ent.LaunchedAt = entEdit.LaunchedAt;
            ent.Result = entEdit.Result;
            ent.RuleId = entEdit.RuleId;
            try
            {
                _launchHistoryDb.Save();
                return Ok(entEdit);
            }
            catch (Exception)
            {

                return UnprocessableEntity(value);
            }

        }

        // DELETE api/<LaunchHistoryController>/5
        [HttpDelete("Delete/{id}")]
        public void Delete(int id) => _launchHistoryDb.Delete(id);
        [HttpPost("Edit")]
        public IActionResult Edit([FromBody] LaunchHistory value)
        {
            var ent = _launchHistoryDb.Edit(value);
            return Ok(ent);

        }
        public LaunchHistoryController(IDBService<LaunchHistory> service)
        {
            _launchHistoryDb = service;
        }
    }
}
