using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.API.Repository.Interface;
using AppActs.Core.Exceptions;
using AppActs.DomainModel;

namespace AppActs.API.Repository.MSTest
{
    [TestClass]
    public class CrashRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(DataAccessLayerException))]
        public void SaveThrowsDataAccessLayerException()
        {
            ICrashRepository iCrashRepository = new CrashRepository(this.connectionString);
            
            iCrashRepository.Save(1, 1, new Crash("1.1.1.1", Guid.NewGuid(), DateTime.Now));
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            ICrashRepository iCrashRepository = new CrashRepository(this.connectionString);

            iCrashRepository.Save(this.Application.Id, this.Device.Id, new Crash("1.1", Guid.NewGuid(), DateTime.Now));
        }
    }
}
