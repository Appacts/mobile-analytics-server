using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Model.Device;

namespace AppActs.API.Model.Event
{
    public class ScreenSummary : Summary
    {
        public List<DurationAggregate> Durations { get; set; }

        public ScreenSummary(Event eventItem)
            : base(eventItem)
        {
            this.Durations = new List<DurationAggregate>();
            this.Durations.Add(new DurationAggregate(eventItem.ScreenName, eventItem.Length / 1000));
        }
    }
}
