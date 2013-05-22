using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AppActs.Client.Model
{
    public class ReportDefinitionsSection : ConfigurationSection
    {
        [ConfigurationProperty("ReportDefinitions")]
        public ReportDefinitions Definitions 
        {
            get
            {
                return (ReportDefinitions)this["ReportDefinitions"];
            }
            set
            {
                this["ReportDefinitions"] = value;
            }
        }
    }
}

