using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.API.DataMapper;
using MongoDB.Bson;
using AppActs.API.Model.User;
using MongoDB.Driver;
using AppActs.Model.Enum;
using MongoDB.Driver.Builders;
using FluentAssertions;

namespace AppActs.API.Test.Integration
{
    [TestClass]
    public class AppUserMapperTest : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save_AppUser_ValuesIncrement()
        {
            AppUserMapper appuserMapper = new AppUserMapper(this.client, this.database);
            Guid applicationId = Guid.NewGuid();

            AppUserSummary expected = new AppUserSummary()
            {
                ApplicationId = applicationId,
                Count = 2,
                Date = date,
                PlatformId = platform,
                Version = version,
                Users = new List<UserAggregate>()
                {
                    new UserAggregate()
                    {
                         Key = SexType.Male,
                         AgeGroup = new AgeGroup(null)
                         {
                             _25_35 = 2
                         },
                         Count = 2
                    }
                }
            };

            AppUserSummary summary = new AppUserSummary()
            {
                ApplicationId = applicationId,
                Count = 1,
                Date = date,
                PlatformId = platform,
                Users = new List<UserAggregate>()
                {
                    new UserAggregate(SexType.Male, 30)
                },
                Version = version
            };

            appuserMapper.Save(summary);
            appuserMapper.Save(summary);

            IMongoQuery query = Query.And
                (
                    Query<AppUserSummary>.EQ<DateTime>(mem => mem.Date, date),
                    Query<AppUserSummary>.EQ<Guid>(mem => mem.ApplicationId, applicationId),
                    Query<AppUserSummary>.EQ<string>(mem => mem.Version, version),
                    Query<AppUserSummary>.EQ<PlatformType>(mem => mem.PlatformId, platform)
                );

            AppUserSummary actual = this.GetCollection<AppUserSummary>().FindOne(query);

            actual.ShouldHave().AllPropertiesBut(x => x.Id)
                .IncludingNestedObjects().EqualTo(expected);
        }
    }
}
