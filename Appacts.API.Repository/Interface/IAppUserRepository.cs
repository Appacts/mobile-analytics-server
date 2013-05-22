using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Model.User;

namespace AppActs.API.Repository.Interface
{
    public interface IAppUserRepository 
    {
        void Save(AppUser user);
        void Save(AppUserSummary summary);
    }
}
