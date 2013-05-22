using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using System.Collections.Specialized;

namespace AppActs.API.Model.SystemError
{
    public class SystemError
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateCreatedOnDevice { get; set; }
        public DateTime Date { get; private set; }
        public string Version { get; set; }
        public int PlatformId { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Data { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid DeviceId { get; set; }

        public SystemError(NameValueCollection keyValues)
        {
            this.Version = keyValues[Keys.SYSTEM_VERSION];
            this.PlatformId = int.Parse(keyValues[Keys.PLATFORM_TYPE]);
            this.Message = keyValues[Keys.ERROR_MESSAGE];
            this.StackTrace = keyValues[Keys.ERROR_STACK_TRACE];
            this.Data = keyValues[Keys.DATA];
            this.ApplicationId = new Guid(keyValues[Keys.APPLICATION_GUID]);
            this.DeviceId = new Guid(keyValues[Keys.DEVICE_GUID]);
            this.DateCreated = DateTime.Now;
            this.DateCreatedOnDevice = DateTime.Parse(keyValues[Keys.DATE_CREATED]);
            this.DateCreated = DateTime.Now;
            this.Date = DateTime.Today;
        }
    }
}
