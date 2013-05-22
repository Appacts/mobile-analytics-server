using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AppActs.Client.Model
{
    public class ReportDefinitionReportNormal : ReportDefinitionReport
    {
        [ConfigurationProperty("Detail", IsRequired = false)]
        public ReportDefinitionReportDetail Detail { get { return (ReportDefinitionReportDetail)this["Detail"]; } set { this["Detail"] = value; } }
    }
}
