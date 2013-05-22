using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Service.Interface;
using AppActs.Model.Enum;
using AppActs.Model;
using MongoDB.Bson;
using AppActs.API.Model;
using System.Xml.Serialization;
using AppActs.API.Model.User;
using AppActs.API.Model.Device;
using AppActs.API.Model.User;

namespace AppActs.API.DataUploader
{
    public class Device
    {
        readonly IDeviceService deviceService;

        public Guid DeviceId { get; set; }
        public string Version { get; set; }
        public DateTime DateCreated { get; set; }

        private Guid applicationId;

        public Device()
        {

        }

        public Device(IDeviceService iDeviceService, string version, DateTime timeWhenUsed, Guid applicationId)
        {
            this.deviceService = iDeviceService;
            this.Version = version;
            this.DateCreated = timeWhenUsed;
            this.applicationId = applicationId;
        }

        public void Register(string deviceModel, PlatformType platformType, string carrier, string platformOS, int timeZoneOffset, string locale, string manufac, Tuple<int, int> resolution)
        {
            DeviceInfo deviceInfo = new DeviceInfo()
            {
                Model = deviceModel,
                PlatformType = platformType,
                Carrier = carrier,
                OperatingSystem = platformOS,
                TimeZoneOffset = timeZoneOffset,
                Locale = locale,
                ResolutionHeight = resolution.Item1,
                ResolutionWidth = resolution.Item2,
                Manufactorer = manufac,
                DateCreated = this.DateCreated,
                Date = this.DateCreated.Date
            };

            ApplicationInfo applicationInfo = new ApplicationInfo()
            {
                ApplicationId = applicationId,
                Version = this.Version
            };

            deviceService.Log(deviceInfo, applicationInfo);

            DeviceId = deviceInfo.Guid;
        }

        public void Location(string country, string countryCode,  string countryLevel1, string countryLevel1Code)
        {
            deviceService.Log
                (
                    new DeviceLocation()
                    {
                        DeviceId = this.DeviceId,
                        Longitude = 0,
                        Latitude = 0
                    },
                    new ApplicationInfo()
                    {
                        ApplicationId = applicationId,
                        Version = this.Version
                    }
                );
        }

        public void User(SexType sexType, int age)
        {
            deviceService.Log
                (
                    new AppUser() 
                    { 
                        ApplicationId = applicationId,
                        DeviceId = this.DeviceId,
                        Age = age,
                        Sex = sexType,
                        SessionId = Guid.NewGuid(),
                        DateCreated = DateCreated,
                        Date = DateCreated.Date,
                        DateCreatedOnDevice = DateCreated
                    }
                );
        }
    }
}
