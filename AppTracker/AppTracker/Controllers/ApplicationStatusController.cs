using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppTracker.Models.DB;

namespace AppTracker.Controllers
{
    [Produces("application/json")]
    [Route("api/ApplicationStatus")]
    public class ApplicationStatusController : Controller
    {
        private readonly AppTrackerDBContext _context;

        public ApplicationStatusController(AppTrackerDBContext context)
        {
            _context = context;
        }

        // GET: api/ApplicationStatus
        [HttpGet]
        public IEnumerable<ApplicationStatus> GetApplicationStatus()
        {
            return _context.ApplicationStatus;
        }

        // GET: api/ApplicationStatus/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationStatus = await _context.ApplicationStatus.SingleOrDefaultAsync(m => m.Id == id);

            if (applicationStatus == null)
            {
                return NotFound();
            }

            return Ok(applicationStatus);
        }

        // PUT: api/ApplicationStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationStatus([FromRoute] int id, [FromBody] ApplicationStatus applicationStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(applicationStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationStatusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ApplicationStatus
        [HttpPost]
        public async Task<IActionResult> PostApplicationStatus([FromBody] ApplicationStatus applicationStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ApplicationStatus.Add(applicationStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicationStatus", new { id = applicationStatus.Id }, applicationStatus);
        }

        // DELETE: api/ApplicationStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationStatus = await _context.ApplicationStatus.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationStatus == null)
            {
                return NotFound();
            }

            _context.ApplicationStatus.Remove(applicationStatus);
            await _context.SaveChangesAsync();

            return Ok(applicationStatus);
        }

        private bool ApplicationStatusExists(int id)
        {
            return _context.ApplicationStatus.Any(e => e.Id == id);
        }
    }
}