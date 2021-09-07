using System;
using District09.Servicefactory.Werkdagen.Domain.Contracts;
using District09.Servicefactory.Werkdagen.Domain.Dto;

namespace District09.Servicefactory.Werkdagen.Domain.Mappers
{
    public class WorkDayMapper : IMapper<DateTime, DayDto>
    {
        public DayDto Map(DateTime input)
        {
            return new DayDto()
            {
                Day = input
            };
        }
    }
}