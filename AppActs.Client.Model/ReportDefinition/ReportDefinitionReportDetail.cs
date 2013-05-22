using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AppActs.Client.Model
{
    public class ReportDefinitionReportDetail : ConfigurationElement
    {
        [ConfigurationProperty("Guid", IsRequired = true)]
        public Guid Guid { get { return (Guid)this["Guid"]; } set { this["Guid"] = value; } }

        [ConfigurationProperty("Template", IsRequired = true)]
        public ReportDefinitionNestedValue Template { get { return (ReportDefinitionNestedValue)this["Template"]; } set { this["Template"] = value; } }

        [ConfigurationProperty("Query", IsRequired = false)]
        public ReportDefinitionNestedValue Query { get { return (ReportDefinitionNestedValue)this["Query"]; } set { this["Query"] = value; } }
    }
}
