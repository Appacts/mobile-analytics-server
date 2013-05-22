using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using System.Collections.Specialized;

namespace AppActs.API.Model
{
    public class ApplicationInfo
    {
        public Guid ApplicationId { get; set; }
        public string Version { get; set; }

        public ApplicationInfo()
        {

        }

        public ApplicationInfo(NameValueCollection keyValues)
        {
            this.ApplicationId = new Guid(keyValues[Keys.APPLICATION_GUID]);
            this.Version = keyValues.Get(Keys.VERSION);
        }
    }
}
