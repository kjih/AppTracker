using AppTracker.Models.DB;
using AppTracker.Models.DTO;
using System.Collections.Generic;

namespace AppTracker.Models.Repositories.Interfaces
{
    public interface ICompanyRepo
    {
        IEnumerable<CompanyDTO> GetAll();
        CompanyDTO GetId(int companyId);
        CompanyDTO CreateCompany(Company company);
        bool EditCompany(int companyId, Company company);
        CompanyDTO DeleteCompany(int companyId);
        bool CompanyExists(int companyId);
    }
}
