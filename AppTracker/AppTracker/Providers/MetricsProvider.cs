using AppTracker.Models.DTO.Metrics;
using AppTracker.Models.Repositories.Interfaces;
using AppTracker.Providers.Interfaces;
using System;

namespace AppTracker.Providers
{
    public class MetricsProvider : IMetricsProvider
    {
        private IApplicationRepo _appRepo;

        public MetricsProvider(IApplicationRepo appRepo)
        {
            _appRepo = appRepo;
        }

        public HeardBackDTO HeardBack(Format format)
        {
            int totalAppCount = _appRepo.GetTotalAppCount();
            int pendingAppCount = _appRepo.GetPendingAppCount();
            int heardBackCount = totalAppCount - pendingAppCount;


            var result = new HeardBackDTO(format.ToString(), 
                                          CalculateHeardBack(format, totalAppCount, heardBackCount));

            return result;
        }

        private decimal CalculateHeardBack(Format format, int totalCount, int heardBackCount)
        {
            if (totalCount == 0)
            {
                return 0;
            }

            decimal heardBack = (decimal) heardBackCount / (decimal) totalCount;
            
            if (format == Format.Percentage)
            {
                heardBack *= 100;
            }

            return Decimal.Round(heardBack, 2);
        }
    }
}
