using Digipolis.Microservices.Configuration;
using Digipolis.Microservices.Health.Controllers;
using Microsoft.Extensions.Options;

namespace District09.Servicefactory.Werkdagen.Api.Controllers
{
    public class HealthController : HealthBaseController
    {
        public HealthController(IOptions<ApiInformation> apiInformation) : base(apiInformation)
        {
        }
    }
}