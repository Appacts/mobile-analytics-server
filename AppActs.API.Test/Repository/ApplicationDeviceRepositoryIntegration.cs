using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppActs.API.Repository.MSTest
{
    [TestClass]
    public class ApplicationDeviceRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            AppActs.API.Repository.Interface.IApplicationDevicesRepository iApplicationDevicesRepository =
                new AppActs.API.Repository.ApplicationDevicesRepository(this.connectionString);

            iApplicationDevicesRepository.Save(this.Application.Id, this.Device.Id, "1.1.1", true, DateTime.Now);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void SaveAndUpdate()
        {
            AppActs.API.Repository.Interface.IApplicationDevicesRepository iApplicationDevicesRepository =
                new AppActs.API.Repository.ApplicationDevicesRepository(this.connectionString);

            iApplicationDevicesRepository.Save(this.Application.Id, this.Device.Id, "1.1.1", true, DateTime.Now);

            iApplicationDevicesRepository.UpdateCurrentActive(this.Application.Id, this.Device.Id, false);

            iApplicationDevicesRepository.Save(this.Application.Id, this.Device.Id, "1.1.2", true, DateTime.Now);
        }

    }
}
