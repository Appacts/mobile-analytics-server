using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Repository.Interface;
using AppActs.Model;
using System.Data;
using AppActs.Core.Exceptions;
using System.Data.SqlClient;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace AppActs.Repository
{
    public class ApplicationRepository : NoSqlBase<Application>, IApplicationRepository
    {
        public ApplicationRepository(MongoClient client, string databaseName)
            : base(client, databaseName)
        {

        }

        public Application Find(Guid id)
        {
            try
            {
                return this.GetCollection()
                    .Find(Query<Application>.EQ<Guid>(app => app.Guid, id))
                    .SetFields(Fields.Exclude("_id")).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public IEnumerable<string> GetVersionsByApplication(Guid id)
        {
            try
            {
                IEnumerable<BsonValue> values =
                    this.GetCollection("AppUsageSummary").Distinct("Version", Query.EQ("ApplicationId", id));

                List<string> versions = new List<string>();

                foreach (BsonValue value in values)
                {
                    versions.Add(value.ToString());
                }

                return versions;
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }
    }
}
