using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using MongoDB.Bson.Serialization.Attributes;
using AppActs.API.Model.Device;
using AppActs.Model.Enum;

namespace AppActs.API.Model.Crash
{
    public class Crash : Item
    {
        [BsonElement("model")]
        public string Model { get; set; }

        [BsonElement("osver")]
        public string OsVersion { get; set; }

        [BsonElement("lstscrn")]
        public string LastScreen { get; set; }

        public Crash()
            : base()
        {

        }

        public Crash(NameValueCollection keyValues)
            : base(keyValues)
        {

        }

        public void Add(DeviceInfo deviceInfo, string lastScreen)
        {
            this.Model = deviceInfo.Model;
            this.OsVersion = deviceInfo.OperatingSystem;
            this.LastScreen = lastScreen;
            this.PlatformId = deviceInfo.PlatformType;
        }
    }
}
