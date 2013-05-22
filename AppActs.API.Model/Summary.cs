using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using AppActs.API.Model.Device;
using AppActs.Model.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace AppActs.API.Model
{
    public abstract class Summary
    {
        public ObjectId Id { get; set; }
        public string Version { get; set; }
        public DateTime Date { get; set; }
        public Guid ApplicationId { get; set; }
        public PlatformType PlatformId { get; set; }
        public int Count { get; set; }

        public Summary()
        {

        }

        public Summary(Item item)
        {
            this.Version = item.Version;
            this.Date = DateTime.SpecifyKind(item.Date, DateTimeKind.Utc);
            this.ApplicationId = item.ApplicationId;
            this.PlatformId = item.PlatformId;
            this.Count++;
        }

        public Summary(DeviceInfo deviceInfo, ApplicationInfo applicationInfo)
        {
            this.Date = DateTime.SpecifyKind(deviceInfo.Date, DateTimeKind.Utc);
            this.ApplicationId = applicationInfo.ApplicationId;
            this.PlatformId = deviceInfo.PlatformType;
            this.Version = applicationInfo.Version;
            this.Count++;
        }

    }
}
