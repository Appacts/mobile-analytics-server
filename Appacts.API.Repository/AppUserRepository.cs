using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Repository.Interface;
using AppActs.Core.Exceptions;
using System.Data.SqlClient;
using System.Data;
using MongoDB.Driver;
using AppActs.API.DataMapper.Interface;
using AppActs.API.Model.User;

namespace AppActs.API.Repository
{
    public class AppUserRepository : IAppUserRepository
    {
        readonly IAppUserMapper appUserMapper;

        public AppUserRepository(IAppUserMapper appUserMapper)
        {
            this.appUserMapper = appUserMapper;
        }

        public void Save(AppUser user)
        {
            this.appUserMapper.Save(user);
        }

        public void Save(AppUserSummary summary)
        {
            this.appUserMapper.Save(summary);
        }
    }
}
