using AppTracker.Models.DTO;
using System.Collections.Generic;
using AppTracker.Models.DB;

namespace AppTracker.Models.Repositories.Interfaces
{
    public interface IApplicationStatusRepo
    {
        IEnumerable<ApplicationStatusDTO> GetAllApplicationStatus(int appId);
        ApplicationStatusDTO GetId(int statusId);
        ApplicationStatusDTO CreateStatus(ApplicationStatus status);
        bool EditStatus(int statusId, ApplicationStatus status);
        ApplicationStatusDTO DeleteStatus(int statusId);
        bool StatusExists(int statusId);
    }
}
