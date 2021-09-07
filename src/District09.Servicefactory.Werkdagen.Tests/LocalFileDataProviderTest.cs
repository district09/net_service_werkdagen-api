using District09.Servicefactory.Werkdagen.Domain.Configuration;
using District09.Servicefactory.Werkdagen.Domain.Data.Providers;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace District09.Servicefactory.Werkdagen.Tests
{
    public class LocalFileDataProviderTest
    {
        [Fact]
        public void ProvideDataShouldReturnNonEmptyDataset()
        {
            var optionsMock = new Mock<IOptions<ExcellConfigOptions>>();
            optionsMock.SetupGet(options => options.Value).Returns(new ExcellConfigOptions()
            {
                DateInColumn = 1,
                ExcellFilePath = "../../../../local_dates.xlsx"
            });

            var provider = new LocalFileFreedayDataProvider(optionsMock.Object);

            var data = provider.ProvideData();

            Assert.NotNull(data);
            Assert.NotEmpty(data.WorkDays);
        }
    }
}