using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Model;
using System.Data;
using AppActs.Core.Exceptions;
using AppActs.Client.Model;
using System.Data.SqlClient;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using AppActs.Repository;
using AppActs.Client.Repository.Interface;
using AppActs.Model.Enum;

namespace AppActs.Client.Repository
{
    public class PlatformRepository : NoSqlBase<Platform>, IPlatformRepository
    {
        public PlatformRepository(MongoClient client, string databaseName)
            : base(client, databaseName)
        {

        }

        public IEnumerable<Platform> FindAll()
        {
            try
            {

                return this.GetCollection().FindAll()
                    .SetFields(Fields.Exclude("_id"));
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public Platform Find(PlatformType id)
        {
            try
            {
                return this.GetCollection()
                    .Find(Query<Platform>.EQ<PlatformType>(x => x.Type, id))
                    .SetFields(Fields.Exclude("_id")).First(); 
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public void Save(IEnumerable<Platform> platforms)
        {
            try
            {
                this.GetCollection().InsertBatch(platforms);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }
    }
}
