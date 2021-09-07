using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace District09.Servicefactory.Weekdagen.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WerkDagController : ControllerBase
    {
        private readonly ILogger<WerkDagController> _logger;

        public WerkDagController(ILogger<WerkDagController> logger)
        {
            _logger = logger;
        }
    }
}