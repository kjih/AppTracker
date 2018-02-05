using AppTracker.Models.DB;
using AppTracker.Models.DTO;
using AppTracker.Models.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTracker.Models.Repositories
{
    public class CompanyRepo : ICompanyRepo
    {
        private readonly AppTrackerDBContext _context;

        public CompanyRepo(AppTrackerDBContext context)
        {
            _context = context;
        }

        Func<Company, CompanyDTO> toCompanyDTO = x => new CompanyDTO
        {
            Id = x.Id,
            Name = x.Name,
            Address1 = x.Address1,
            Address2 = x.Address2,
            Address3 = x.Address3,
            Notes = x.Notes
        };

        public bool CompanyExists(int companyId)
        {
            return _context.Company.Any(e => e.Id == companyId);
        }

        public IEnumerable<CompanyDTO> GetAll()
        {
            return _context.Company.Select(toCompanyDTO);
        }

        public CompanyDTO GetId(int companyId)
        {
            var company =  _context.Company.SingleOrDefault(m => m.Id == companyId);

            return (company == null) ? null : toCompanyDTO(company);
        }

        public CompanyDTO CreateCompany(Company company)
        {
            _context.Company.Add(company);
            _context.SaveChanges();

            return toCompanyDTO(company);
        }

        public bool EditCompany(int companyId, Company company)
        {
            if (companyId != company.Id)
            {
                return false;
            }

            _context.Entry(company).State = EntityState.Modified;

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
        
        public CompanyDTO DeleteCompany(int companyId)
        {

            var company = _context.Company.SingleOrDefault(m => m.Id == companyId);
            if (company == null)
            {
                return null;
            }

            _context.Company.Remove(company);
            _context.SaveChanges();

            return toCompanyDTO(company);
        }
    }
}
