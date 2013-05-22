using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MongoDB.Driver;

namespace AppActs.Client.Repository.SqlServer.MSTest
{
    public class TestBase
    {
        protected string Connection
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            }
        }

        protected string Database
        {
            get
            {
                return ConfigurationManager.AppSettings["database"];
            }
        }

        private MongoClient client;

        protected MongoClient Client
        {
            get
            {
                if (this.client == null)
                {
                    this.client = new MongoClient(this.Connection);
                }
                return this.client;
            }
        }
    }
}
