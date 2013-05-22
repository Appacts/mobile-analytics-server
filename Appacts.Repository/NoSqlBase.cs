using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace AppActs.Repository
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



    }
}
