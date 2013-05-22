using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Model.Enum;

namespace AppActs.Client.Model
{
    public class Settings : AppActs.Model.Settings
    {
        public string EmailFrom { get; private set; }
        public string EmailFromDisplayName { get; private set; }
        public string EmailTo { get; private set; }
        public string Url { get; private set; }
        public bool SecureConnectionSupported { get; private set; }
        public string SecurityKey { get; private set; }

        public Settings()
        {

        }

        public Settings(string emailFrom, string emailTo, string url, bool secureConnectionSupported, string securityKey,
            string emailFromDisplayName)
        {
            this.EmailFrom = emailFrom;
            this.EmailTo = emailTo;
            this.Url = url;
            this.SecureConnectionSupported = secureConnectionSupported;
            this.SecurityKey = securityKey;
            this.EmailFromDisplayName = emailFromDisplayName;
        }
    }
}
