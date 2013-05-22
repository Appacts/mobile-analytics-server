using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using AppActs.Core.Exceptions;

namespace AppActs.Repository
{
    public abstract class NoSqlBase<TEntity> : NoSqlBase
    {
        public NoSqlBase(MongoClient client, string databaseName)
            : base(client, databaseName)
        {

        }

        protected MongoCollection<TEntity> GetCollection()
        {
            return this.GetDatabase().GetCollection<TEntity>(typeof(TEntity).Name);
        }

        protected MongoCollection GetCollection(string collectionName)
        {
            return this.GetDatabase().GetCollection(collectionName);
        }

        public virtual void Save(TEntity value)
        {
            try
            {
                this.GetCollection().Insert(value);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }


    }
}
