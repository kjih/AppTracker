using AppTracker.Models.DB;
using AppTracker.Models.DTO;
using AppTracker.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppTracker.Models.Repositories
{
    public class ContactRepo : IContactRepo
    {
        private readonly AppTrackerDBContext _context;

        public ContactRepo(AppTrackerDBContext context)
        {
            _context = context;
        }

        Func<Contact, ContactDTO> toContactDTO = x => new ContactDTO
        {
            Id = x.Id,
            CompanyId = x.CompanyId,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Phone = x.Phone,
            Email = x.Email,
            Role = x.Role,
            Notes = x.Notes
        };

        public IEnumerable<ContactDTO> GetContactsByCompany(int companyId)
        {
            var contacts = _context.Contact.Where(c => c.CompanyId == companyId).Select(toContactDTO);

            return contacts;
        }
    }
}
