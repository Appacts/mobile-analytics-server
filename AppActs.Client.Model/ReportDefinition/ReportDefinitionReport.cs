using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using AppActs.Client.Model.Enum;

namespace AppActs.Client.Model
{
    public class ReportDefinitionReport : ConfigurationElement
    {
        [ConfigurationProperty("Query", IsRequired = false)]
        public ReportDefinitionNestedValue Query { get { return (ReportDefinitionNestedValue)this["Query"]; } set { this["Query"] = value; } }

        [ConfigurationProperty("Guid", IsRequired = true)]
        public Guid Guid { get { return (Guid)this["Guid"]; } set { this["Guid"] = value; } }

        [ConfigurationProperty("ChartType", IsRequired = true)]
        public ChartType ChartType { get { return (ChartType)this["ChartType"]; } set { this["ChartType"] = value; } }

        public ReportDefinition Parent { get; set; }
    }
}
