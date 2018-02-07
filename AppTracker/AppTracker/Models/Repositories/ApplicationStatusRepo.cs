using AppTracker.Models.DB;
using AppTracker.Models.DTO;
using AppTracker.Models.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppTracker.Models.Repositories
{
    public class ApplicationStatusRepo : IApplicationStatusRepo
    {
        private readonly AppTrackerDBContext _context;
        
        public ApplicationStatusRepo (AppTrackerDBContext context)
        {
            _context = context;
        }

        Func<ApplicationStatus, ApplicationStatusDTO> toAppStatusDTO = x => new ApplicationStatusDTO
        {
            Id = x.Id,
            ApplicationId = x.ApplicationId,
            Timestamp = x.Timestamp,
            Active = x.Active,
            Status = x.Status,
            Notes = x.Notes
        };

        public ApplicationStatusDTO CreateStatus(ApplicationStatus status)
        {
            if (status.Timestamp != null)
            {
                return null;
            }

            try
            {
                _context.ApplicationStatus.Add(status);
                _context.SaveChanges();
            }
            catch
            {
                return null;
            }

            return toAppStatusDTO(status);
        }

        public ApplicationStatusDTO DeleteStatus(int statusId)
        {
            var status = _context.ApplicationStatus.SingleOrDefault(s => s.Id == statusId);

            if (status == null)
            {
                return null;
            }

            _context.ApplicationStatus.Remove(status);
            _context.SaveChanges();

            return toAppStatusDTO(status);
        }

        public bool EditStatus(int statusId, ApplicationStatus status)
        {
            var timestamp = _context.ApplicationStatus
                                    .AsNoTracking()
                                    .SingleOrDefault(s => s.Id == statusId)
                                    .Timestamp;

            if (status.Timestamp != timestamp)
            {
                return false;
            }

            _context.Entry(status).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                //TODO: Log
                return false;
            }

            return true;
        }

        public IEnumerable<ApplicationStatusDTO> GetAllApplicationStatus(int appId)
        {
            var statusList = _context.ApplicationStatus
                                     .Where(a => a.ApplicationId == appId)
                                     .OrderBy(a => a.Timestamp)
                                     .Select(toAppStatusDTO);

            return statusList;
        }

        public ApplicationStatusDTO GetId(int statusId)
        {
            var status = _context.ApplicationStatus.SingleOrDefault(s => s.Id == statusId);

            return toAppStatusDTO(status);
        }

        public bool StatusExists(int statusId)
        {
            return _context.ApplicationStatus.Any(s => s.Id == statusId);
        }
    }
}
