using System;
using System.Collections.Generic;
using System.Linq;
using District09.Servicefactory.Werkdagen.Domain.Contracts;
using District09.Servicefactory.Werkdagen.Domain.Exceptions;
using District09.Servicefactory.Werkdagen.Domain.Models;

namespace District09.Servicefactory.Werkdagen.Domain.Data
{
    public class WerkdagRepository : IWerkdagRepository
    {
        private readonly WerkDagData _data;

        private IEnumerable<WerkDag> FreeDays => _data.Werkdagen.Where(e => !e.IsWerkDag);

        public WerkdagRepository(IFreedayDataProvider dataProvider)
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
            var direction = range < 0 ? -1 : 1;
            while (counter != range)
            {
                retrievedDate = retrievedDate.AddDays(direction);
                if (FreeDays.Any(data => retrievedDate.Equals(data.DateTime.Date)))
                {
                    // retrieved day is a free day, not counting it
                    continue;
                }

                // retrieved day is a workday, counter should go to range
                counter += direction;

                if (retrievedDate < _data.LowerBound() || retrievedDate > _data.HigherBound())
                {
                    throw new DateOutOfBoundsException(retrievedDate);
                }
            }

            return retrievedDate;
        }
    }
}