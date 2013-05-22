using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.API.DataMapper;
using MongoDB.Bson;
using AppActs.API.Model.Event;
using MongoDB.Driver;
using AppActs.Model.Enum;
using AppActs.API.Model.Device;
using FluentAssertions;
using AppActs.API.Model;
using MongoDB.Driver.Builders;

namespace AppActs.API.Test.Integration
{
    [TestClass]
    public class DeviceMapperTest : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save_DeviceSummary_ValuesIncrement()
        {
            DeviceMapper deviceMapper = new DeviceMapper(this.client, this.database);
            Guid applicationId = Guid.NewGuid();
            Guid deviceId = Guid.NewGuid();

            DeviceSummary expected = new DeviceSummary()
            {
                ApplicationId = applicationId,
                Count = 2,
                Date = date,
                PlatformId = platform,
                Version = version,
                Carriers = new List<Aggregate<string>>()
                {
                    new Aggregate<string>()
                    {
                        Key = "o2",
                        Count = 2
                    }
                },
                Locales = new List<Aggregate<string>>()
                {
                    new Aggregate<string>()
                    {
                        Key = "EN",
                        Count = 2
                    }
                },
                ManufacturerModels = new List<ManufacturerModelAggregate>()
                {
                    new ManufacturerModelAggregate("HTC", "OneX")
                    {
                         Count = 2
                    }
                },
                OperatingSystems = new List<Aggregate<string>>()
                {
                     new Aggregate<string>()
                     {
                         Key = "2.2.2.2",
                         Count = 2
                     }
                },
                Resolutions = new List<Resolution>()
                {
                     new Resolution(900, 300)
                     {
                          Count = 2
                     }
                }
            };

            DeviceSummary summary = new DeviceSummary()
            {
                ApplicationId = applicationId,
                Date = date,
                Version = version,
                PlatformId = platform,
                Locales = new List<Aggregate<string>>()
                {
                    new Aggregate<string>("EN")
                },
                Carriers = new List<Aggregate<string>>()
                {
                    new Aggregate<string>("o2")
                },
                ManufacturerModels = new List<ManufacturerModelAggregate>()
                {
                    new ManufacturerModelAggregate("HTC", "OneX")
                },
                OperatingSystems = new List<Aggregate<string>>()
                {
                    new Aggregate<string>("2.2.2.2")
                },
                Resolutions = new List<Resolution>()
                {
                    new Resolution(900, 300)
                }
            };

            deviceMapper.Save(summary);
            deviceMapper.Save(summary);

            IMongoQuery query = Query.And
                (
                    Query<DeviceSummary>.EQ<DateTime>(mem => mem.Date, date),
                    Query<DeviceSummary>.EQ<Guid>(mem => mem.ApplicationId, applicationId),
                    Query<DeviceSummary>.EQ<string>(mem => mem.Version, version),
                    Query<DeviceSummary>.EQ<PlatformType>(mem => mem.PlatformId, platform)
                );

            DeviceSummary actual = this.GetCollection<DeviceSummary>().FindOne(query);

            actual.ShouldHave().AllPropertiesBut(x => x.Id)
                .IncludingNestedObjects().EqualTo(expected);
        }
    }
}
