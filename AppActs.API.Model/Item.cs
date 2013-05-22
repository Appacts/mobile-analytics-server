using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Specialized;
using AppActs.Model.Enum;
using System.Globalization;

namespace AppActs.API.Model
{
    public abstract class Item
    {
        [BsonElement("dtc")]
        public DateTime DateCreated { get; set; }

        [BsonElement("dtcd")]
        public DateTime DateCreatedOnDevice { get; set; }

        [BsonElement("dt")]
        public DateTime Date { get; set; }

        [BsonElement("ses")]
        public Guid SessionId { get; set; }

        [BsonElement("ver")]
        public string Version { get; set; }

        [BsonElement("appId")]
        public Guid ApplicationId { get; set; }

        [BsonElement("devId")]
        public Guid DeviceId { get; set; }

        [BsonElement("pltId")]
        public PlatformType PlatformId { get; set; }

        public Item()
        {

        }

        public Item(NameValueCollection keyValues)
        {
            this.DateCreated = DateTime.UtcNow;
            this.DateCreatedOnDevice = DateTime.SpecifyKind(DateTime.Parse(keyValues[Keys.DATE_CREATED]), DateTimeKind.Utc);
            this.Date = this.DateCreatedOnDevice.Date;
            this.SessionId = Guid.Parse(keyValues[Keys.SESSION_ID]);
            this.Version = keyValues[Keys.VERSION];
            this.ApplicationId = new Guid(keyValues[Keys.APPLICATION_GUID]);
            this.DeviceId = new Guid(keyValues[Keys.DEVICE_GUID]);
        }
    }
}
