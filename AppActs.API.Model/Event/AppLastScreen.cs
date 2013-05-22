using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AppActs.API.Model.Event
{
    [BsonIgnoreExtraElements]
    public class DeviceAppLastScreen : ItemWithScreen
    {
        public DeviceAppLastScreen(ItemWithScreen eventItem)
            : base()
        {
            this.ScreenName = eventItem.ScreenName;
            this.SessionId = eventItem.SessionId;
            this.PlatformId = eventItem.PlatformId;
            this.ApplicationId = eventItem.ApplicationId;
            this.Date = eventItem.Date;
            this.DateCreated = eventItem.DateCreated;
            this.DateCreatedOnDevice = eventItem.DateCreatedOnDevice;
            this.DeviceId = eventItem.DeviceId;
            this.Version = eventItem.Version;
        }
    }
}
