using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace AppActs.API.Model.Device
{
    public class DeviceSummary : Summary
    {
        public List<Aggregate<string>> Locales { get; set; }
        public List<Aggregate<string>> Carriers { get; set; }
        public List<ManufacturerModelAggregate> ManufacturerModels { get; set; }
        public List<Aggregate<string>> OperatingSystems { get; set; }
        public List<Resolution> Resolutions { get; set; }

        public DeviceSummary()
        {

        }

        public DeviceSummary(DeviceInfo deviceInfo, ApplicationInfo applicationInfo)
            : base(deviceInfo, applicationInfo)
        {
            this.Locales = new List<Aggregate<string>>();
            this.Carriers = new List<Aggregate<string>>();
            this.ManufacturerModels = new List<ManufacturerModelAggregate>();
            this.OperatingSystems = new List<Aggregate<string>>();
            this.Resolutions = new List<Resolution>();

            this.Locales.Add(new Aggregate<string>(deviceInfo.Locale));
            this.Carriers.Add(new Aggregate<string>(deviceInfo.Carrier));
            this.ManufacturerModels.Add(new ManufacturerModelAggregate(deviceInfo.Manufactorer, deviceInfo.Model));
            this.OperatingSystems.Add(new Aggregate<string>(deviceInfo.OperatingSystem));
            this.Resolutions.Add(new Resolution(deviceInfo.ResolutionWidth, deviceInfo.ResolutionHeight));
        }
    }
}
