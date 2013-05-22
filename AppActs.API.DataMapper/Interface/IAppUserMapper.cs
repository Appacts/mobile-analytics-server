using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Model.User;

namespace AppActs.API.DataMapper.Interface
{
    public interface IAppUserMapper : ISave<AppUser, AppUserSummary>
    {

    }
}
