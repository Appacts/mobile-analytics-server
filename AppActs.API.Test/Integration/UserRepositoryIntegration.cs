using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.Core.Exceptions;
using AppActs.API.Repository.Interface;
using AppActs.DomainModel;
using AppActs.API.Repository;
using AppActs.API.DomainModel;

namespace AppActs.API.Test.Integration
{
    [TestClass]
    public class UserRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            IAppUserRepository iUserRepository = new AppUserRepository(this.client, this.database);

            AppUser user = new AppUser
                (
                    this.Application.Id,
                    this.Device.Id,
                    18,
                    AppActs.DomainModel.Enum.SexType.Male,
                    Guid.NewGuid(),
                    DateTime.Now
                );

            iUserRepository.Save(user);
        }
    }
}
