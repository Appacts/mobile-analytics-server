using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Repository.Interface;
using AppActs.Core.Exceptions;
using System.Data.SqlClient;
using AppActs.API.Model;
using MongoDB.Driver;
using AppActs.Repository;
using AppActs.API.Model.Crash;
using AppActs.API.DataMapper.Interface;

namespace AppActs.API.Repository
{
    public class CrashRepository : ICrashRepository
    {
        readonly ICrashMapper crashMapper;

        public CrashRepository(ICrashMapper crashMapper)
        {
            this.crashMapper = crashMapper;
        }

        public void Save(Crash crash)
        {
            this.crashMapper.Save(crash);
        }

        public void Save(CrashSummary crash)
        {
            this.crashMapper.Save(crash);
        }
    }
}
