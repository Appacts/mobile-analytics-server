using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace AppActs.API.Model.Event
{
    public class DeviceAppVisit
    {
        public Guid DeviceId { get; set; }
        public int Count { get; set; }

        public DeviceAppVisit(Guid deviceId)
        {
            this.DeviceId = deviceId;
        }

        public DeviceAppVisit CopyOnlyKey()
        {
            return new DeviceAppVisit(this.DeviceId);
        }
    }
}
