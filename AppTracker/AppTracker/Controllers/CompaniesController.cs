using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AppTracker.Models.DB;
using AppTracker.Models.DTO;
using AppTracker.Models.Repositories.Interfaces;

namespace AppTracker.Controllers
{
    [Produces("application/json")]
    [Route("api/Companies")]
    public class CompaniesController : Controller
    {
        private readonly ICompanyRepo _companyRepo;
        private readonly IContactRepo _contactRepo;

        public CompaniesController(ICompanyRepo companyRepo, IContactRepo contactRepo)
        {
            _companyRepo = companyRepo;
            _contactRepo = contactRepo;
        }

        // GET: api/Companies
        [HttpGet]
        public IEnumerable<CompanyDTO> GetCompany()
        {
            return _companyRepo.GetAll();
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public IActionResult GetCompany([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var company = _companyRepo.GetId(id);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // GET: api/Companies/5/Contacts
        [HttpGet("{id}/Contacts")]
        public IActionResult GetCompanyContacts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_companyRepo.CompanyExists(id))
            {
                return NotFound();
            }

            var contacts = _contactRepo.GetContactsByCompany(id);

            return Ok(contacts);
        }

        // PUT: api/Companies/5
        [HttpPut("{id}")]
        public IActionResult PutCompany([FromRoute] int id, [FromBody] Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_companyRepo.CompanyExists(id))
            {
                return NotFound();
            }

            if (!_companyRepo.EditCompany(id, company))
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Companies
        [HttpPost]
        public IActionResult PostCompany([FromBody] Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyDto = _companyRepo.CreateCompany(company);

            if (companyDto == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetCompany", new { id = company.Id }, companyDto);
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCompany([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var removed = _companyRepo.DeleteCompany(id);

            return (removed == null) ? (IActionResult) NotFound() : Ok(removed);
        }
    }
}