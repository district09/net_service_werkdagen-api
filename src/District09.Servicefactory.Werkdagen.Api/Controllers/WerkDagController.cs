using System;
using District09.Servicefactory.Werkdagen.Domain.Contracts;
using District09.Servicefactory.Werkdagen.Domain.Dto;
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
        private readonly IWorkdayRepository _repository;
        private readonly IMapper<DateTime, DayDto> _mapper;

        public WerkDagController(ILogger<WerkDagController> logger, IWorkdayRepository repository,
            IMapper<DateTime, DayDto> mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public IActionResult GetFreeDays([FromQuery(Name = "range")] int range = 0)
        {
            DateTime found;
            try
            {
                _logger.LogInformation("Finding day at {Range} working days from today", range);
                found = _repository.FindDay(range);
                _logger.LogInformation("Found working day {Found}", found);
            }
            catch (DateOutOfBoundsException e)
            {
                _logger.LogWarning(e, "date requested with range {Range} was out of bounds", range);
                return BadRequest($"range {range} is out of bounds");
            }

            return Ok(_mapper.Map(found));
        }
    }
}