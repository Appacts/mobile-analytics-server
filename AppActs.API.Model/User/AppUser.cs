using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Specialized;

namespace AppActs.API.Model.User
{
    public class AppUser : Item
    {
        [BsonElement("ag")]
        public Nullable<Int32> Age { get; set; }

        [BsonElement("sx")]
        public Nullable<SexType> Sex { get; set; }

        public AppUser()
            : base()
        {

        }

        public AppUser(NameValueCollection keyValues)
            : base(keyValues)
        {
            if (!String.IsNullOrEmpty(keyValues[Keys.AGE]))
            {
                this.Age = int.Parse(keyValues[Keys.AGE]);
            }

            if (!String.IsNullOrEmpty(keyValues[Keys.SEX_TYPE]))
            {
                this.Sex = (SexType)int.Parse(keyValues[Keys.SEX_TYPE]);
            }
        }
    }
}
