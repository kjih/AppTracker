using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppTracker.Models.DB;
using AppTracker.Models.Repositories.Interfaces;
using AppTracker.Models.DTO;

namespace AppTracker.Controllers
{
    [Produces("application/json")]
    [Route("api/Contacts")]
    public class ContactsController : Controller
    {
        private readonly IContactRepo _contactRepo;

        public ContactsController(IContactRepo contactRepo)
        {
            _contactRepo = contactRepo;
        }

        // GET: api/Contacts
        [HttpGet]
        public IEnumerable<ContactDTO> GetContact()
        {
            return _contactRepo.GetAll();
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public IActionResult GetContact([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = _contactRepo.GetId(id);


            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public IActionResult PutContact([FromRoute] int id, [FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.Id)
            {
                return BadRequest();
            }

            if (!_contactRepo.EditContact(id, contact)) 
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Contacts
        [HttpPost]
        public IActionResult PostContact([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contactDto = _contactRepo.CreateContact(contact);

            if (contactDto == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetContact", new { id = contact.Id }, contactDto);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public IActionResult DeleteContact([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var removed = _contactRepo.DeleteContact(id);

            return (removed == null) ? (IActionResult)NotFound() : Ok(removed);
        }

        private bool ContactExists(int id)
        {
            return _contactRepo.ContactExists(id);
        }
    }
}