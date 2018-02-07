using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppTracker.Models.DB;
using AppTracker.Models.Repositories.Interfaces;

namespace AppTracker.Controllers
{
    [Produces("application/json")]
    [Route("api/Applications/{appId}/Status")]
    public class ApplicationStatusController : Controller
    {
        private readonly IApplicationStatusRepo _statusRepo;

        public ApplicationStatusController(IApplicationStatusRepo statusRepo)
        {
            _statusRepo = statusRepo;
        }

        // GET: api/Applications/1/Status
        [HttpGet]
        public IActionResult GetAllApplicationStatus([FromRoute] int appId)
        {
            var statusList = _statusRepo.GetAllApplicationStatus(appId);

            return Ok(statusList);
        }

        // GET: api/Applications/1/Status/1
        [HttpGet("{statusId}")]
        public IActionResult GetApplicationStatus([FromRoute] int appId, [FromRoute] int statusId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var status = _statusRepo.GetId(statusId);

            if (status == null)
            {
                return NotFound();
            }
            else if (status.ApplicationId != appId)
            {
                return BadRequest();
            }

            return Ok(status);
        }

        // PUT: api/Applications/1/Status/5
        [HttpPut("{statusId}")]
        public IActionResult PutApplicationStatus([FromRoute] int appId, [FromRoute] int statusId, [FromBody] ApplicationStatus applicationStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (statusId != applicationStatus.Id
                || applicationStatus.ApplicationId != appId)
            {
                return BadRequest();
            }

            if (!_statusRepo.StatusExists(statusId))
            {
                return NotFound();
            }

            if (!_statusRepo.EditStatus(statusId, applicationStatus))
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Applications/1/Status
        [HttpPost]
        public IActionResult PostApplicationStatus([FromRoute] int appId, [FromBody] ApplicationStatus applicationStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            applicationStatus.ApplicationId = appId;

            var dto = _statusRepo.CreateStatus(applicationStatus);

            if (dto == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetApplicationStatus", 
                                    new { appId = dto.ApplicationId, statusId = dto.Id }, 
                                    dto);
        }

        // DELETE: api/Application/1/Status/5
        [HttpDelete("{statusId}")]
        public IActionResult DeleteApplicationStatus([FromRoute] int appId ,[FromRoute] int statusId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var status = _statusRepo.GetId(statusId);

            if (status.ApplicationId != appId)
            {
                return BadRequest();
            }

            var removed = _statusRepo.DeleteStatus(statusId);

            if (removed == null)
            {
                return NotFound();
            }

            return Ok(removed);
        }
    }
}