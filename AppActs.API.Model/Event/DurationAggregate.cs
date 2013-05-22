using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model.Event
{
    public class DurationAggregate : Aggregate<string>
    {
        public long Seconds { get; set; }

        public DurationAggregate(string key, long seconds)
            : base(key)
        {
            this.Seconds = seconds;
        }
    }
}
