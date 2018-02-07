using AppTracker.Models.DTO.Metrics;

namespace AppTracker.Providers.Interfaces
{
    public enum Format
    {
        Decimal = 0,
        Percentage = 1
    }

    public interface IMetricsProvider
    {
        HeardBackDTO HeardBack(Format format);
    }
}
