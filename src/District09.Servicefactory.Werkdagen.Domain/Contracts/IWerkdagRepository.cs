using System;

namespace District09.Servicefactory.Werkdagen.Domain.Contracts
{
    public interface IWerkdagRepository
    {
        public DateTime FindDay(int range);
        public DateTime FindDay(DateTime fromDay, int range);
    }
}