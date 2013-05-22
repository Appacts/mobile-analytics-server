using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.API.Repository.Interface;
using AppActs.DomainModel;
using AppActs.DomainModel.Enum;

namespace AppActs.API.Repository.MSTest
{
    [TestClass]
    public class DeviceManufacturerInfoRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            IDeviceManufacturerInfoRepository iDeviceManufacturerInfoRepository =
                new DeviceManufacturerInfoRepository(this.connectionString);

            DeviceManufacturerInfo deviceManufacturerInfo =
                new DeviceManufacturerInfo(PlatformType.Blackberry, 
                    Guid.NewGuid().ToString().Substring(0, 10), 2, 800, 800);

            iDeviceManufacturerInfoRepository.Save(deviceManufacturerInfo);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void GetByDeviceTypeAndModelFail()
        {
            IDeviceManufacturerInfoRepository iDeviceManufacturerInfoRepository =
                new DeviceManufacturerInfoRepository(this.connectionString);

            DeviceManufacturerInfo deviceManufacturerInfo = iDeviceManufacturerInfoRepository
                .Find(PlatformType.Blackberry, 
                Guid.NewGuid().ToString().Substring(0, 10));

            Assert.IsNull(deviceManufacturerInfo);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void GetByDeviceTypeAndModel()
        {
            IDeviceManufacturerInfoRepository iDeviceManufacturerInfoRepository =
                new DeviceManufacturerInfoRepository(this.connectionString);

            DeviceManufacturerInfo deviceManufacturerInfo =
                new DeviceManufacturerInfo(PlatformType.Blackberry,
                    Guid.NewGuid().ToString().Substring(0, 10), 2, 800, 800);

            iDeviceManufacturerInfoRepository.Save(deviceManufacturerInfo);

            DeviceManufacturerInfo deviceManufacturerInfoRetrieved = iDeviceManufacturerInfoRepository.Find(PlatformType.Blackberry, deviceManufacturerInfo.Model);

            Assert.IsNotNull(deviceManufacturerInfoRetrieved);
            Assert.IsTrue(deviceManufacturerInfo.DeviceType == deviceManufacturerInfoRetrieved.DeviceType);
            Assert.IsTrue(deviceManufacturerInfo.Model == deviceManufacturerInfoRetrieved.Model);
            Assert.IsTrue(deviceManufacturerInfo.ResolutionHeight == deviceManufacturerInfoRetrieved.ResolutionHeight);
            Assert.IsTrue(deviceManufacturerInfo.ResolutionWidth == deviceManufacturerInfoRetrieved.ResolutionWidth);
            Assert.IsTrue(deviceManufacturerInfo.ManufacturerId == deviceManufacturerInfoRetrieved.ManufacturerId);
        }
    }
}
