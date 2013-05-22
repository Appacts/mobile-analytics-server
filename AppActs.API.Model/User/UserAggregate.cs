using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model.User
{
    public class UserAggregate : Aggregate<Nullable<SexType>>
    {
        public AgeGroup AgeGroup { get; set; }

        public UserAggregate()
        {

        }

        public UserAggregate(Nullable<SexType> sexType, Nullable<int> age)
            : base(sexType)
        {
            this.AgeGroup = new AgeGroup(age);
        }

        public new UserAggregate CopyOnlyKeys()
        {
            return new UserAggregate(this.Key, null);
        }
    }
}
