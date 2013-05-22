using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Specialized;
using AppActs.API.Model.Device;

namespace AppActs.API.Model.Error
{
    public class Error : ItemWithScreen
    {
        [BsonElement("data")]
        public string Data { get; set; }

        [BsonElement("evtnm")]
        public string EventName { get; set; }

        [BsonElement("avldrvsz")]
        public Int64 AvailableFlashDriveSize { get; set; }

        [BsonElement("avlmemsz")]
        public Int64 AvailableMemorySize { get; set; }

        [BsonElement("btr")]
        public Int32 Battery { get; set; }

        [BsonElement("msg")]
        public string Message { get; set; }

        [BsonElement("stktr")]
        public string StackTrace { get; set; }

        [BsonElement("model")]
        public string Model { get; set; }

        [BsonElement("osver")]
        public string OsVersion { get; set; }

        public Error()
            : base()
        {

        }

        public Error(NameValueCollection keyValues)
            : base(keyValues)
        {
            this.Data = keyValues[Keys.DATA];
            this.EventName = keyValues[Keys.EVENT_NAME];
            this.AvailableFlashDriveSize = long.Parse(keyValues[Keys.AVAILABLE_FLASH_DRIVE_SIZE]);
            this.AvailableMemorySize = long.Parse(keyValues[Keys.AVAILABLE_MEMORY_SIZE]);
            this.Battery = int.Parse(keyValues[Keys.BATTERY]);
            this.Message = keyValues[Keys.ERROR_MESSAGE];
            this.StackTrace = keyValues[Keys.ERROR_STACK_TRACE];
        }

        public void Add(DeviceInfo device)
        {
            this.Model = device.Model;
            this.OsVersion = device.OperatingSystem;
            this.PlatformId = device.PlatformType;
        }
    }
}
