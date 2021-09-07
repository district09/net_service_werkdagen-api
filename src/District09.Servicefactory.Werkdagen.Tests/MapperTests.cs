using System;
using District09.Servicefactory.Werkdagen.Domain.Mappers;
using Xunit;

namespace District09.Servicefactory.Werkdagen.Tests
{
    public class MapperTests
    {
        [Fact]
        public void MapDateShouldConvertToCorrectDto()
        {
            var mapper = new WorkDayMapper();
            var output = mapper.Map(DateTime.Today);

            Assert.Equal(DateTime.Today, output.Day);
        }
    }
}