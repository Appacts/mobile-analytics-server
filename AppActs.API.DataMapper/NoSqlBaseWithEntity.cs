using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using AppActs.Core.Exceptions;

namespace AppActs.API.DataMapper
{
    public abstract class NoSqlBase<TEntity> : NoSqlBase
    {
        public NoSqlBase(MongoClient client, string databaseName)
            : base(client, databaseName)
        {

        }

        protected MongoCollection<TEntity> GetCollection()
        {
            return this.GetCollection<TEntity>();
        }

        public virtual void Save(TEntity value)
        {
            this.Save<TEntity>(value);
        }
    }
}
