using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.DataMapper.Interface;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using AppActs.Core.Exceptions;
using AppActs.API.Model.Error;
using AppActs.Model.Enum;
using AppActs.API.Model;

namespace AppActs.API.DataMapper
{
    public class ErrorMapper : NoSqlBase, IErrorMapper
    {
        public ErrorMapper(MongoClient client, string databaseName)
            : base(client, databaseName)
        {

        }

        public void Save(Error entity)
        {
            base.Save(entity);
        }

        public void Save(ErrorSummary entity)
        {
            try
            {
                IMongoQuery queryBase = Query.And
                    (
                        Query<ErrorSummary>.EQ<DateTime>(mem => mem.Date, entity.Date),
                        Query<ErrorSummary>.EQ<Guid>(mem => mem.ApplicationId, entity.ApplicationId),
                        Query<ErrorSummary>.EQ<string>(mem => mem.Version, entity.Version),
                        Query<ErrorSummary>.EQ<PlatformType>(mem => mem.PlatformId, entity.PlatformId)
                    );

                IMongoUpdate update = Update<ErrorSummary>
                    .SetOnInsert(x => x.Version, entity.Version)
                    .SetOnInsert(x => x.Date, entity.Date)
                    .SetOnInsert(x => x.ApplicationId, entity.ApplicationId)
                    .SetOnInsert(x => x.PlatformId, entity.PlatformId)
                    .SetOnInsert(x => x.ScreenErrors, new List<Aggregate<string>>())
                    .Inc(mem => mem.Count, entity.Count);

                this.GetCollection<ErrorSummary>().FindAndModify(queryBase, SortBy.Descending("Date"), update, false, true);
                this.GetCollection<ErrorSummary>().EnsureIndex(IndexKeys.Descending("Date"));

                IMongoQuery queryNotExists = Query.And
                    (
                        queryBase,
                        Query.NE("ScreenErrors.Key", BsonValue.Create(entity.ScreenErrors.First().Key))
                    );

                IMongoUpdate insertScreenError = Update
                    .Push("ScreenErrors", entity.ScreenErrors.First().CopyOnlyKeys().ToBsonDocument());

                this.GetCollection<ErrorSummary>().Update(queryNotExists, insertScreenError);

                IMongoQuery queryErrors = Query.And
                    (
                        queryBase,
                        Query.EQ("ScreenErrors.Key", BsonValue.Create(entity.ScreenErrors.First().Key))
                    );

                IMongoUpdate updateErrors = Update
                    .Inc("ScreenErrors.$.Count", 1);

                this.GetCollection<ErrorSummary>().Update(queryErrors, updateErrors);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }
    }
}
