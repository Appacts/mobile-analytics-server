using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using System.Collections.Specialized;
using AppActs.Model.Enum;

namespace AppActs.API.Model.Device
{
    public class DeviceLocation
    {
        public Guid DeviceId { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public ProcessStatusType Status { get; set; }
        public string CountryName { get; set; }
        public string CountryAreaLevelOneName { get; set; }
        public AreaInformationType AreaInformationType { get; set; }

        public DeviceLocation()
        {

        }

        public DeviceLocation(NameValueCollection keyValues)
        {
            this.DeviceId = new Guid(keyValues[Keys.DEVICE_GUID]);
            this.Longitude = decimal.Parse(keyValues[Keys.LOCATION_LONGITUDE]);
            this.Latitude = decimal.Parse(keyValues[Keys.LOCATION_LATITUDE]);
            this.Status = ProcessStatusType.Pending;
            this.AreaInformationType = AreaInformationType.Coordinates;
            this.DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        }
    }
}
