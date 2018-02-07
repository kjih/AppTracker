using Microsoft.AspNetCore.Mvc;
using AppTracker.Providers.Interfaces;
using System;

namespace AppTracker.Controllers
{
    [Produces("application/json")]
    [Route("api/Metrics")]
    public class MetricsController : Controller
    {
        private readonly IMetricsProvider _metricsProvider;

        public MetricsController(IMetricsProvider metricsProvider)
        {
            _metricsProvider = metricsProvider;
        }

        // GET: api/Metrics/HeardBack?format=[percentage,decimal]
        [HttpGet("HeardBack")]
        public IActionResult ApplicationsHeardBack([FromQuery] string format)
        {
            Format formatEnum;
            Enum.TryParse(format, true, out formatEnum);

            var result = _metricsProvider.HeardBack(formatEnum);

            return Ok(result);
        }
    }
}
