using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model.Event
{
    public class EventAggregate : Aggregate<string>
    {
        public string Event { get; set; }
        public string ScreenAndEvent { get; set; }

        public EventAggregate(string screen, string eventName)
            : base(screen)
        {
            this.Event = eventName;
            this.ScreenAndEvent = String.Concat(screen, eventName);
        }

        public EventAggregate CopyOnlyKeys()
        {
            return new EventAggregate(this.Key, this.Event);
        }
    }
}
