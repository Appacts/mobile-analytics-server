using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Repository.Interface;
using AppActs.Core.Exceptions;
using System.Data.SqlClient;
using MongoDB.Driver;
using AppActs.Repository;
using AppActs.API.DataMapper.Interface;
using AppActs.API.Model.SystemError;

namespace AppActs.API.Repository
{
    public class SystemErrorRepository : ISystemErrorRepository
    {
        readonly ISystemErrorMapper systemErrorMapper;

        public SystemErrorRepository(ISystemErrorMapper systemErrorMapper)
        {
            this.systemErrorMapper = systemErrorMapper;
        }

        public void Save(SystemError systemError)
        {
            this.systemErrorMapper.Save(systemError);
        }
    }
}
