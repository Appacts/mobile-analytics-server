using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.API.Repository.Interface;

namespace AppActs.API.Repository.MSTest
{
    [TestClass]
    public class ManufacturerRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            IManufacturerRepository iManufacturer = new ManufacturerRepository(this.connectionString);
            int manufacturerId = iManufacturer.Save(Guid.NewGuid().ToString().Substring(0, 6));
            Assert.IsTrue(manufacturerId > 0);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void SaveAndGet()
        {
            IManufacturerRepository iManufacturer = new ManufacturerRepository(this.connectionString);
            string name = Guid.NewGuid().ToString().Substring(0, 6);

            int manufacturerSavedId = iManufacturer.Save(name);
            int? manufacturerGetId = iManufacturer.Find(name);

            Assert.IsNotNull(manufacturerGetId);
            Assert.IsTrue(manufacturerSavedId == manufacturerGetId.Value);
        }
    }
}
