using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.API.DataMapper;
using AppActs.API.Model.Crash;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using AppActs.Model.Enum;
using FluentAssertions;

namespace AppActs.API.Test.Integration
{
    [TestClass]
    public class CrashMapperTest : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save_RawRecord_Saves()
        {
            CrashMapper crashMapper = new CrashMapper(this.client, this.database);
            Guid applicationId = Guid.NewGuid();
            Guid deviceId = Guid.NewGuid();

            Crash crash = new Crash()
            {
                ApplicationId = applicationId,
                DeviceId = deviceId,
                SessionId = Guid.NewGuid(),
                DateCreatedOnDevice = dateCreatedOnDevice,
                Version = version,
                Date = date,
                DateCreated = dateCreatedOnDevice,
                PlatformId = platform
            };

            crashMapper.Save(crash);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Save_SummaryRecord_MatchesSummary()
        {
            CrashMapper crashMapper = new CrashMapper(this.client, this.database);
            Guid applicationId = Guid.NewGuid();

            CrashSummary expected = new CrashSummary()
            {
                ApplicationId = applicationId,
                Count = 2,
                Date = date,
                PlatformId = platform,
                Version = version
            };

            Crash crash = new Crash()
            {
                ApplicationId = applicationId,
                DeviceId = Guid.NewGuid(),
                SessionId = Guid.NewGuid(),
                DateCreatedOnDevice = dateCreatedOnDevice,
                Date = date,
                DateCreated = DateTime.Now,
                Version = version,
                PlatformId = platform
            };

            CrashSummary summary = new CrashSummary(crash);
            crashMapper.Save(summary);

            Crash crash2 = new Crash()
            {
                ApplicationId = applicationId,
                DeviceId = Guid.NewGuid(),
                SessionId = Guid.NewGuid(),
                DateCreatedOnDevice = dateCreatedOnDevice,
                Date = date,
                DateCreated = DateTime.Now,
                Version = version,
                PlatformId = platform
            };

            CrashSummary summary2 = new CrashSummary(crash2);
            crashMapper.Save(summary2);

            IMongoQuery query = Query.And
                (
                    Query<CrashSummary>.EQ<DateTime>(mem => mem.Date, date),
                    Query<CrashSummary>.EQ<Guid>(mem => mem.ApplicationId, applicationId),
                    Query<CrashSummary>.EQ<string>(mem => mem.Version, version),
                    Query<CrashSummary>.EQ<PlatformType>(mem => mem.PlatformId, platform)
                );

            CrashSummary actual = this.GetCollection<CrashSummary>().FindOne(query);

            actual.ShouldHave().AllPropertiesBut(x => x.Id).EqualTo(expected);
        }
    }
}
