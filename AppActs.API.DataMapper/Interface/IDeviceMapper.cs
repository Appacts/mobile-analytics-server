using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using AppActs.API.Model.Device;

namespace AppActs.API.DataMapper.Interface
{
    public interface IDeviceMapper
    {
        DeviceInfo Find(Guid id);
        void Save(DeviceInfo device);
        void Save(DeviceLocation location);
        void Save(DeviceSummary summary);
        void Save(DeviceUpgradeSummary summary);
    }
}
