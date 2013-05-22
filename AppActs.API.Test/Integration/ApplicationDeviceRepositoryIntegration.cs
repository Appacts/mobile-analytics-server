using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.DomainModel;

namespace AppActs.API.Test.Integration
{
    [TestClass]
    public class ApplicationDeviceRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            AppActs.API.Repository.Interface.IApplicationDevicesRepository iApplicationDevicesRepository =
                new AppActs.API.Repository.ApplicationDevicesRepository(this.client, this.database);

            iApplicationDevicesRepository.Save(new ApplicationDevice(this.Application.Id, this.Device.Id, "1.1.1", true, DateTime.Now));
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Update()
        {
            AppActs.API.Repository.Interface.IApplicationDevicesRepository iApplicationDevicesRepository =
                new AppActs.API.Repository.ApplicationDevicesRepository(this.client, this.database);

            DomainModel.Device device = this.GenerateNewDevice();

            ApplicationDevice applicationDevice = new ApplicationDevice(this.Application.Id, this.Device.Id, "1.2.3", true, DateTime.Now);

            iApplicationDevicesRepository.Save(applicationDevice);

            applicationDevice.Active = false;
            iApplicationDevicesRepository.Update(applicationDevice);

            Assert.IsNull(iApplicationDevicesRepository.FindCurrentActiveByDevice(device.Id));
        }

    }
}
