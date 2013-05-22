using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using AppActs.API.DataMapper.Interface;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using AppActs.Core.Exceptions;
using AppActs.API.Model.Crash;
using AppActs.Model.Enum;

namespace AppActs.API.DataMapper
{
    public class CrashMapper : NoSqlBase, ICrashMapper
    {
        public CrashMapper(MongoClient client, string databaseName)
            : base(client, databaseName)
        {

        }

        public void Save(Crash entity)
        {
            base.Save(entity);
        }

        public void Save(CrashSummary entity)
        {
            try
            {
                IMongoQuery query = Query.And
                    (
                        Query<CrashSummary>.EQ<DateTime>(mem => mem.Date, entity.Date),
                        Query<CrashSummary>.EQ<Guid>(mem => mem.ApplicationId, entity.ApplicationId),
                        Query<CrashSummary>.EQ<string>(mem => mem.Version, entity.Version),
                        Query<CrashSummary>.EQ<PlatformType>(mem => mem.PlatformId, entity.PlatformId)
                    );

                IMongoUpdate update = Update<CrashSummary>
                    .SetOnInsert(x => x.Version, entity.Version)
                    .SetOnInsert(x => x.Date, entity.Date)
                    .SetOnInsert(x => x.ApplicationId, entity.ApplicationId)
                    .SetOnInsert(x => x.PlatformId, entity.PlatformId)
                    .Inc(mem => mem.Count, entity.Count);

                this.GetCollection<CrashSummary>().FindAndModify(query, SortBy.Descending("Date"), update, false, true);
                this.GetCollection<CrashSummary>().EnsureIndex(IndexKeys.Descending("Date"));
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }
    }
}
