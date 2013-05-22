using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Repository.Interface;
using AppActs.Client.Model;
using AppActs.Client.Model.Enum;
using System.Data;
using AppActs.Core.Exceptions;
using System.Data.SqlClient;
using AppActs.Model.Enum;
using AppActs.Core.Xml;
using MongoDB.Driver;
using MongoDB.Bson;
using AppActs.Repository;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.IO;
using System.Web.Script.Serialization;


namespace AppActs.Client.Repository
{
    public class DataRepository : NoSqlBase<GraphSeries>, IDataRepository
    {
        public DataRepository(MongoClient client, string databaseName)
            : base(client, databaseName)
        {

        }

        public List<GraphSeries> GetGraphAxis(string query, Guid applicationId, 
            DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                BsonValue value = this.GetDatabase().Eval(EvalFlags.NoLock, new BsonJavaScript(query),
                    applicationId, dateStart, dateEnd);

                return BsonSerializer.Deserialize<List<GraphSeries>>(value.ToJson());
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public List<GraphSeries> GetGraphAxis(string query, Guid applicationId, 
            IEnumerable<PlatformType> platformTypes, DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                BsonValue value = this.GetDatabase().Eval(EvalFlags.NoLock, new BsonJavaScript(query),
                    applicationId, dateStart, dateEnd, platformTypes);

                return BsonSerializer.Deserialize<List<GraphSeries>>(value.ToJson());
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public List<GraphSeries> GetGraphAxis(string query, IEnumerable<Guid> applicationIds, 
            DateTime dateStart, DateTime dateEnd)
        {
            BsonValue value = this.GetDatabase().Eval(EvalFlags.NoLock, new BsonJavaScript(query),
                   applicationIds, dateStart, dateEnd);

            return BsonSerializer.Deserialize<List<GraphSeries>>(value.ToJson());
        }

        public List<GraphSeries> GetGraphAxis(string query, Guid applicationId,
            IEnumerable<string> versions, DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                BsonValue value = this.GetDatabase().Eval(EvalFlags.NoLock, new BsonJavaScript(query),
                    applicationId, dateStart, dateEnd, versions);

                return BsonSerializer.Deserialize<List<GraphSeries>>(value.ToJson());
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public object GetDetail(string query, Guid applicationId, 
            DateTime dateStart, DateTime dateEnd, string detailId)
        {
            try
            {
                BsonValue value = this.GetDatabase().Eval(EvalFlags.NoLock, new BsonJavaScript(query),
                    applicationId, dateStart, dateEnd, detailId);

                JsonWriterSettings settings = JsonWriterSettings.Defaults;
                settings.OutputMode = JsonOutputMode.Strict;

                return new JavaScriptSerializer().DeserializeObject(value.ToJson(settings));
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

    }
}
