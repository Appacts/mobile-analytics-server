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
    public class EventRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            IEventRepository iEventRepository = new EventRepository(this.client, this.database);

            EventItem eventItem = new EventItem
                (
                    this.Application.Id,
                    this.Device.Id,
                    "07545197580",
                    AppActs.DomainModel.Enum.EventType.Event,
                    "searched",
                    30,
                    "Main",
                    Guid.NewGuid(),
                    DateTime.Now,
                    "1.1.1.1"
                 );

            iEventRepository.Save(eventItem);
        }
    }
}
