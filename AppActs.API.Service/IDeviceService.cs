using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Model;
using MongoDB.Bson;
using AppActs.API.Model.Crash;
using AppActs.API.Model.Device;
using AppActs.API.Model.Error;
using AppActs.API.Model.Event;
using AppActs.API.Model.Feedback;
using AppActs.API.Model.SystemError;
using AppActs.API.Model.User;
using AppActs.API.Model;
using AppActs.API.Model.Upgrade;

namespace AppActs.API.Service.Interface
{
    public interface IDeviceService
    {
        void Log(Crash crash);
        void Log(DeviceInfo deviceInformation, ApplicationInfo applicationInfo);
        void Log(Error error);
        void Log(Event eventItem);
        void Log(Feedback feedback);
        void Log(SystemError systemError);
        void Log(AppUser user);
        void Log(DeviceLocation deviceLocation, ApplicationInfo applicationInfo);
        void Log(UpgradeInfo upgradeInfo);
    }
}
