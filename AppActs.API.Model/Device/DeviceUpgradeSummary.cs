using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using AppActs.Model.Enum;

namespace AppActs.API.Model.Device
{
    public class DeviceUpgradeSummary
    {
        public string Version { get; set; }
        public DateTime Date { get; set; }
        public Guid ApplicationId { get; set; }
        public PlatformType PlatformType { get; set; }
        public int Count { get; set; }

        public DeviceUpgradeSummary(string version, DateTime date, Guid applicationId, PlatformType platformType)
        {
            this.Version = version;
            this.Date = date;
            this.ApplicationId = applicationId;
            this.PlatformType = platformType;
            this.Count += 1;
        }
    }
}
