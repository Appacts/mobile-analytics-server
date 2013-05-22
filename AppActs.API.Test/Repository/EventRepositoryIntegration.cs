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
    public class EventRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(DataAccessLayerException))]
        public void SaveThrowsDataAccessLayerException()
        {
            IEventRepository iEventRepository = new EventRepository(this.connectionString);

            iEventRepository.Save(0, 0, new EventItem("07545197580", AppActs.DomainModel.Enum.EventType.Event, "searched", 30, "Main", Guid.NewGuid(), DateTime.Now, "1.1.1.1"));
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            IEventRepository iEventRepository = new EventRepository(this.connectionString);

            EventItem eventItem = new EventItem
                (
                    "data",
                    AppActs.DomainModel.Enum.EventType.ApplicationOpen,
                    "openScreen",
                    100,
                    "Main",
                    Guid.NewGuid(),
                    DateTime.Now,
                    "1.1"
                );

            iEventRepository.Save(this.Application.Id, this.Device.Id, eventItem);
        }
    }
}
