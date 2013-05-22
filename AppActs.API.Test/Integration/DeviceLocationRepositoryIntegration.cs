using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.API.Repository.Interface;
using AppActs.DomainModel;
using AppActs.API.Repository;
using AppActs.API.DomainModel;

namespace AppActs.API.Test.Integration
{
    [TestClass]
    public class DeviceLocationRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            IDeviceLocationRepository iDeviceLocationRepository =
                new DeviceLocationRepository(this.client, this.database);

            DeviceLocation deviceLocation = 
                new DeviceLocation
                (
                    this.Device.Id,
                    222.222333m,
                    399.220393m
                );

            iDeviceLocationRepository.Save(deviceLocation);
        }
    }
}
