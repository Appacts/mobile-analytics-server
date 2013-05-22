using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Model;
using System.Configuration;
using AppActs.Client.Repository.Interface;
using AppActs.Client.Repository;
using AppActs.API.Repository.Interface;
using AppActs.API.Repository;
using MongoDB.Driver;
using AppActs.API.Model;
using AppActs.API.Model.Device;
using AppActs.API.DataMapper;
using AppActs.Model.Enum;

namespace AppActs.API.Test.Integration
{
    public class TestBase
    {
        protected DateTime date = new DateTime(2013, 05, 01, 0, 0, 0, DateTimeKind.Utc);
        protected DateTime dateCreatedOnDevice = new DateTime(2013, 05, 01, 10, 0, 0, DateTimeKind.Utc);
        protected PlatformType platform = PlatformType.iOS;
        protected string version = "1.1";

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
