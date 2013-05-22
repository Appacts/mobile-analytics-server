using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.Client.Model
{
    public class DatePicker
    {
        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public DatePicker(DateTime startDate, DateTime endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }
}
