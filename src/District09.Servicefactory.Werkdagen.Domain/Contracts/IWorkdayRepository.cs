using System;
using District09.Servicefactory.Werkdagen.Domain.Models;

namespace District09.Servicefactory.Werkdagen.Domain.Contracts
{
    public interface IWorkdayRepository
    {
        public DateTime FindDay(int range);
        public DateTime FindDay(DateTime fromDay, int range);
    }
}