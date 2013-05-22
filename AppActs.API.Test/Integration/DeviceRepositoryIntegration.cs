using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.API.Repository.Interface;
using AppActs.API.Repository;

namespace AppActs.API.Test.Integration
{
    [TestClass]
    public class DeviceRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Find_NotRealRecord_Null()
        {
            IDeviceRepository iDeviceRepository = new DeviceRepository(this.client, this.database);

            Assert.IsNull(iDeviceRepository.Find(Guid.NewGuid()));
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Find_RealRecord_SavedRecord()
        {
            IDeviceRepository iDeviceRepository = new DeviceRepository(this.client, this.database);

            Assert.IsNotNull(iDeviceRepository.Find(this.Device.Guid));
        }
    }
}
