using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Repository.Interface;
using AppActs.Core.Exceptions;
using System.Data.SqlClient;
using System.Data;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using AppActs.Repository;
using MongoDB.Bson;
using AppActs.API.DataMapper.Interface;
using AppActs.API.Model.Device;

namespace AppActs.API.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        readonly IDeviceMapper deviceMapper;

        public DeviceRepository(IDeviceMapper deviceMapper)
        {
            this.deviceMapper = deviceMapper;
        }

        public DeviceInfo Find(Guid id)
        {
            return this.deviceMapper.Find(id);
        }

        public void Save(DeviceInfo device)
        {
            this.deviceMapper.Save(device);
        }

        public void Save(DeviceLocation location)
        {
            this.deviceMapper.Save(location);
        }

        public void Save(DeviceSummary summary)
        {
            this.deviceMapper.Save(summary);
        }

        public void Save(DeviceUpgradeSummary summary)
        {
            this.deviceMapper.Save(summary);
        }
    }
}
