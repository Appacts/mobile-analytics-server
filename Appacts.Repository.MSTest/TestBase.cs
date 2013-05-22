using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;

namespace AppActs.Repository.SqlServer.MSTest
{
    public abstract class TestBase
    {
        protected MongoClient client = new MongoClient(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        protected string database = ConfigurationManager.AppSettings["database"];

        protected MongoCollection<TEntity> GetCollection<TEntity>()
        {
            return this.GetDatabase().GetCollection<TEntity>(typeof(TEntity).Name);
        }

        protected MongoDatabase GetDatabase()
        {
            return this.client.GetServer().GetDatabase(this.database);
        }
    }
}
