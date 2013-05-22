using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.Client.Repository.Interface;
using AppActs.DomainModel;
using AppActs.DomainModel.Enum;
using AppActs.Client.DomainModel;
using MongoDB.Bson;

namespace AppActs.Client.Repository.SqlServer.MSTest
{
    [TestClass]
    public class UserRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            IUserRepository userRepository = new UserRepository(this.Client, this.Database);

            User accountUser = new User("name", Guid.NewGuid().ToString(), "password");
            userRepository.Save(accountUser);

            Assert.IsTrue(accountUser.Id != ObjectId.Empty);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Return_Null_For_Wrong_Email()
        {
            IUserRepository userRepository = new UserRepository(this.Client, this.Database);
            Assert.IsNull(userRepository.Find(Guid.NewGuid().ToString()));
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Update()
        {
            IUserRepository userRepository = new UserRepository(this.Client, this.Database);

            User accountUser = new User("name", Guid.NewGuid().ToString(), "password");
            userRepository.Save(accountUser);

            User accountUserLoaded = userRepository.Find(accountUser.Id);

            accountUserLoaded.Active = false;
            accountUserLoaded.Password = "password";
            accountUserLoaded.DateModified = DateTime.Now;

            userRepository.Update(accountUserLoaded);

            User accountUserUpdate = userRepository.Find(accountUser.Id);

            Assert.IsTrue(accountUserUpdate.Active == accountUserLoaded.Active);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Find_By_Guid()
        {
            IUserRepository userRepository = new UserRepository(this.Client, this.Database);

            User accountUser = new User("name",  Guid.NewGuid().ToString(), "password");
            userRepository.Save(accountUser);

            User accountUserLoaded = userRepository.Find(accountUser.Guid);
            Assert.IsTrue(accountUserLoaded.Id == accountUserLoaded.Id);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Find_By_Email_And_Password()
        {
            IUserRepository userRepository = new UserRepository(this.Client, this.Database);

            User accountUser = new User("name", Guid.NewGuid().ToString(), "password");
            userRepository.Save(accountUser);

            User accountUserFound = userRepository.Find(accountUser.Email, accountUser.Password);
            Assert.IsNotNull(accountUserFound);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Find_By_Email()
        {
            IUserRepository userRepository = new UserRepository(this.Client, this.Database);

            User accountUser = new User("name", Guid.NewGuid().ToString(), "password");
            userRepository.Save(accountUser);

            User accountUserFound = userRepository.Find(accountUser.Email);
            Assert.IsNotNull(accountUserFound);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Find_By_ForgotPassword()
        {
            IUserRepository userRepository = new UserRepository(this.Client, this.Database);

            User accountUser = new User("name", Guid.NewGuid().ToString(), "password");
            userRepository.Save(accountUser);

            accountUser.ForgotPasswordGuid = Guid.NewGuid();
            userRepository.Update(accountUser);

            User userFound = userRepository.FindByForgotPassword(accountUser.ForgotPasswordGuid);
            Assert.IsNotNull(userFound);
        }
        
    }
}
