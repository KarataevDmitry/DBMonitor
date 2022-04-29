using DBMonitor.BLL;
using DBMonitor.DAL.Interfaces;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DBMonitor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleController : ControllerBase
    {
        private readonly IDBService<Rule> _rulesDb;
        private readonly IValidator<Rule> _ruleValidator;

        // GET: api/<LaunchHistoryController>
        [HttpGet("GetAll")]
        public IEnumerable<Rule> Get() => _rulesDb.GetAll();


        // GET api/<LaunchHistoryController>/5
        [HttpGet("Get/{id}")]
        public IActionResult Get(int id)
        {
            var ent = _rulesDb.Get(id);
            return ent == null ? NotFound(ent) : Ok(ent);
        }

        // POST api/<LaunchHistoryController>
        [HttpPost("Edit")]
        public IActionResult Post([FromBody] Rule value)
        {
            Rule ent = null;
            if (!_ruleValidator.Validate(value).IsValid)
            {
                return BadRequest("Validation error");
            }
            ent = _rulesDb.Edit(value);
            return Ok(ent);


        }
        [HttpPost("Create")]
        public IActionResult CreateRule([FromBody] Rule value)
        {

            var createdId = _rulesDb.Add(value);

            if (!_ruleValidator.Validate(value).IsValid)
            {
                return BadRequest("ValidationError");
            }

            try
            {
                _rulesDb.Save();
                return Created($"/Get/{createdId}", _rulesDb.Get(createdId));
            }
            catch (Exception)
            {

                return UnprocessableEntity(value);

            }
        }

        [HttpPut("CreateOrUpdate/{id}")]
        public IActionResult Put(int id, [FromBody] Rule value)
        {
            var ent = _rulesDb.Get(id);
            var entEdit = value;
            if (!_ruleValidator.Validate(value).IsValid)
            {
                return BadRequest("Validation error");
            }
            if (entEdit == null)
            {
                return NoContent();
            }

            if (ent == null)
            {
                var createdId = _rulesDb.Add(entEdit);
                return Created($"/Get/{createdId}", entEdit);
            }

            ent.AddedByUser = entEdit.AddedByUser;
            ent.Description = entEdit.Description;
            ent.QueryText = entEdit.QueryText;
            ent.RunAt = entEdit.RunAt;
            try
            {
                _rulesDb.Save();
                return Ok(entEdit);
            }
            catch (Exception)
            {

                return UnprocessableEntity(value);
            }

        }

        // DELETE api/<LaunchHistoryController>/5
        [HttpDelete("Delete/{id}")]
        public void Delete(int id) => _rulesDb.Delete(id);
        public RuleController(IDBService<Rule> service, IValidator<Rule> validator)
        {
            _rulesDb = service;
            _ruleValidator = validator;
        }
    }
}
