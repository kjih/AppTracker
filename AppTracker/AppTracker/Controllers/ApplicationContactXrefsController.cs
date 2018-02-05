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
    [Route("api/ApplicationContactXrefs")]
    public class ApplicationContactXrefsController : Controller
    {
        private readonly AppTrackerDBContext _context;

        public ApplicationContactXrefsController(AppTrackerDBContext context)
        {
            _context = context;
        }

        // GET: api/ApplicationContactXrefs
        [HttpGet]
        public IEnumerable<ApplicationContactXref> GetApplicationContactXref()
        {
            return _context.ApplicationContactXref;
        }

        // GET: api/ApplicationContactXrefs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationContactXref([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationContactXref = await _context.ApplicationContactXref.SingleOrDefaultAsync(m => m.Id == id);

            if (applicationContactXref == null)
            {
                return NotFound();
            }

            return Ok(applicationContactXref);
        }

        // PUT: api/ApplicationContactXrefs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationContactXref([FromRoute] int id, [FromBody] ApplicationContactXref applicationContactXref)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationContactXref.Id)
            {
                return BadRequest();
            }

            _context.Entry(applicationContactXref).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationContactXrefExists(id))
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

        // POST: api/ApplicationContactXrefs
        [HttpPost]
        public async Task<IActionResult> PostApplicationContactXref([FromBody] ApplicationContactXref applicationContactXref)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ApplicationContactXref.Add(applicationContactXref);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicationContactXref", new { id = applicationContactXref.Id }, applicationContactXref);
        }

        // DELETE: api/ApplicationContactXrefs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationContactXref([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationContactXref = await _context.ApplicationContactXref.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationContactXref == null)
            {
                return NotFound();
            }

            _context.ApplicationContactXref.Remove(applicationContactXref);
            await _context.SaveChangesAsync();

            return Ok(applicationContactXref);
        }

        private bool ApplicationContactXrefExists(int id)
        {
            return _context.ApplicationContactXref.Any(e => e.Id == id);
        }
    }
}