using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.DataMapper.Interface;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using AppActs.Core.Exceptions;
using AppActs.API.Model.Feedback;
using AppActs.Model.Enum;

namespace AppActs.API.DataMapper
{
    public class FeedbackMapper : NoSqlBase, IFeedbackMapper
    {
        public FeedbackMapper(MongoClient client, string databaseName)
            : base(client, databaseName)
        {

        }

        public void Save(Feedback entity)
        {
            base.Save(entity);
        }

        public void Save(FeedbackSummary entity)
        {
            try
            {
                IMongoQuery queryBase = Query.And
                    (
                        Query<FeedbackSummary>.EQ<DateTime>(mem => mem.Date, entity.Date),
                        Query<FeedbackSummary>.EQ<Guid>(mem => mem.ApplicationId, entity.ApplicationId),
                        Query<FeedbackSummary>.EQ<string>(mem => mem.Version, entity.Version),
                        Query<FeedbackSummary>.EQ<PlatformType>(mem => mem.PlatformId, entity.PlatformId)
                    );

                IMongoUpdate update = Update<FeedbackSummary>
                    .SetOnInsert(x => x.Version, entity.Version)
                    .SetOnInsert(x => x.Date, entity.Date)
                    .SetOnInsert(x => x.ApplicationId, entity.ApplicationId)
                    .SetOnInsert(x => x.PlatformId, entity.PlatformId)
                    .SetOnInsert(x => x.Ratings, new List<RatingAggregate>())
                    .Inc(mem => mem.Count, entity.Count)
                    .Inc(mem => mem.SumOfRatings, entity.SumOfRatings);

                this.GetCollection<FeedbackSummary>()
                    .FindAndModify(queryBase, SortBy.Descending("Date"), update, false, true);
                this.GetCollection<FeedbackSummary>().EnsureIndex(IndexKeys.Descending("Date"));

                IMongoQuery queryFeedbackInsert = Query.And
                    (
                        queryBase,
                        Query.NE("Ratings.Key", BsonValue.Create(entity.Ratings.First().Key))
                    );

                IMongoUpdate updateFeedback = Update
                    .Push("Ratings", BsonValue.Create(entity.Ratings.First().CopyOnlyKey().ToBsonDocument()));

                this.GetCollection<FeedbackSummary>().Update(queryFeedbackInsert, updateFeedback);

                IMongoQuery queryFeedbackUpdate = Query.And
                    (
                        queryBase,
                        Query.EQ("Ratings.Key", BsonValue.Create(entity.Ratings.First().Key))
                    );

                IMongoUpdate updateInc = Update
                    .Inc("Ratings.$.Rating", entity.Ratings.First().Rating)
                    .Inc("Ratings.$.Count", 1);

                this.GetCollection<FeedbackSummary>().Update(queryFeedbackUpdate, updateInc);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }
    }
}
