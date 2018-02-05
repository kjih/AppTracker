using AppTracker.Models.DTO;
using System.Collections.Generic;

namespace AppTracker.Models.Repositories.Interfaces
{
    public interface IContactRepo
    {
        IEnumerable<ContactDTO> GetContactsByCompany(int companyId);
    }
}
