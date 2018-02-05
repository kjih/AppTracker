using AppTracker.Models.DB;
using AppTracker.Models.DTO;
using AppTracker.Models.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<ContactDTO> GetAll()
        {
            return _context.Contact.Select(toContactDTO);
        }

        public ContactDTO GetId(int contactId)
        {
            var contact = _context.Contact.SingleOrDefault(m => m.Id == contactId);

            return (contact == null) ? null : toContactDTO(contact);
        }

        public ContactDTO CreateContact(Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.FirstName) 
                && string.IsNullOrWhiteSpace(contact.LastName))
            {
                return null;
            }

            _context.Contact.Add(contact);
            _context.SaveChanges();

            return toContactDTO(contact);
        }

        public bool EditContact(int contactId, Contact contact)
        {
            if (contactId != contact.Id ||
                (string.IsNullOrWhiteSpace(contact.FirstName) 
                && string.IsNullOrWhiteSpace(contact.LastName)))
            {
                return false;
            }

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                // TODO: Log
                return false;
            }

            return true;
        }

        public ContactDTO DeleteContact(int contactId)
        {
            var contact = _context.Contact.SingleOrDefault(m => m.Id == contactId);

            if (contact == null)
            {
                return null;
            }

            _context.Contact.Remove(contact);
            _context.SaveChanges();

            return toContactDTO(contact);
        }

        public bool ContactExists(int contactId)
        {
            return _context.Contact.Any(e => e.Id == contactId);
        }
    }
}
