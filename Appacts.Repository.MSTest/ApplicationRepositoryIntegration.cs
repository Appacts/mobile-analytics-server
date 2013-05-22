using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.Repository.Interface;
using System.Configuration;
using AppActs.Model;
using FluentAssertions;

namespace AppActs.Repository.SqlServer.MSTest
{
    [TestClass]
    public class ApplicationRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Load_NoApplication_Null()
        {
            IApplicationRepository applicationRepository = new ApplicationRepository(this.client, this.database);
            Application application = applicationRepository.Find(Guid.Empty);
            Assert.IsNull(application);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Load_FindApplication_Match()
        {
            IApplicationRepository applicationRepository = new ApplicationRepository(this.client, this.database);

            Application expected = new Application()
            {
                Guid = Guid.NewGuid(),
                Active = true,
                DateCreated = new DateTime(2013, 01, 01),
                DateModified = new DateTime(2013, 01, 01),
                Name = "Testing App",
                Platforms = new List<Platform>()
            };

            this.GetCollection<Application>().Insert(expected);

            Application actual = applicationRepository.Find(expected.Guid);

            expected.ShouldHave().AllProperties().EqualTo(actual);
        }

    }
}
