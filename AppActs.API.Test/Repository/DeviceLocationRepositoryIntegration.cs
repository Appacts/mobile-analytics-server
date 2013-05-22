using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.API.Repository.Interface;
using AppActs.DomainModel;

namespace AppActs.API.Repository.MSTest
{
    [TestClass]
    public class DeviceLocationRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            IDeviceLocationRepository iDeviceLocationRepository =
                new DeviceLocationRepository(this.connectionString);

            DeviceLocation deviceLocation = 
                new DeviceLocation
                (
                    1,
                    222.222333m,
                    399.220393m
                );

            iDeviceLocationRepository.Save(this.Application.Id, this.Device.Id, deviceLocation);
        }
    }
}
