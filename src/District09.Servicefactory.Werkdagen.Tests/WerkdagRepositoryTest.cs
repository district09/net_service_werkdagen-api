using System;
using System.Collections.Generic;
using System.Linq;
using District09.Servicefactory.Werkdagen.Domain.Contracts;
using District09.Servicefactory.Werkdagen.Domain.Data;
using District09.Servicefactory.Werkdagen.Domain.Exceptions;
using District09.Servicefactory.Werkdagen.Domain.Models;
using Moq;
using Xunit;

namespace District09.Servicefactory.Werkdagen.Tests
{
    public class WerkdagRepositoryTest
    {
        [Fact]
        public void RepoWithoutFreeDaysShouldReturnXDaysAgo()
        {
            var repo = CreateRepo(new List<WerkDag>());

            var today = DateTime.Now.Date;

            var foundDay = repo.FindDay(-5);

            Assert.Equal(today.AddDays(-5).Date, foundDay.Date);
        }

        [Fact]
        public void RepoWithYesterdayIncludedShouldReturnPreviousDay()
        {
            var repo = CreateRepo(new List<WerkDag>()
            {
                new WerkDag { DateTime = DateTime.Now.AddDays(-1).Date, IsWerkDag = false }
            });

            var today = DateTime.Now.Date;
            var founDay = repo.FindDay(-1);
            Assert.Equal(today.AddDays(-2).Date, founDay.Date);
        }

        [Fact]
        public void RepoWithoutFreeDaysShouldReturnNextXDay()
        {
            var repo = CreateRepo(new List<WerkDag>());

            var today = DateTime.Now.Date;

            var foundDay = repo.FindDay(5);

            Assert.Equal(today.AddDays(5).Date, foundDay.Date);
        }

        [Fact]
        public void RepoWithTomorrowIncludedShouldReturnNextDay()
        {
            var repo = CreateRepo(new List<WerkDag>()
            {
                new WerkDag() { DateTime = DateTime.Now.AddDays(1).Date, IsWerkDag = false }
            });

            var today = DateTime.Now.Date;
            var founDay = repo.FindDay(1);
            Assert.Equal(today.AddDays(2).Date, founDay.Date);
        }

        [Fact]
        public void OutOfBoundDateShouldThrowException()
        {
            var repo = CreateRepo(new List<WerkDag>
            {
                new WerkDag { DateTime = DateTime.Now.AddDays(-1), IsWerkDag = false },
                new WerkDag { DateTime = DateTime.Now.AddDays(-10), IsWerkDag = false }
            });

            Assert.Throws<DateOutOfBoundsException>(() => repo.FindDay(-60));
        }

        private static IWerkdagRepository CreateRepo(IEnumerable<WerkDag> days)
        {
            var data = new WerkDagData()
            {
                Werkdagen = days.ToList()
            };
            var dataProvider = new Mock<IFreedayDataProvider>();
            dataProvider.Setup(provider => provider.ProvideData()).Returns(data);
            return new WerkdagRepository(dataProvider.Object);
        }
    }
}