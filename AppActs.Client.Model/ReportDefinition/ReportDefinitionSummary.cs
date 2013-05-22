using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using AppActs.Client.Model.Enum;

namespace AppActs.Client.Model
{
    public class ReportDefinitionSummary : ConfigurationElement
    {
        [ConfigurationProperty("Guid", IsRequired = true)]
        public Guid Guid { get { return (Guid)this["Guid"]; } set { this["Guid"] = value; } }

        [ConfigurationProperty("Order", IsRequired = false)]
        public int Order { get { return (int)this["Order"]; } set { this["Order"] = value; } }

        [ConfigurationProperty("Template", IsRequired = true)]
        public ReportDefinitionNestedValue Template { get { return (ReportDefinitionNestedValue)this["Template"]; } set { this["Template"] = value; } }

        [ConfigurationProperty("Query", IsRequired = true)]
        public ReportDefinitionNestedValue Query { get { return (ReportDefinitionNestedValue)this["Query"]; } set { this["Query"] = value; } }

        [ConfigurationProperty("Type", IsRequired = true)]
        public TileType Type { get { return (TileType)this["Type"]; } set { this["Type"] = value; } }
    }
}
