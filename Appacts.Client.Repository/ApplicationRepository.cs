using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Model;
using System.Data;
using System.Data.SqlClient;
using AppActs.Core.Exceptions;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace AppActs.Client.Repository
{
    public class ApplicationRepository : AppActs.Repository.ApplicationRepository, AppActs.Client.Repository.Interface.IApplicationRepository
    {
        public ApplicationRepository(MongoClient client, string databaseName)
            : base(client, databaseName)
        {
            
        }

        public Application Find(string applicationName)
        {
            try
            {
                var query = Query.And
                    (
                        Query<Application>.EQ<bool>(x => x.Active, true),
                        Query<Application>.EQ<string>(x => x.Name, applicationName)
                    );

                return this.GetCollection().Find(query)
                    .SetFields(Fields.Exclude("_id")).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public void Update(Application application)
        {
            try
            {
                var query = Query<Application>.EQ<Guid>(x => x.Guid, application.Guid);
                var update = Update<Application>
                    .Set(x => x.Active, application.Active)
                    .Set(x => x.DateModified, application.DateModified)
                    .Set(x => x.Name, application.Name)
                    .Set(x => x.Platforms, application.Platforms);

                this.GetCollection().Update(query, update);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public IEnumerable<Application> FindAll()
        {
            try
            {
                return this.GetCollection()
                    .Find(Query<Application>.EQ<bool>(x => x.Active, true))
                    .SetFields(Fields.Exclude("_id"));
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }
    }
}
