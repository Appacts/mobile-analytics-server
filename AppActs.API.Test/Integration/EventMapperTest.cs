using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using FluentAssertions;
using AppActs.API.DataMapper;
using AppActs.API.Model.Event;
using AppActs.Model.Enum;

namespace AppActs.API.Test.Integration
{
    [TestClass]
    public class EventMapperTest : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save_AppUsageSummary_ValuesIncrement()
        {
            EventMapper eventMapper = new EventMapper(this.client, this.database);
            Guid applicationId = Guid.NewGuid();
            Guid deviceId = Guid.NewGuid();

            AppUsageSummary expected = new AppUsageSummary()
            {
                ApplicationId = applicationId,
                Count = 2,
                Date = date,
                PlatformId = platform,
                Version = version,
                TimeGroup = new TimeOfDayGroup()
                {
                    _10 = 2
                },
                WeekGroup = new DayOfWeekGroup()
                {
                    Wed = 2
                },
                FrequencyUsageGroup = new FrequencyOfUsageGroup(),
                DevicesVisits = new List<DeviceAppVisit>()
                {
                    new DeviceAppVisit(deviceId)
                    {
                        Count = 2
                    }
                },
                NewReturningGroup = new NewReturningGroup(2, 0)
            };

            Event eventItem = new Event()
            {
                ApplicationId = applicationId,
                Date = date,
                Version = version,
                PlatformId = platform,
                DateCreatedOnDevice = dateCreatedOnDevice,
                DeviceId = deviceId,
                DateCreated = date    
            };

            AppUsageSummary summary = new AppUsageSummary(eventItem, null);

            eventMapper.Save(summary);
            eventMapper.Save(summary);

            IMongoQuery query = Query.And
                (
                    Query<AppUsageSummary>.EQ<DateTime>(mem => mem.Date, date),
                    Query<AppUsageSummary>.EQ<Guid>(mem => mem.ApplicationId, applicationId),
                    Query<AppUsageSummary>.EQ<string>(mem => mem.Version, version),
                    Query<AppUsageSummary>.EQ<PlatformType>(mem => mem.PlatformId, platform)
                );

            AppUsageSummary actual = this.GetCollection<AppUsageSummary>().FindOne(query);

            actual.ShouldHave().AllPropertiesBut(x => x.Id)
                .IncludingNestedObjects().EqualTo(expected);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Save_ScreenRoute_ValuesIncrement()
        {
            EventMapper eventMapper = new EventMapper(this.client, this.database);
            Guid applicationId = Guid.NewGuid();
            Guid deviceId = Guid.NewGuid();

            ScreenRouteSummary expected = new ScreenRouteSummary()
            {
                ApplicationId = applicationId,
                Count = 2,
                Date = date,
                PlatformId = platform,
                Version = version,
                ScreenRoutes = new List<ScreenRoute>()
                {
                     new ScreenRoute("screenNameBefore", "screenNameAfter")
                     {
                         Count = 2
                     }
                }
            };

            Event eventItem = new Event()
            {
                ApplicationId = applicationId,
                Date = date,
                Version = version,
                PlatformId = platform,
                DateCreatedOnDevice = dateCreatedOnDevice,
                DeviceId = deviceId,
                DateCreated = date,
                EventTypeId = Model.Enum.EventType.ScreenOpen,
                ScreenName = "screenNameAfter"
            };

            ScreenRouteSummary summary = new ScreenRouteSummary(eventItem, "screenNameBefore", "screenNameAfter");

            eventMapper.Save(summary);
            eventMapper.Save(summary);

            IMongoQuery query = Query.And
                (
                    Query<ScreenRouteSummary>.EQ<DateTime>(mem => mem.Date, date),
                    Query<ScreenRouteSummary>.EQ<Guid>(mem => mem.ApplicationId, applicationId),
                    Query<ScreenRouteSummary>.EQ<string>(mem => mem.Version, version),
                    Query<ScreenRouteSummary>.EQ<PlatformType>(mem => mem.PlatformId, platform)
                );

            ScreenRouteSummary actual = this.GetCollection<ScreenRouteSummary>().FindOne(query);

            actual.ShouldHave().AllPropertiesBut(x => x.Id).IncludingNestedObjects().EqualTo(expected);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Save_ScreenRouteNoLastScreen_ValuesIncrement()
        {
            EventMapper eventMapper = new EventMapper(this.client, this.database);
            Guid applicationId = Guid.NewGuid();
            Guid deviceId = Guid.NewGuid();

            ScreenRouteSummary expected = new ScreenRouteSummary()
            {
                ApplicationId = applicationId,
                Count = 2,
                Date = date,
                PlatformId = platform,
                Version = version,
                ScreenRoutes = new List<ScreenRoute>()
                {
                    new ScreenRoute(string.Empty, "screenNameAfter")
                    {
                         Count = 2
                    }
                }
            };

            Event eventItem = new Event()
            {
                ApplicationId = applicationId,
                Date = date,
                Version = version,
                PlatformId = platform,
                DateCreatedOnDevice = dateCreatedOnDevice,
                DeviceId = deviceId,
                DateCreated = date,
                EventTypeId = Model.Enum.EventType.ScreenOpen,
                ScreenName = "screenNameAfter"
            };

            ScreenRouteSummary summary = new ScreenRouteSummary(eventItem, string.Empty, eventItem.ScreenName);
            eventMapper.Save(summary);
            eventMapper.Save(summary);

            IMongoQuery query = Query.And
                (
                    Query<ScreenRouteSummary>.EQ<DateTime>(mem => mem.Date, date),
                    Query<ScreenRouteSummary>.EQ<Guid>(mem => mem.ApplicationId, applicationId),
                    Query<ScreenRouteSummary>.EQ<string>(mem => mem.Version, version),
                    Query<ScreenRouteSummary>.EQ<PlatformType>(mem => mem.PlatformId, platform)
                );

            ScreenRouteSummary actual = this.GetCollection<ScreenRouteSummary>().FindOne(query);

            actual.ShouldHave().AllPropertiesBut(x => x.Id).IncludingNestedObjects().EqualTo(expected);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Save_ScreenRouteSimiliarRoutes_ValuesIncrementMatchesCorrectly()
        {
            EventMapper eventMapper = new EventMapper(this.client, this.database);
            Guid applicationId = Guid.NewGuid();
            Guid deviceId = Guid.NewGuid();

            ScreenRouteSummary expected = new ScreenRouteSummary()
            {
                ApplicationId = applicationId,
                Count = 2,
                Date = date,
                PlatformId = platform,
                Version = version,
                ScreenRoutes = new List<ScreenRoute>()
                {
                     new ScreenRoute("screenNameBefore", "screenNameAfter")
                     {
                         Count = 1
                     },
                     new ScreenRoute("screenNameOther", "screenNameAfter")
                     {
                         Count = 1
                     }
                }
            };

            Event eventItem = new Event()
            {
                ApplicationId = applicationId,
                Date = date,
                Version = version,
                PlatformId = platform,
                DateCreatedOnDevice = dateCreatedOnDevice,
                DeviceId = deviceId,
                DateCreated = date,
                EventTypeId = Model.Enum.EventType.ScreenOpen,
                ScreenName = "screenNameAfter"
            };

            ScreenRouteSummary summaryBefore = new ScreenRouteSummary(eventItem, "screenNameBefore", eventItem.ScreenName);
            ScreenRouteSummary summaryOther = new ScreenRouteSummary(eventItem, "screenNameOther", eventItem.ScreenName);
            eventMapper.Save(summaryBefore);
            eventMapper.Save(summaryOther);

            IMongoQuery query = Query.And
                (
                    Query<ScreenRouteSummary>.EQ<DateTime>(mem => mem.Date, date),
                    Query<ScreenRouteSummary>.EQ<Guid>(mem => mem.ApplicationId, applicationId),
                    Query<ScreenRouteSummary>.EQ<string>(mem => mem.Version, version),
                    Query<ScreenRouteSummary>.EQ<PlatformType>(mem => mem.PlatformId, platform)
                );

            ScreenRouteSummary actual = this.GetCollection<ScreenRouteSummary>().FindOne(query);

            actual.ShouldHave().AllPropertiesBut(x => x.Id).IncludingNestedObjects().EqualTo(expected);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Save_EventSummary_ValuesIncrement()
        {
            EventMapper eventMapper = new EventMapper(this.client, this.database);
            Guid applicationId = Guid.NewGuid();
            Guid deviceId = Guid.NewGuid();

            EventSummary expected = new EventSummary()
            {
                ApplicationId = applicationId,
                Count = 2,
                Date = date,
                PlatformId = platform,
                Version = version,
                ScreenEvents = new List<EventAggregate>()
                {
                    new EventAggregate("someScreen", "someEvent")
                    {
                        Count = 2
                    }
                }
            };

            Event eventItem = new Event()
            {
                ApplicationId = applicationId,
                Date = date,
                Version = version,
                PlatformId = platform,
                DateCreatedOnDevice = dateCreatedOnDevice,
                ScreenName = "someScreen",
                EventName = "someEvent",
                DeviceId = deviceId,
                DateCreated = date
            };

            EventSummary summary = new EventSummary(eventItem);

            eventMapper.Save(summary);
            eventMapper.Save(summary);

            IMongoQuery query = Query.And
                (
                    Query<AppUsageSummary>.EQ<DateTime>(mem => mem.Date, date),
                    Query<AppUsageSummary>.EQ<Guid>(mem => mem.ApplicationId, applicationId),
                    Query<AppUsageSummary>.EQ<string>(mem => mem.Version, version),
                    Query<AppUsageSummary>.EQ<PlatformType>(mem => mem.PlatformId, platform)
                );

            EventSummary actual = this.GetCollection<EventSummary>().FindOne(query);

            actual.ShouldHave().AllPropertiesBut(x => x.Id)
                .IncludingNestedObjects().EqualTo(expected);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Save_AppUsageDurationSummary_ValuesIncrement()
        {
            EventMapper eventMapper = new EventMapper(this.client, this.database);
            Guid applicationId = Guid.NewGuid();
            Guid deviceId = Guid.NewGuid();

            AppUsageDurationSummary expected = new AppUsageDurationSummary()
            {
                ApplicationId = applicationId,
                Count = 2,
                Date = date,
                PlatformId = platform,
                Version = version,
                LengthGroup = new SessionLengthGroup()
                {
                    _20sec = 2
                }
            };

            Event eventItem = new Event()
            {
                ApplicationId = applicationId,
                Date = date,
                Version = version,
                PlatformId = platform,
                DateCreatedOnDevice = dateCreatedOnDevice,
                ScreenName = "someScreen",
                EventName = "someEvent",
                DeviceId = deviceId,
                DateCreated = date,
                Length = 20000
            };

            AppUsageDurationSummary summary = new AppUsageDurationSummary(eventItem);

            eventMapper.Save(summary);
            eventMapper.Save(summary);

            IMongoQuery query = Query.And
                (
                    Query<AppUsageSummary>.EQ<DateTime>(mem => mem.Date, date),
                    Query<AppUsageSummary>.EQ<Guid>(mem => mem.ApplicationId, applicationId),
                    Query<AppUsageSummary>.EQ<string>(mem => mem.Version, version),
                    Query<AppUsageSummary>.EQ<PlatformType>(mem => mem.PlatformId, platform)
                );

            AppUsageDurationSummary actual = 
                this.GetCollection<AppUsageDurationSummary>().FindOne(query);

            actual.ShouldHave().AllPropertiesBut(x => x.Id)
                .IncludingNestedObjects().EqualTo(expected);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Save_ContentLoadSummary_ValuesIncrement()
        {
            EventMapper eventMapper = new EventMapper(this.client, this.database);
            Guid applicationId = Guid.NewGuid();
            Guid deviceId = Guid.NewGuid();

            ContentLoadSummary expected = new ContentLoadSummary()
            {
                ApplicationId = applicationId,
                Count = 2,
                Date = date,
                PlatformId = platform,
                Version = version,
                Loads = new List<ContentDurationAggregate>()
                {
                    new ContentDurationAggregate("someScreen", "someContent", 46)
                    {
                        Count = 2
                    }
                }
            };

            Event eventItem = new Event()
            {
                ApplicationId = applicationId,
                Date = date,
                Version = version,
                PlatformId = platform,
                DateCreatedOnDevice = dateCreatedOnDevice,
                ScreenName = "someScreen",
                EventName = "someContent",
                DeviceId = deviceId,
                DateCreated = date,
                Length = 23000
            };

            ContentLoadSummary summary = new ContentLoadSummary(eventItem);

            eventMapper.Save(summary);
            eventMapper.Save(summary);

            IMongoQuery query = Query.And
                (
                    Query<AppUsageSummary>.EQ<DateTime>(mem => mem.Date, date),
                    Query<AppUsageSummary>.EQ<Guid>(mem => mem.ApplicationId, applicationId),
                    Query<AppUsageSummary>.EQ<string>(mem => mem.Version, version),
                    Query<AppUsageSummary>.EQ<PlatformType>(mem => mem.PlatformId, platform)
                );

            ContentLoadSummary actual =
                this.GetCollection<ContentLoadSummary>().FindOne(query);

            actual.ShouldHave().AllPropertiesBut(x => x.Id)
                .IncludingNestedObjects().EqualTo(expected);
        }
    }
}
