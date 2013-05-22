using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.API.Repository.Interface;
using AppActs.DomainModel;
using AppActs.API.Repository;
using AppActs.API.DomainModel;
using AppActs.DomainModel.Exception;

namespace AppActs.API.Test.Integration
{
    [TestClass]
    public class SystemErrorRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            ISystemErrorRepository iSystemErrorRepository = new SystemErrorRepository(this.client, this.database);

            SystemError systemError = new SystemError
                (
                    this.Application.Id,
                    this.Device.Id,
                    new DeviceException("message", "stackTrace", "source", "data"),
                    new DomainModel.System(AppActs.DomainModel.Enum.PlatformType.iOS, "1.1"),
                    DateTime.Now,
                    "6.5"
                );

            iSystemErrorRepository.Save(systemError);
        }
    }
}
