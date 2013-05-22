using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.API.Repository.Interface;

namespace AppActs.API.Repository.MSTest
{
    [TestClass]
    public class DeviceRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void LoadFail()
        {
            IDeviceRepository iDeviceRepository = new DeviceRepository(this.connectionString);

            Assert.IsNull(iDeviceRepository.Find(Guid.NewGuid()));
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Load()
        {
            IDeviceRepository iDeviceRepository = new DeviceRepository(this.connectionString);

            Assert.IsNotNull(iDeviceRepository.Find(this.Device.Guid));
        }
    }
}
