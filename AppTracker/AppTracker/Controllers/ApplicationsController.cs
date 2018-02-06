using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AppTracker.Models.DB;
using AppTracker.Models.Repositories.Interfaces;
using AppTracker.Models.DTO;

namespace AppTracker.Controllers
{
    [Produces("application/json")]
    [Route("api/Applications")]
    public class ApplicationsController : Controller
    {
        private readonly IApplicationRepo _appRepo;
        private readonly IContactRepo _contactRepo;

        public ApplicationsController(IApplicationRepo appRepo, IContactRepo contactRepo)
        {
            _appRepo = appRepo;
            _contactRepo = contactRepo;
        }

        // GET: api/Applications
        [HttpGet]
        public IEnumerable<ApplicationDTO> GetApplication()
        {
            return _appRepo.GetAll();
        }

        // GET: api/Applications/5
        [HttpGet("{id}")]
        public IActionResult GetApplication([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = _appRepo.GetId(id);

            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }

        // PUT: api/Applications/5
        [HttpPut("{id}")]
        public IActionResult PutApplication([FromRoute] int id, [FromBody] Application application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_appRepo.ApplicationExists(id))
            {
                return NotFound();
            }

            if (!_appRepo.EditApplication(id, application))
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Applications
        [HttpPost]
        public IActionResult PostApplication([FromBody] Application application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _appRepo.CreateApplication(application);

            if (result == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetApplication", new { id = result.Id }, result);
        }

        // DELETE: api/Applications/5
        [HttpDelete("{id}")]
        public IActionResult DeleteApplication([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _appRepo.DeleteApplication(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        #region Application Contact References
        [HttpGet("{id}/contacts")]
        public IActionResult GetApplicationContacts([FromRoute] int id)
        {
            if (!_appRepo.ApplicationExists(id))
            {
                return NotFound();
            }

            var appContacts = _contactRepo.GetContactsByApplication(id);

            return Ok(appContacts);
        }

        [HttpPut("{appId}/contacts/{contactId}")]
        public IActionResult AddApplicationContactReference([FromRoute] int appId, [FromRoute] int contactId)
        {
            if (!_contactRepo.AddApplicationContactReference(appId, contactId))
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{appId}/contacts/{contactId}")]
        public IActionResult DeleteApplicationContactReference([FromRoute] int appId, [FromRoute] int contactId)
        {
            if (!_contactRepo.DeleteApplictaionContactReference(appId, contactId))
            {
                return BadRequest();
            }

            return Ok();
        } 
        #endregion
    }
}