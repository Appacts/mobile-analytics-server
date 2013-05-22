using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using AppActs.Core.Exceptions;

namespace AppActs.API.DataMapper
{
    public abstract class NoSqlBase
    {
        protected readonly MongoClient client;
        private readonly string databaseName;

        public NoSqlBase(MongoClient client, string databaseName)
        {
            this.client = client;
            this.databaseName = databaseName;
        }

        protected MongoDatabase GetDatabase()
        {
            return this.client.GetServer().GetDatabase(this.databaseName);
        }

        protected MongoCollection<TEntity> GetCollection<TEntity>()
        {
            return this.GetDatabase().GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual void Save<TEntity>(TEntity value)
        {
            try
            {
                this.GetCollection<TEntity>().Insert(value);
            }
            catch (Exception ex)
            {
                throw new DataAccessLayerException(ex);
            }
        }

    }
}
