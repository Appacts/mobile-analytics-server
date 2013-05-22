using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model.Event
{
    public class FrequencyOfUsageGroup
    {
        public int _24hrs { get; set; }
        public int _1day { get; set; }
        public int _2days { get; set; }
        public int _3days { get; set; }
        public int _4days { get; set; }
        public int _5days { get; set; }
        public int _6days { get; set; }
        public int _1wk { get; set; }
        public int _2wk { get; set; }
        public int _3wk { get; set; }
        public int _1mt { get; set; }
        public int Over1Mt { get; set; }

        public Tuple<string, int> PropertyAndValue { get; set; }

        public FrequencyOfUsageGroup()
        {
            this.setPropertyAndValue("_24hrs", 0);
        }

        public FrequencyOfUsageGroup(Nullable<DateTime> deviceLastVisit)
            : base()
        {
            if (!deviceLastVisit.HasValue)
            {
                return;
            }

            TimeSpan timeSinceLastVisit = DateTime.UtcNow - deviceLastVisit.Value;

            if (timeSinceLastVisit.Days == 0)
            {
                this._24hrs += 1;
                this.setPropertyAndValue("_24hrs", 1);
            }
            else if (timeSinceLastVisit.Days == 1)
            {
                this._1day += 1;
                this.setPropertyAndValue("_1day", 1);
            }
            else if (timeSinceLastVisit.Days == 2)
            {
                this._2days += 1;
                this.setPropertyAndValue("_2days", 1);
            }
            else if (timeSinceLastVisit.Days == 3)
            {
                this._3days += 1;
                this.setPropertyAndValue("_3days", 1);
            }
            else if (timeSinceLastVisit.Days == 4)
            {
                this._4days += 1;
                this.setPropertyAndValue("_4days", 1);
            }
            else if (timeSinceLastVisit.Days == 5)
            {
                this._5days += 1;
                this.setPropertyAndValue("_5days", 1);
            }
            else if (timeSinceLastVisit.Days == 6)
            {
                this._6days += 1;
                this.setPropertyAndValue("_6days", 1);
            }
            else if (timeSinceLastVisit.Days == 7)
            {
                this._1wk += 1;
                this.setPropertyAndValue("_1wk", 1);
            }
            else if (timeSinceLastVisit.Days > 7 && timeSinceLastVisit.Days <= 14)
            {
                this._2wk += 1;
                this.setPropertyAndValue("_2wk", 1);
            }
            else if (timeSinceLastVisit.Days > 14 && timeSinceLastVisit.Days <= 21)
            {
                this._3wk += 1;
                this.setPropertyAndValue("_3wk", 1);
            }
            else if (timeSinceLastVisit.Days > 21 && timeSinceLastVisit.Days <= 31)
            {
                this._1mt += 1;
                this.setPropertyAndValue("_1mt", 1);
            }
            else if (timeSinceLastVisit.Days > 31)
            {
                this.Over1Mt += 1;
                this.setPropertyAndValue("Over1Mt", 1);
            }
        }

        private void setPropertyAndValue(string name, int value)
        {
            this.PropertyAndValue = new Tuple<string, int>(name, value);
        }
    }
}
