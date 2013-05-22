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
using AppActs.API.DataMapper.Interface;
using AppActs.API.Model.Error;

namespace AppActs.API.Repository
{
    public class ErrorRepository : IErrorRepository
    {
        readonly IErrorMapper errorMapper;

        public ErrorRepository(IErrorMapper errorMapper)
        {
            this.errorMapper = errorMapper;
        }

        public void Save(Error entity)
        {
            this.errorMapper.Save(entity);
        }

        public void Save(ErrorSummary entity)
        {
            this.errorMapper.Save(entity);
        }
    }
}
