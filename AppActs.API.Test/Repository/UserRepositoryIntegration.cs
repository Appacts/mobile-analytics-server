using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.Core.Exceptions;
using AppActs.API.Repository.Interface;
using AppActs.DomainModel;

namespace AppActs.API.Repository.MSTest
{
    [TestClass]
    public class UserRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(DataAccessLayerException))]
        public void SaveThrowsDataAccessLayerException()
        {
            IUserRepository iUserRepository = new UserRepository(this.connectionString);
            iUserRepository.Save(0, 0, new User(23, AppActs.DomainModel.Enum.SexType.Male, Guid.NewGuid(), DateTime.Now));
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            IUserRepository iUserRepository = new UserRepository(this.connectionString);

            User user = new User
                (
                    18,
                    AppActs.DomainModel.Enum.SexType.Male,
                    Guid.NewGuid(),
                    DateTime.Now
                );

            iUserRepository.Save(this.Application.Id, this.Device.Id, user);
        }
    }
}
