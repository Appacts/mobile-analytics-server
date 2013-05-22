using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Repository.Interface;
using AppActs.Client.Model;
using System.Data.SqlClient;
using AppActs.Core.Exceptions;
using AppActs.Client.Model.Enum;
using MongoDB.Driver;
using MongoDB.Bson;
using AppActs.Repository;
using MongoDB.Bson.Serialization;
using AppActs.Client.Repository.Dictionary;

namespace AppActs.Client.Repository
{
    public class TileRepository : NoSqlBase, ITileRepository
    {
        public TileRepository(MongoClient client, string databaseName)
            : base(client, databaseName)
        {
            
        }

        public Tile Get(TileType tileType, string query, Guid applicationId, DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                Type type = TileTypeMapping.Get(tileType);

                BsonValue value = this.GetDatabase().Eval(EvalFlags.NoLock, new BsonJavaScript(query),
                    applicationId, dateStart, dateEnd);

                return (Tile)BsonSerializer.Deserialize(value.ToJson(), type);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

        public Tile Get(TileType tileType, string query, Guid applicationId, DateTime dateStart, DateTime dateEnd, DateTime dateStartCompare, DateTime dateEndCompare)
        {
            try
            {
                Type type = TileTypeMapping.Get(tileType);

                BsonValue value = this.GetDatabase().Eval(EvalFlags.NoLock, new BsonJavaScript(query),
                            applicationId, dateStart, dateEnd, dateStartCompare, dateEndCompare);

                return (Tile)BsonSerializer.Deserialize(value.ToJson(), type);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }
    }
}
