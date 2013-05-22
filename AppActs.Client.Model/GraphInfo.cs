using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Model.Enum;

namespace AppActs.Client.Model
{
    public class GraphInfo
    {
        public string Name { get; private set; }
        public Nullable<Guid> CompareApplicationGuid { get; private set; }
        public Nullable<Guid> CompareVersionGuid { get; private set; }
        public Nullable<Guid> ComparePlatformGuid { get; private set; }
        public Nullable<Guid> DetailGuid { get; private set; }

        public GraphInfo()
        {

        }

        public GraphInfo(ReportDefinition reportDefinition)
        {
            this.Name = reportDefinition.Name;
            this.CompareApplicationGuid =
                reportDefinition.ReportCompareApplication.Guid != Guid.Empty ? reportDefinition.ReportCompareApplication.Guid : new Nullable<Guid>();
            this.ComparePlatformGuid =
                reportDefinition.ReportComparePlatform.Guid != Guid.Empty ? reportDefinition.ReportComparePlatform.Guid : new Nullable<Guid>();
            this.CompareVersionGuid =
                reportDefinition.ReportCompareVersion.Guid != Guid.Empty ? reportDefinition.ReportCompareVersion.Guid : new Nullable<Guid>();
            this.DetailGuid =
                reportDefinition.ReportNormal.Detail.Guid != Guid.Empty ? reportDefinition.ReportNormal.Detail.Guid : new Nullable<Guid>();
        }
    }
}
