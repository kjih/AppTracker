using AppTracker.Models.DB;
using AppTracker.Models.DTO;
using System.Collections.Generic;

namespace AppTracker.Models.Repositories.Interfaces
{
    public interface IContactRepo
    {
        IEnumerable<ContactDTO> GetContactsByCompany(int companyId);
        IEnumerable<ContactDTO> GetAll();
        ContactDTO GetId(int contactId);
        ContactDTO CreateContact(Contact contact);
        bool EditContact(int contactId, Contact contact);
        ContactDTO DeleteContact(int contactId);
        bool ContactExists(int contactId);
    }
}
