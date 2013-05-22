using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model.Event
{
    public class DayOfWeekGroup
    {
        public int Mon { get; set; }
        public int Tue { get; set; }
        public int Wed { get; set; }
        public int Thu { get; set; }
        public int Fri { get; set; }
        public int Sat { get; set; }
        public int Sun { get; set; }

        public Tuple<string, int> PropertyAndValue { get; set; }

        public DayOfWeekGroup()
        {

        }

        public DayOfWeekGroup(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Friday:
                    this.Fri += 1;
                    this.setPropertyAndValue("Fri", 1);
                    break;
                case DayOfWeek.Monday:
                    this.Mon += 1;
                    this.setPropertyAndValue("Mon", 1);
                    break;
                case DayOfWeek.Saturday:
                    this.Sat += 1;
                    this.setPropertyAndValue("Sat", 1);
                    break;
                case DayOfWeek.Sunday:
                    this.Sun += 1;
                    this.setPropertyAndValue("Sun", 1);
                    break;
                case DayOfWeek.Thursday:
                    this.Thu += 1;
                    this.setPropertyAndValue("Thu", 1);
                    break;
                case DayOfWeek.Tuesday:
                    this.Tue += 1;
                    this.setPropertyAndValue("Tue", 1);
                    break;
                case DayOfWeek.Wednesday:
                    this.Wed += 1;
                    this.setPropertyAndValue("Wed", 1);
                    break;
            }
        }

        private void setPropertyAndValue(string name, int value)
        {
            this.PropertyAndValue = new Tuple<string, int>(name, value);
        }

    }
}
