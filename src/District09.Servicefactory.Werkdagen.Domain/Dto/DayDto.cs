using System;
using Newtonsoft.Json;

namespace District09.Servicefactory.Werkdagen.Domain.Dto
{
    public class DayDto
    {
        [JsonProperty("day")] public DateTime Day { get; set; }
    }
}