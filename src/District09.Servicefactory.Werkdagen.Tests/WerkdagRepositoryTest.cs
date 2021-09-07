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
            var repo = CreateRepo(new List<WorkDay>());

            var today = DateTime.Now.Date;

            var foundDay = repo.FindDay(-5);

            Assert.Equal(today.AddDays(-5).Date, foundDay.Date);
        }

        [Fact]
        public void RepoWithYesterdayIncludedShouldReturnPreviousDay()
        {
            var repo = CreateRepo(new List<WorkDay>()
            {
                new WorkDay { DateTime = DateTime.Now.AddDays(-1).Date, IsWerkDag = false }
            });

            var today = DateTime.Now.Date;
            var founDay = repo.FindDay(-1);
            Assert.Equal(today.AddDays(-2).Date, founDay.Date);
        }

        [Fact]
        public void RepoWithoutFreeDaysShouldReturnNextXDay()
        {
            var repo = CreateRepo(new List<WorkDay>());

            var today = DateTime.Now.Date;

            var foundDay = repo.FindDay(5);

            Assert.Equal(today.AddDays(5).Date, foundDay.Date);
        }

        [Fact]
        public void RepoWithTomorrowIncludedShouldReturnNextDay()
        {
            var repo = CreateRepo(new List<WorkDay>()
            {
                new WorkDay() { DateTime = DateTime.Now.AddDays(1).Date, IsWerkDag = false }
            });

            var today = DateTime.Now.Date;
            var founDay = repo.FindDay(1);
            Assert.Equal(today.AddDays(2).Date, founDay.Date);
        }

        [Fact]
        public void LowerOutOfBoundDateShouldThrowException()
        {
            var repo = CreateRepo(new List<WorkDay>
            {
                new WorkDay { DateTime = DateTime.Now.AddDays(-1), IsWerkDag = false },
                new WorkDay { DateTime = DateTime.Now.AddDays(-10), IsWerkDag = false }
            });

            Assert.Throws<DateOutOfBoundsException>(() => repo.FindDay(-60));
        }

        [Fact]
        public void HigherOutOfBoundDateShouldThrowException()
        {
            var repo = CreateRepo(new List<WorkDay>
            {
                new WorkDay { DateTime = DateTime.Now.AddDays(-1), IsWerkDag = false },
                new WorkDay { DateTime = DateTime.Now.AddDays(-10), IsWerkDag = false }
            });

            Assert.Throws<DateOutOfBoundsException>(() => repo.FindDay(60));
        }

        [Fact]
        public void RangeZeroAndEmptyListShouldReturnToday()
        {
            var repo = CreateRepo(new List<WorkDay>());

            var found = repo.FindDay(0);

            Assert.Equal(DateTime.Today, found.Date);
        }

        [Fact]
        public void RangeZeroAndFilledListShouldReturnPreviousValidDay()
        {
            var repo = CreateRepo(new[]
            {
                new WorkDay { DateTime = DateTime.Today, IsWerkDag = false },
                new WorkDay { DateTime = DateTime.Today.AddDays(-1), IsWerkDag = false }
            });

            var found = repo.FindDay(0);

            Assert.Equal(DateTime.Today.AddDays(-2), found);
        }

        [Fact]
        public void FindDateFromHistoricalPovShouldReturnCorrectDate()
        {
            var repo = CreateRepo(new[]
            {
                new WorkDay { DateTime = DateTime.Today, IsWerkDag = false },
                new WorkDay { DateTime = DateTime.Today.AddDays(-1), IsWerkDag = false },
                new WorkDay { DateTime = DateTime.Today.AddDays(-2), IsWerkDag = false },
                new WorkDay { DateTime = DateTime.Today.AddDays(-3), IsWerkDag = false }
            });

            var found = repo.FindDay(DateTime.Today.AddDays(-2), -1);

            Assert.Equal(DateTime.Today.AddDays(-4), found);
        }

        private static IWorkdayRepository CreateRepo(IEnumerable<WorkDay> days)
        {
            var data = new WorkDayData()
            {
                WorkDays = days.ToList()
            };
            var dataProvider = new Mock<IFreedayDataProvider>();
            dataProvider.Setup(provider => provider.ProvideData()).Returns(data);
            return new WorkdayRepository(dataProvider.Object);
        }
    }
}