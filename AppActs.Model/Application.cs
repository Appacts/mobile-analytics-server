using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.ObjectModel;

namespace AppActs.Model
{
    public class Application : ApplicationMeta
    {
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public List<Platform> Platforms { get; set; }

        public Application()
            : base()
        {

        }

        public Application(string applicationName)
            : base(applicationName)
        {
            this.Active = true;
            this.DateCreated = DateTime.Now;
            this.DateModified = DateTime.Now;
        }
    }
}
