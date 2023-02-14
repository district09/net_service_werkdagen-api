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
            var repo = CreateRepo(new List<WorkDay>
            {
                new WorkDay { DateTime = GetFriday().AddDays(50) },
                new WorkDay { DateTime = GetFriday().AddDays(-100) }
            });

            var today = GetFriday();

            var foundDay = repo.FindDay(today, -2);

            Assert.Equal(today.AddDays(-2).Date, foundDay.Date);
        }

        [Fact]
        public void RepoWithYesterdayIncludedShouldReturnPreviousDay()
        {
            var repo = CreateRepo(new List<WorkDay>()
            {
                new WorkDay { DateTime = GetFriday().AddDays(-1).Date, IsWerkDag = false }
            });

            var today = GetFriday();
            var founDay = repo.FindDay(today, -1);
            Assert.Equal(today.AddDays(-2).Date, founDay.Date);
        }

        [Fact]
        public void RepoWithoutFreeDaysShouldReturnNextXDay()
        {
            var repo = CreateRepo(new List<WorkDay>
            {
                new WorkDay { DateTime = GetFriday().AddDays(50) },
                new WorkDay { DateTime = GetFriday().AddDays(-100) }
            });

            var today = GetFriday();

            var foundDay = repo.FindDay(today, 5);

            Assert.Equal(today.AddDays(7).Date, foundDay.Date);
        }

        [Fact]
        public void RepoWithTomorrowIncludedShouldReturnNextDay()
        {
            var repo = CreateRepo(new List<WorkDay>()
            {
                new WorkDay() { DateTime = GetFriday().Date, IsWerkDag = false }
            });

            var today = GetFriday().AddDays(-1); // thursday
            var founDay = repo.FindDay(today, 1);
            Assert.Equal(today.AddDays(4).Date, founDay.Date);
        }

        [Fact]
        public void LowerOutOfBoundDateShouldThrowException()
        {
            var repo = CreateRepo(new List<WorkDay>
            {
                new WorkDay { DateTime = GetFriday().AddDays(-1), IsWerkDag = false },
                new WorkDay { DateTime = GetFriday().AddDays(-10), IsWerkDag = false }
            });

            Assert.Throws<DateOutOfBoundsException>(() => repo.FindDay(GetFriday(), -60));
        }

        [Fact]
        public void HigherOutOfBoundDateShouldThrowException()
        {
            var repo = CreateRepo(new List<WorkDay>
            {
                new WorkDay { DateTime = GetFriday().AddDays(-1), IsWerkDag = false },
                new WorkDay { DateTime = GetFriday().AddDays(-10), IsWerkDag = false }
            });

            Assert.Throws<DateOutOfBoundsException>(() => repo.FindDay(GetFriday(), 60));
        }

        [Fact]
        public void RangeZeroAndEmptyListShouldReturnToday()
        {
            var repo = CreateRepo(new List<WorkDay>());

            var found = repo.FindDay(GetFriday(), 0);

            Assert.Equal(GetFriday(), found.Date);
        }

        [Fact]
        public void RangeZeroAndFilledListShouldReturnPreviousValidDay()
        {
            var repo = CreateRepo(new[]
            {
                new WorkDay { DateTime = GetFriday(), IsWerkDag = false }, // friday
                new WorkDay { DateTime = GetFriday().AddDays(-1), IsWerkDag = false } // thursday
            });

            var found = repo.FindDay(GetFriday(), 0);

            Assert.Equal(GetFriday().AddDays(-2), found);
        }

        [Fact]
        public void FindDateFromHistoricalPovShouldReturnCorrectDate()
        {
            var repo = CreateRepo(new[]
            {
                new WorkDay { DateTime = GetFriday(), IsWerkDag = false }, // friday
                new WorkDay { DateTime = GetFriday().AddDays(-1), IsWerkDag = false }, // thursday
                new WorkDay { DateTime = GetFriday().AddDays(-2), IsWerkDag = false }, // wednesday
                new WorkDay { DateTime = GetFriday().AddDays(-3), IsWerkDag = false } // Tuesday
            });

            var found = repo.FindDay(GetFriday().AddDays(-2), -1);

            Assert.Equal(GetFriday().AddDays(-4), found);
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

        private static DateTime GetFriday()
        {
            return new DateTime(2021, 9, 10).Date;
        }
    }
}