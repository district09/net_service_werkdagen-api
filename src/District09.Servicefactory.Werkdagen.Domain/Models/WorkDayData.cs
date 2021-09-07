using System;
using System.Collections.Generic;
using System.Linq;

namespace District09.Servicefactory.Werkdagen.Domain.Models
{
    public class WorkDayData
    {
        public List<WorkDay> WorkDays { get; set; }

        public WorkDayData()
        {
            WorkDays = new List<WorkDay>();
        }

        public DateTime LowerBound()
        {
            var dag = WorkDays.OrderBy(a => a.DateTime.Date).FirstOrDefault() ??
                      new WorkDay() { DateTime = DateTime.Now, IsWerkDag = false };
            return dag.DateTime.AddMonths(-1).Date;
        }

        public DateTime HigherBound()
        {
            var dag = WorkDays.OrderByDescending(a => a.DateTime.Date).FirstOrDefault() ??
                      new WorkDay() { DateTime = DateTime.Now, IsWerkDag = false };
            return dag.DateTime.AddMonths(1).Date;
        }
    }
}