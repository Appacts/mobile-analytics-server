using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model.Event
{
    public class AppUsageDurationSummary : Summary
    {
        public SessionLengthGroup LengthGroup { get; set; }

        public AppUsageDurationSummary()
        {

        }

        public AppUsageDurationSummary(Event eventItem)
            : base(eventItem)
        {
            this.LengthGroup = new SessionLengthGroup(eventItem.Length / 1000);
        }
    }
}
