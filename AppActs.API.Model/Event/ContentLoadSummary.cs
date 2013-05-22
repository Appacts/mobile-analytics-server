using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model.Event
{
    public class ContentLoadSummary : Summary
    {
        public List<ContentDurationAggregate> Loads { get; set; }

        public ContentLoadSummary()
        {

        }

        public ContentLoadSummary(Event eventItem)
            : base(eventItem)
        {
            this.Loads = new List<ContentDurationAggregate>();
            this.Loads.Add(new ContentDurationAggregate(eventItem.ScreenName, 
                eventItem.EventName, eventItem.Length / 1000));
        }

    }
}
