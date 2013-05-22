using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Model;
using AppActs.Client.Model.Enum;

namespace AppActs.Client.Repository.Interface
{
    public interface IReportRepository
    {
        IEnumerable<ReportDefinition> GetAll();
        ReportDefinitionReportDetail GetReportDetail(Guid id);
        ReportDefinitionReportNormal GetReportNormal(Guid id);
        ReportDefinitionReportReportCompareApplication GetReportCompareApplication(Guid id);
        ReportDefinitionReportReportCompareVersion GetReportCompareVersion(Guid id);
        ReportDefinitionReportComparePlatform GetReportComparePlatform(Guid id);
        ReportDefinitionSummary GetReportSummary(Guid id);
    }
}
