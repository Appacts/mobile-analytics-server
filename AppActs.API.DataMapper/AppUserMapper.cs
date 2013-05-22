using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using AppActs.API.DataMapper.Interface;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using AppActs.Core.Exceptions;
using AppActs.API.Model.User;
using AppActs.Model.Enum;

namespace AppActs.API.DataMapper
{
    public class AppUserMapper : NoSqlBase, IAppUserMapper
    {
        public AppUserMapper(MongoClient client, string databaseName)
            : base(client, databaseName)
        {

        }

        public void Save(AppUser entity)
        {
            base.Save(entity);
        }

        public void Save(AppUserSummary entity)
        {
            try
            {
                IMongoQuery queryBase = Query.And
                    (
                        Query<AppUserSummary>.EQ<DateTime>(mem => mem.Date, entity.Date),
                        Query<AppUserSummary>.EQ<Guid>(mem => mem.ApplicationId, entity.ApplicationId),
                        Query<AppUserSummary>.EQ<string>(mem => mem.Version, entity.Version),
                        Query<AppUserSummary>.EQ<PlatformType>(mem => mem.PlatformId, entity.PlatformId)
                    );

                IMongoUpdate update = Update<AppUserSummary>
                    .SetOnInsert(x => x.Version, entity.Version)
                    .SetOnInsert(x => x.Date, entity.Date)
                    .SetOnInsert(x => x.ApplicationId, entity.ApplicationId)
                    .SetOnInsert(x => x.PlatformId, entity.PlatformId)
                    .SetOnInsert(x => x.Users, new List<UserAggregate>())
                    .Inc(mem => mem.Count, entity.Count);


                this.GetCollection<AppUserSummary>().FindAndModify(queryBase, SortBy.Null, update, false, true);


                IMongoQuery queryCheckNotExist = Query.And
                    (
                        queryBase,
                        Query.NE("Users.Key", BsonValue.Create(entity.Users.First().Key))
                    );

                IMongoUpdate insertUser = Update
                    .Push("Users", entity.Users.First().CopyOnlyKeys().ToBsonDocument());

                this.GetCollection<AppUserSummary>().Update(queryCheckNotExist, insertUser);


                IMongoQuery queryUser = Query.And
                    (
                        queryBase,
                        Query.EQ("Users.Key", BsonValue.Create(entity.Users.First().Key))
                    );

                IMongoUpdate updateUser = Update
                    .Inc("Users.$.AgeGroup._1_18", entity.Users.First().AgeGroup._1_18)
                    .Inc("Users.$.AgeGroup._19_24", entity.Users.First().AgeGroup._19_24)
                    .Inc("Users.$.AgeGroup._25_35", entity.Users.First().AgeGroup._25_35)
                    .Inc("Users.$.AgeGroup._36_50", entity.Users.First().AgeGroup._36_50)
                    .Inc("Users.$.AgeGroup._51_69", entity.Users.First().AgeGroup._51_69)
                    .Inc("Users.$.AgeGroup._71", entity.Users.First().AgeGroup._71)
                    .Inc("Users.$.Count", 1);

                this.GetCollection<AppUserSummary>().Update(queryUser, updateUser);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }
    }
}
