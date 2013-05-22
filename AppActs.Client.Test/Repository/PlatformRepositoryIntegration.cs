using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.Client.Repository.Interface;
using AppActs.DomainModel;
using AppActs.Client.DomainModel;

namespace AppActs.Client.Repository.SqlServer.MSTest
{
    [TestClass]
    public class PlatformRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Load()
        {
            IPlatformRepository iPlatformDAL = new PlatformRepository(this.Client, this.Database);

            Assert.IsTrue(iPlatformDAL.FindAll().Count() > 0);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void GetPlatformSDK()
        {
            IPlatformRepository iPlatformDAL = new PlatformRepository(this.Client, this.Database);

            IEnumerable<Platform> platforms = iPlatformDAL.FindAll();

            foreach (Platform platform in platforms)
            {
                Platform platformSDK = iPlatformDAL.Find(platform.Id);
                Assert.IsTrue(platformSDK.Id > 0);
                Assert.IsNotNull(platformSDK.Name);
                Assert.IsNotNull(platformSDK.Url);
                Assert.IsNotNull(platformSDK.UrlTitle);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void LoadByGuid()
        {
            IPlatformRepository iPlatformDAL = new PlatformRepository(this.Client, this.Database);

            IEnumerable<Platform> platforms = iPlatformDAL.FindAll();

            foreach (Platform platform in platforms)
            {
                Platform platformToCheck = iPlatformDAL.Find(platform.Guid);
                Assert.IsTrue(platformToCheck.Id > 0);
                Assert.IsNotNull(platformToCheck.Name);
            }  
        }
    }
}
