using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using AppActs.API.Model.Enum;
using AppActs.API.Model.Device;
using System.Collections.Specialized;

namespace AppActs.API.Model.Event
{
    public class Event : ItemWithScreen
    {
        [BsonElement("data")]
        public string Data { get; set; }

        [BsonElement("evntid")]
        public EventType EventTypeId { get; set; }

        [BsonElement("evntnm")]
        public string EventName { get; set; }

        [BsonElement("len")]
        public Int64 Length { get; set; }

        public Event()
            : base()
        {

        }

        public Event(NameValueCollection keyValues)
            : base(keyValues)
        {
            this.Data = keyValues[Keys.DATA];
            this.EventTypeId = (EventType)int.Parse(keyValues[Keys.EVENT_TYPE]);
            this.EventName = keyValues[Keys.EVENT_NAME];
            this.Length = long.Parse(keyValues[Keys.LENGTH]);
        }
    }
}
