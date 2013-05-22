using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace AppActs.API.Model.Event
{
    public class ScreenRoute
    {
        [BsonElement("A")]
        public string LastScreen { get; set; }

        [BsonElement("B")]
        public string CurrentScreen { get; set; }

        [BsonElement("AB")]
        public string LastAndCurrentScreen { get; set; }

        public int Count { get; set; }

        public ScreenRoute()
        {

        }

        public ScreenRoute(string lastScreen, string screenCurrent)
        {
            this.LastScreen = lastScreen;
            this.CurrentScreen = screenCurrent;
            this.LastAndCurrentScreen = String.Concat(lastScreen, screenCurrent);
        }

        public ScreenRoute CopyOnlyKeys()
        {
            return new ScreenRoute(this.LastScreen, this.CurrentScreen);
        }
    }
}
