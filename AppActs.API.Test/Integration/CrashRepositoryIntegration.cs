using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.API.Repository.Interface;
using AppActs.Core.Exceptions;
using AppActs.DomainModel;
using AppActs.API.Repository;
using AppActs.API.DomainModel;

namespace AppActs.API.Test.Integration
{
    [TestClass]
    public class CrashRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            ICrashRepository iCrashRepository = new CrashRepository(this.client, this.database);

            iCrashRepository.Save(new Crash(this.Application.Id, this.Device.Id, "1.1", Guid.NewGuid(), DateTime.Now));
        }
    }
}
