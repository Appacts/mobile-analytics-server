using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.DomainModel;

namespace AppActs.API.Repository.MSTest
{
    [TestClass]
    public class ApplicationRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void GetNull()
        {
            AppActs.API.Repository.Interface.IApplicationRepository iApplicationRepositoryDevice =
                new AppActs.API.Repository.ApplicationRepository(this.connectionString);

            Application application = 
                iApplicationRepositoryDevice.Get(Guid.NewGuid());

            Assert.IsNull(application);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Get()
        {
            AppActs.API.Repository.Interface.IApplicationRepository iApplicationRepositoryDevice =
                new AppActs.API.Repository.ApplicationRepository(this.connectionString);

            Application applicationLoad =
                iApplicationRepositoryDevice.Get(this.Application.Guid);

            Assert.IsNotNull(applicationLoad);
            Assert.IsTrue(applicationLoad.Guid == this.Application.Guid);
        }



    }
}
