using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.API.DataMapper;
using MongoDB.Bson;
using AppActs.API.Model.Error;
using MongoDB.Driver;
using AppActs.Model.Enum;
using FluentAssertions;
using AppActs.API.Model;
using MongoDB.Driver.Builders;


namespace AppActs.API.Test.Integration
{
    [TestClass]
    public class ErrorMapperTest : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save_Error_ValuesIncrement()
        {
            ErrorMapper errorMapper = new ErrorMapper(this.client, this.database);
            Guid applicationId = Guid.NewGuid();
            Guid deviceId = Guid.NewGuid();

            ErrorSummary expected = new ErrorSummary()
            {
                ApplicationId = applicationId,
                Count = 2,
                Date = date,
                PlatformId = platform,
                Version = version,
                ScreenErrors = new List<Aggregate<string>>()
                {
                     new Aggregate<string>()
                     {
                          Key = "someScreen",
                          Count = 2
                     }
                }
            };

            ErrorSummary summary = new ErrorSummary()
            {
                ApplicationId = applicationId,
                Count = 1,
                Date = date,
                PlatformId = platform,
                Version = version,
                ScreenErrors = new List<Aggregate<string>>()
                {
                     new Aggregate<string>("someScreen")
                }
            };

            errorMapper.Save(summary);
            errorMapper.Save(summary);

            IMongoQuery query = Query.And
                (
                    Query<ErrorSummary>.EQ<DateTime>(mem => mem.Date, date),
                    Query<ErrorSummary>.EQ<Guid>(mem => mem.ApplicationId, applicationId),
                    Query<ErrorSummary>.EQ<string>(mem => mem.Version, version),
                    Query<ErrorSummary>.EQ<PlatformType>(mem => mem.PlatformId, platform)
                );

            ErrorSummary actual = this.GetCollection<ErrorSummary>().FindOne(query);

            actual.ShouldHave().AllPropertiesBut(x => x.Id)
                .IncludingNestedObjects().EqualTo(expected);
        }
    }
}
