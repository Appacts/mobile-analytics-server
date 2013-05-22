using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.DomainModel;

namespace AppActs.API.Repository.MSTest
{
    [TestClass]
    public class AccountRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void IsActiveFail()
        {
            AppActs.API.Repository.Interface.IAccountRepository iAccountRepository =
                new AppActs.API.Repository.AccountRepository(this.connectionString);

            Assert.IsFalse(iAccountRepository.Find(true, Guid.Empty) > 0);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void IsActive()
        {
            AppActs.API.Repository.Interface.IAccountRepository iAccountRepositoryDevice =
                new AppActs.API.Repository.AccountRepository(this.connectionString);

            int accountCount = 
                iAccountRepositoryDevice.Find(true, this.Account.Guid);

            Assert.IsTrue(accountCount > 0);
        }
    }
}
