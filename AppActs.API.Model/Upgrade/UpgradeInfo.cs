using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using System.Collections.Specialized;

namespace AppActs.API.Model.Upgrade
{
    public class UpgradeInfo
    {
        public Guid ApplicationId { get; set; }
        public Guid DeviceId { get; set; }
        public string Version { get; set; }

        public UpgradeInfo()
        {

        }

        public UpgradeInfo(NameValueCollection keyValues)
        {
            this.ApplicationId = new Guid(keyValues[Keys.APPLICATION_GUID]);
            this.DeviceId = new Guid(keyValues[Keys.DEVICE_GUID]);
            this.Version = keyValues[Keys.VERSION];
        }
    }
}
