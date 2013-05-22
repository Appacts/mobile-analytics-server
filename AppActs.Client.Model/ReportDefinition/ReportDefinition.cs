using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ComponentModel;

namespace AppActs.Client.Model
{
    public class ReportDefinition : ConfigurationElement
    {
        [ConfigurationProperty("Name", IsRequired = true)]
        public string Name { get { return (string)this["Name"]; } set { this["Name"] = value; } }

        [ConfigurationProperty("X", IsRequired = true)]
        public string X { get { return (string)this["X"]; } set { this["X"] = value; } }

        [TypeConverter(typeof(TypeNameConverter))]
        [ConfigurationProperty("XType", IsRequired = false, DefaultValue = typeof(System.String))]
        public Type XType { get { return (Type)this["XType"]; } set { this["XType"] = value; } }

        [ConfigurationProperty("Y", IsRequired = true)]
        public string Y { get { return (string)this["Y"]; } set { this["Y"] = value; } }

        [TypeConverter(typeof(TypeNameConverter))]
        [ConfigurationProperty("YType", IsRequired = false, DefaultValue = typeof(System.Int32))]
        public Type YType { get { return (Type)this["YType"]; } set { this["YType"] = value; } }

        [ConfigurationProperty("YY", IsRequired = false)]
        public string YY { get { return (string)this["YY"]; } set { this["YY"] = value; } }

        [TypeConverter(typeof(TypeNameConverter))]
        [ConfigurationProperty("YYType", IsRequired = false, DefaultValue = typeof(System.Int32))]
        public Type YYType { get { return (Type)this["YYType"]; } set { this["YYType"] = value; } }

        [ConfigurationProperty("Summary", IsRequired = true)]
        public ReportDefinitionSummary Summary { get { return (ReportDefinitionSummary)this["Summary"]; } set { this["Summary"] = value; } }

        [ConfigurationProperty("ReportNormal", IsRequired = false, DefaultValue = null)]
        public ReportDefinitionReportNormal ReportNormal { get { return (ReportDefinitionReportNormal)this["ReportNormal"]; } set { this["ReportNormal"] = value; } }

        [ConfigurationProperty("ReportComparePlatform", IsRequired = false, DefaultValue = null)]
        public ReportDefinitionReportComparePlatform ReportComparePlatform { get { return (ReportDefinitionReportComparePlatform)this["ReportComparePlatform"]; } set { this["ReportComparePlatform"] = value; } }

        [ConfigurationProperty("ReportCompareApplication", IsRequired = false, DefaultValue = null)]
        public ReportDefinitionReportReportCompareApplication ReportCompareApplication { get { return (ReportDefinitionReportReportCompareApplication)this["ReportCompareApplication"]; } set { this["ReportCompareApplication"] = value; } }

        [ConfigurationProperty("ReportCompareVersion", IsRequired = false, DefaultValue = null)]
        public ReportDefinitionReportReportCompareVersion ReportCompareVersion { get { return (ReportDefinitionReportReportCompareVersion)this["ReportCompareVersion"]; } set { this["ReportCompareVersion"] = value; } }

        [ConfigurationProperty("Active", IsRequired = false, DefaultValue = false)]
        public bool Active { get { return (bool)this["Active"]; } set { this["Active"] = value; } }
    }
}
