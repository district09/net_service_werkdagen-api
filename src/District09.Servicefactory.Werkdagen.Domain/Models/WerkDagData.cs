using System;
using System.Collections.Generic;
using System.Linq;

namespace District09.Servicefactory.Werkdagen.Domain.Models
{
    public class WerkDagData
    {
        public List<WerkDag> Werkdagen { get; set; }

        public WerkDagData()
        {
            Werkdagen = new List<WerkDag>();
        }

        public DateTime LowerBound()
        {
            var dag = Werkdagen.OrderBy(a => a.DateTime.Date).FirstOrDefault() ??
                      new WerkDag() { DateTime = DateTime.Now, IsWerkDag = false };
            return dag.DateTime.AddMonths(-1).Date;
        }

        public DateTime HigherBound()
        {
            var dag = Werkdagen.OrderByDescending(a => a.DateTime.Date).FirstOrDefault() ??
                      new WerkDag() { DateTime = DateTime.Now, IsWerkDag = false };
            return dag.DateTime.AddMonths(1).Date;
        }
    }
}