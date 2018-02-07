using AppTracker.Models.DB;
using AppTracker.Models.DTO;
using AppTracker.Models.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppTracker.Models.Repositories
{
    public class ApplicationRepo : IApplicationRepo
    {
        private readonly AppTrackerDBContext _context;

        public ApplicationRepo(AppTrackerDBContext context)
        {
            _context = context;
        }

        Func<Application, ApplicationDTO> toAppDTO = x => new ApplicationDTO
        {
            Id = x.Id,
            CompanyId = x.CompanyId,
            ApplicationDate = x.ApplicationDate,
            Role = x.Role
        };

        public bool ApplicationExists(int appId)
        {
            return _context.Application.Any(a => a.Id == appId);
        }

        public ApplicationDTO CreateApplication(Application app)
        {
            if (app.CompanyId == null)
            {
                return null;
            }

            try
            {
                _context.Application.Add(app);
                _context.SaveChanges();
            }
            catch
            {
                return null;
            }

            return toAppDTO(app);
        }

        public ApplicationDTO DeleteApplication(int appId)
        {
            var app = _context.Application.SingleOrDefault(a => a.Id == appId);

            if (app == null)
            {
                return null;
            }

            _context.Application.Remove(app);
            _context.SaveChanges();

            return toAppDTO(app);
        }

        public bool EditApplication(int appId, Application app)
        {
            if (appId != app.Id
                || app.CompanyId == null)
            {
                return false;
            }

            _context.Entry(app).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<ApplicationDTO> GetAll()
        {
            return _context.Application.Select(toAppDTO);
        }

        public ApplicationDTO GetId(int appId)
        {
            var application = _context.Application.SingleOrDefault(a => a.Id == appId);

            return (application == null) ? null : toAppDTO(application);
        }

        public int GetTotalAppCount()
        {
            int count = _context.Application.Count();

            return count;
        }

        public int GetPendingAppCount()
        {
            int count = (from a in _context.Application
                         join s in _context.ApplicationStatus on a.Id equals s.ApplicationId
                         where s.Active != null
                         group a by a.Id into g
                         select new { ApplicationId = g.Key }).Count();

            return count;
        }
    }
}
