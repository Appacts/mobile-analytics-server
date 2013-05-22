using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using System.Collections.Specialized;
using AppActs.Model.Enum;

namespace AppActs.API.Model.Device
{
    public class DeviceInfo
    {
        public ObjectId Id { get; set; }
        public Guid Guid { get; set; }
        public string Model { get; set; }
        public PlatformType PlatformType { get; set; }
        public string Carrier { get; set; }
        public string OperatingSystem { get; set; }
        public int TimeZoneOffset { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime Date { get;  set; }
        public string Locale { get; set; }
        public int ResolutionWidth { get; set; }
        public int ResolutionHeight { get; set; }
        public string Manufactorer { get; set; }

        public DeviceInfo()
        {

        }

        public DeviceInfo(NameValueCollection keyValues)
        {
            this.Model = keyValues[Keys.MODEL];
            this.PlatformType = (PlatformType)int.Parse(keyValues[Keys.PLATFORM_TYPE]);
            this.Carrier = keyValues[Keys.CARRIER];
            this.OperatingSystem = keyValues[Keys.OPERATING_SYSTEM];
            this.TimeZoneOffset = int.Parse(keyValues[Keys.TIME_ZONE_OFFSET]);
            this.Locale = keyValues[Keys.LOCALE];
            this.ResolutionWidth = int.Parse(keyValues[Keys.RESOLUTION_WIDTH]);
            this.ResolutionHeight = int.Parse(keyValues[Keys.RESOLUTION_HEIGHT]);
            this.Manufactorer = keyValues[Keys.MANUFACTURER];
            this.DateCreated =  DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            this.Date = DateTime.SpecifyKind(DateTime.Today, DateTimeKind.Utc);
        }
    }
}
