using System;
using System.Collections.Generic;
using System.Linq;
using District09.Servicefactory.Werkdagen.Domain.Contracts;
using District09.Servicefactory.Werkdagen.Domain.Exceptions;
using District09.Servicefactory.Werkdagen.Domain.Models;

namespace District09.Servicefactory.Werkdagen.Domain.Data
{
    public class WorkdayRepository : IWorkdayRepository
    {
        private readonly WorkDayData _data;

        private IEnumerable<WorkDay> FreeDays => _data.WorkDays.Where(e => !e.IsWerkDag);

        public WorkdayRepository(IFreedayDataProvider dataProvider)
        {
            _data = dataProvider.ProvideData();
        }

        public DateTime FindDay(int range = 0)
        {
            return FindDayFromDate(range, DateTime.Now.Date);
        }

        public DateTime FindDay(DateTime fromDay, int range = 0)
        {
            return FindDayFromDate(range, fromDay);
        }

        private DateTime FindDayFromDate(int range, DateTime from)
        {
            var counter = 0;
            var retrievedDate = from.Date;
            var direction = range <= 0 ? -1 : 1;

            if (range == 0 && IsFreeDay(DateTime.Today))
            {
                range = -1;
            }

            while (counter != range)
            {
                retrievedDate = retrievedDate.AddDays(direction);
                if (IsFreeDay(retrievedDate))
                {
                    // retrieved day is a free day, not counting it
                    continue;
                }

                // retrieved day is a workday, counter should go to range
                counter += direction;

                CheckBounds(retrievedDate);
            }

            return retrievedDate;
        }

        private bool IsFreeDay(DateTime day)
        {
            return FreeDays.Any(data => day.Equals(data.DateTime.Date));
        }

        private void CheckBounds(DateTime day)
        {
            var lowerBound = _data.LowerBound();
            var higherBound = _data.HigherBound();
            if (day < lowerBound)
            {
                throw new DateOutOfBoundsException(lowerBound);
            }

            if (day > higherBound)
            {
                throw new DateOutOfBoundsException(higherBound);
            }
        }
    }
}