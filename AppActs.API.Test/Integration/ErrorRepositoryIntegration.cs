using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.API.Repository.Interface;
using AppActs.DomainModel;
using AppActs.Core.Exceptions;
using AppActs.API.Repository;
using AppActs.API.DomainModel;

namespace AppActs.API.Test.Integration
{
    [TestClass]
    public class ErrorRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            IErrorRepository iErrorRepository = new ErrorRepository(this.client, this.database);

            ErrorItem errorItem = new ErrorItem
                (
                    this.Application.Id,
                    this.Device.Id,
                    "data",
                    "eventName",
                    new DeviceDynamicInformation(2342344423333332333, 2342344423333332333, 50),
                    new AppActs.DomainModel.Exception.DeviceException("message", "stackTrace", "source", "data"),
                    "screenName",
                    Guid.NewGuid(),
                    DateTime.Now,
                    "1.1"
                );

            iErrorRepository.Save(errorItem);
        }
    }
}
