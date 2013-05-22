using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using AppActs.Model.Enum;

namespace AppActs.Model
{
    public class Platform
    {
        public string Name { get; set; }

        [BsonIgnore]
        public int Id
        {
            get
            {
                return (int)this.Type;
            }
        }

        public PlatformType Type { get; set; }

        public Platform()
        {

        }
    }
}
