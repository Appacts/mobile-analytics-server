using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.API.Repository.Interface;
using AppActs.DomainModel;
using AppActs.Core.Exceptions;

namespace AppActs.API.Repository.MSTest
{
    [TestClass]
    public class ErrorRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(DataAccessLayerException))]
        public void SaveThrowsDataAccessLayerException()
        {
            IErrorRepository iErrorRepository = new ErrorRepository(this.connectionString);

            iErrorRepository.Save
                (
                    0, 
                    0, 
                    new ErrorItem("07545197580", "searched",
                    new DeviceDynamicInformation(2342344423333332333, 2342344423333332333, 10), 
                    new AppActs.DomainModel.Exception.DeviceException("some error", "strack trace", "source", "data"),
                    "someScreen",
                    Guid.NewGuid(),
                    DateTime.Now,
                    "1.1.1.1")
                );
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            IErrorRepository iErrorRepository = new ErrorRepository(this.connectionString);

            ErrorItem errorItem = new ErrorItem
                (
                    "data",
                    "eventName",
                    new DeviceDynamicInformation(2342344423333332333, 2342344423333332333, 50),
                    new AppActs.DomainModel.Exception.DeviceException("message", "stackTrace", "source", "data"),
                    "screenName",
                    Guid.NewGuid(),
                    DateTime.Now,
                    "1.1"
                );

            iErrorRepository.Save(this.Application.Id, this.Device.Id, errorItem);
        }
    }
}
