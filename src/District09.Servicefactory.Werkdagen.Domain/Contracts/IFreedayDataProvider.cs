using System.Collections.Generic;
using District09.Servicefactory.Werkdagen.Domain.Models;

namespace District09.Servicefactory.Werkdagen.Domain.Contracts
{
    public interface IFreedayDataProvider
    {
        public WerkDagData ProvideData();
    }
}