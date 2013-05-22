using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.API.Repository.Interface;
using AppActs.Model;
using AppActs.Core.Exceptions;
using AppActs.API.Repository;
using AppActs.API.Model;
using AppActs.API.DataMapper;
using AppActs.API.Model.Feedback;
using MongoDB.Bson;
using AppActs.Model.Enum;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using FluentAssertions;

namespace AppActs.API.Test.Integration
{
    [TestClass]
    public class FeedbackMapperTest : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void Save_RawRecord_Saves()
        {
            FeedbackMapper feedbackMapper = new FeedbackMapper(this.client, this.database);
            Guid applicationId = Guid.NewGuid();
            Guid deviceId = Guid.NewGuid();

            Feedback feedback = new Feedback()
                {
                    ApplicationId = applicationId,
                    DeviceId = deviceId,
                    Message = "feedback",
                    Rating =  FeedbackRatingType.Five,
                    ScreenName =  "screenName",
                    SessionId = Guid.NewGuid(),
                    DateCreatedOnDevice =  DateTime.Now,
                    Version = "1.1"
                };

            feedbackMapper.Save(feedback);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Save_SummaryRecord_MatchesSummary()
        {
            FeedbackMapper feedbackMapper = new FeedbackMapper(this.client, this.database);
            Guid applicationId = Guid.NewGuid();
            string screenName = "screenName";

            FeedbackSummary expected = new FeedbackSummary()
            {
                ApplicationId = applicationId,
                Count = 2,
                Date = date,
                PlatformId = platform,
                Ratings = new List<RatingAggregate>()
                {
                     new RatingAggregate(screenName, 8)
                     {
                         Count = 2
                     }
                },
                Version = version,
                SumOfRatings = 8
            };

            Feedback feedback = new Feedback()
                {
                    ApplicationId = applicationId,
                    DeviceId = Guid.NewGuid(),
                    Message = "feedback",
                    Rating =  FeedbackRatingType.Five,
                    ScreenName =  screenName,
                    SessionId = Guid.NewGuid(),
                    DateCreatedOnDevice =  dateCreatedOnDevice,
                    Date = date,
                    DateCreated = DateTime.Now,
                    Version = version,
                    PlatformId = platform
                };

            FeedbackSummary summary = new FeedbackSummary(feedback);
            feedbackMapper.Save(summary);

            Feedback feedback2 = new Feedback()
            {
                ApplicationId = applicationId,
                DeviceId = Guid.NewGuid(),
                Message = "feedback",
                Rating =  FeedbackRatingType.Three,
                ScreenName =  screenName,
                SessionId = Guid.NewGuid(),
                DateCreatedOnDevice =  dateCreatedOnDevice,
                Date = date,
                DateCreated = DateTime.Now,
                Version = version,
                PlatformId = platform
            };

            FeedbackSummary summary2 = new FeedbackSummary(feedback2);
            feedbackMapper.Save(summary2);

            IMongoQuery query = Query.And
                (
                    Query<FeedbackSummary>.EQ<DateTime>(mem => mem.Date, date),
                    Query<FeedbackSummary>.EQ<Guid>(mem => mem.ApplicationId, applicationId),
                    Query<FeedbackSummary>.EQ<string>(mem => mem.Version, version),
                    Query<FeedbackSummary>.EQ<PlatformType>(mem => mem.PlatformId, platform)
                );

            FeedbackSummary actual = this.GetCollection<FeedbackSummary>().FindOne(query);

            actual.ShouldHave().AllPropertiesBut(x => x.Id)
                .IncludingNestedObjects().EqualTo(expected);
        }
    }
}
