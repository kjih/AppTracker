using AppTracker.Models.DTO;
using System.Collections.Generic;
using AppTracker.Models.DB;

namespace AppTracker.Models.Repositories.Interfaces
{
    public interface IApplicationRepo
    {
        IEnumerable<ApplicationDTO> GetAll();
        ApplicationDTO GetId(int appId);
        ApplicationDTO CreateApplication(Application app);
        bool EditApplication(int appId, Application app);
        ApplicationDTO DeleteApplication(int appId);
        bool ApplicationExists(int appId);
        int GetTotalAppCount();
        int GetPendingAppCount();
    }
}
