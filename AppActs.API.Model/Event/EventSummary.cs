using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Model.Device;

namespace AppActs.API.Model.Event
{
    public class EventSummary : Summary
    {
        public List<EventAggregate> ScreenEvents { get; set; }

        public EventSummary()
        {

        }

        public EventSummary(Event eventItem)
             : base(eventItem)
        {
            this.ScreenEvents = new List<EventAggregate>();
            this.ScreenEvents.Add(new EventAggregate(eventItem.ScreenName, eventItem.EventName));
        }
    }
}
