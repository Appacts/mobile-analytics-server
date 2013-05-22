using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using AppActs.API.Model.Device;

namespace AppActs.API.Model.User
{
    public class AppUserSummary : Summary
    {
        public List<UserAggregate> Users { get; set; }

        public AppUserSummary()
        {

        }

        public AppUserSummary(AppUser appUser)
            : base(appUser)
        {
            this.Users = new List<UserAggregate>();
            this.Users.Add(new UserAggregate(appUser.Sex, appUser.Age));
        }
    }
}
