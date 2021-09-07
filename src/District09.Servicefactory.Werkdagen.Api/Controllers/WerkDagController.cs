using System;
using District09.Servicefactory.Werkdagen.Domain.Contracts;
using District09.Servicefactory.Werkdagen.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace District09.Servicefactory.Werkdagen.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WerkDagController : ControllerBase
    {
        private readonly ILogger<WerkDagController> _logger;
        private readonly IWerkdagRepository _repository;

        public WerkDagController(ILogger<WerkDagController> logger, IWerkdagRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult GetFreeDays([FromQuery(Name = "range")] int range = 1)
        {
            DateTime found;
            try
            {
                found = _repository.FindDay(range);
            }
            catch (DateOutOfBoundsException e)
            {
                _logger.LogWarning(e, "date requested with range {Range} was out of bounds", range);
                return BadRequest($"range {range} is out of bounds");
            }

            return Ok(found);
        }
    }
}