using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace AppActs.Model
{
    public class ApplicationMeta 
    {
        [BsonIgnore]
        public Guid Id
        {
            get
            {
                return this.Guid;
            }
        }

        public string Name { get; set; }
        public Guid Guid { get; set; }

        public ApplicationMeta()
        {

        }

        public ApplicationMeta(string name)
        {
            this.Guid = Guid.NewGuid();
            this.Name = name;
        }
    }
}
