using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.API.Repository.Interface;
using AppActs.DomainModel;

namespace AppActs.API.Repository.MSTest
{
    [TestClass]
    public class SystemErrorRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            ISystemErrorRepository iSystemErrorRepository = new SystemErrorRepository(this.connectionString);

            SystemError systemError = new SystemError
                (
                    new AppActs.DomainModel.Exception.DeviceException("message", "stackTrace", "source", "data"),
                    new AppActs.DomainModel.System(AppActs.DomainModel.Enum.PlatformType.iOS, "1.1"),
                    DateTime.Now,
                    "6.5"
                );

            iSystemErrorRepository.Save(this.Application.Id, this.Device.Id, systemError);
        }
    }
}
