using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.Client.Repository.Interface;
using AppActs.Model;
using AppActs.Model.Enum;

namespace AppActs.Client.Repository.SqlServer.MSTest
{
    [TestClass]
    public class ApplicationRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            IApplicationRepository applicationRepository = new ApplicationRepository(this.Client, this.Database);

            Application application = new Application(Guid.NewGuid().ToString());
            applicationRepository.Save(application);

            Assert.IsNotNull(application.Id);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Update_By_Guid()
        {
            IApplicationRepository applicationRepository = new ApplicationRepository(this.Client, this.Database);

            Application application = new Application(Guid.NewGuid().ToString());
            applicationRepository.Save(application);

            Application applicationLoad = applicationRepository.Find(application.Guid);

            applicationLoad.Active = false;
            applicationLoad.DateModified = DateTime.Now;
            applicationLoad.Name = Guid.NewGuid().ToString();

            applicationRepository.Update(applicationLoad);

            Application applicationUpdated = applicationRepository.Find(application.Guid);

            Assert.IsTrue(applicationUpdated.Active == applicationLoad.Active);
            Assert.IsTrue(applicationUpdated.Name == applicationLoad.Name);
        }



        [TestMethod]
        [TestCategory("Integration")]
        public void Find_By_Name_Return_Object()
        {
            IApplicationRepository applicationRepository = new ApplicationRepository(this.Client, this.Database);

            Application application = new Application(Guid.NewGuid().ToString());
            applicationRepository.Save(application);

            Assert.IsNotNull(applicationRepository.Find(application.Name));
        }


        [TestMethod]
        [TestCategory("Integration")]
        public void Find_By_Name_Return_Null()
        {
            IApplicationRepository applicationRepository = new ApplicationRepository(this.Client, this.Database);
            Assert.IsNull(applicationRepository.Find(Guid.NewGuid().ToString()));
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Save_Platforms_Against_An_Application()
        {
            IApplicationRepository applicationRepository = new ApplicationRepository(this.Client, this.Database);
            IPlatformRepository iPlatformDAL = new PlatformRepository(this.Client, this.Database);

            Application application = new Application(Guid.NewGuid().ToString());
            application.Platforms = iPlatformDAL.FindAll().ToList();
            applicationRepository.Save(application);

            Application applicationFound = applicationRepository.Find(application.Guid);
            Assert.IsNotNull(applicationFound.Platforms);
            Assert.IsTrue(applicationFound.Platforms.Count == application.Platforms.Count);
        }


        [TestMethod]
        [TestCategory("Integration")]
        public void Remove_Platforms_From_Application()
        {
            IApplicationRepository applicationRepository = new ApplicationRepository(this.Client, this.Database);
            IPlatformRepository iPlatformDAL = new PlatformRepository(this.Client, this.Database);

            Application application = new Application(Guid.NewGuid().ToString());
            application.Platforms = iPlatformDAL.FindAll().ToList();
            applicationRepository.Save(application);

            Application applicationFound = applicationRepository.Find(application.Guid);
            applicationFound.Platforms = new List<Platform>();
            applicationRepository.Update(applicationFound);

            Application applicationRemovedPlatforms = applicationRepository.Find(application.Guid);
            Assert.IsTrue(applicationRemovedPlatforms.Platforms.Count == 0);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Get_All_Applications()
        {
            IApplicationRepository applicationRepository = new ApplicationRepository(this.Client, this.Database);

            Application application1 = new Application(Guid.NewGuid().ToString());
            applicationRepository.Save(application1);

            Application application2 = new Application(Guid.NewGuid().ToString());
            applicationRepository.Save(application2);

            Application application3 = new Application(Guid.NewGuid().ToString());
            applicationRepository.Save(application3);

            IEnumerable<Application> applications = applicationRepository.FindAll();

            Assert.IsNotNull(applications);
        }
    }
}
