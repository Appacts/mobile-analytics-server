using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.DataMapper.Interface;
using MongoDB.Driver;
using AppActs.API.Model.SystemError;

namespace AppActs.API.DataMapper
{
    public class SystemErrorMapper : NoSqlBase<SystemError>, ISystemErrorMapper
    {
        public SystemErrorMapper(MongoClient client, string databaseName)
            : base(client, databaseName)
        {

        }
    }
}
