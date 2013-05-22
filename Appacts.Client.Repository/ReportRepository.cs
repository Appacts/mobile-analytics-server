using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Repository.Interface;
using AppActs.Client.Model;
using AppActs.Client.Model.Enum;
using System.Data;
using AppActs.Core.Exceptions;
using System.Data.SqlClient;
using AppActs.Model.Enum;
using AppActs.Core.Xml;
using CacheProvider.Interface;

namespace AppActs.Client.Repository
{
    public class ReportRepository : IReportRepository
    {
        readonly ReportDefinitionsSection reportDefinitionsSection;
        readonly ICacheProvider<ReportDefinition> cacheReportDefinition;
        readonly ICacheProvider<ReportDefinitionReportDetail> cacheReportDetail;
        readonly ICacheProvider<ReportDefinitionReportNormal> cacheReportNormal;
        readonly ICacheProvider<ReportDefinitionReportReportCompareApplication> cacheReportCompareApplication;
        readonly ICacheProvider<ReportDefinitionReportReportCompareVersion> cacheReportCompareVersion;
        readonly ICacheProvider<ReportDefinitionReportComparePlatform> cacheReportComparePlatform;
        readonly ICacheProvider<ReportDefinitionSummary> cacheReportSummary;

        public ReportRepository(ReportDefinitionsSection reportDefinitionsSection, 
            ICacheProvider<ReportDefinition> cacheReportDefinition, 
            ICacheProvider<ReportDefinitionReportDetail> cacheReportDetail,
            ICacheProvider<ReportDefinitionReportNormal> cacheReportNormal, 
            ICacheProvider<ReportDefinitionReportReportCompareApplication> cacheReportCompareApplication,
            ICacheProvider<ReportDefinitionReportReportCompareVersion> cacheReportCompareVersion, 
            ICacheProvider<ReportDefinitionReportComparePlatform> cacheReportComparePlatform,
            ICacheProvider<ReportDefinitionSummary> cacheReportSummary)
        {
            this.reportDefinitionsSection = reportDefinitionsSection;
            this.cacheReportDefinition = cacheReportDefinition;
            this.cacheReportDetail = cacheReportDetail;
            this.cacheReportNormal = cacheReportNormal;
            this.cacheReportCompareApplication = cacheReportCompareApplication;
            this.cacheReportCompareVersion = cacheReportCompareVersion;
            this.cacheReportComparePlatform = cacheReportComparePlatform;
            this.cacheReportSummary = cacheReportSummary;
        }

        public IEnumerable<ReportDefinition> GetAll()
        {
            return this.cacheReportDefinition.Fetch
                (
                    "ReportRepository.GetAll", 
                    () => { 
                        List<ReportDefinition> reportDefs = new List<ReportDefinition>();
                        foreach (ReportDefinition reportDef in reportDefinitionsSection.Definitions)
                            reportDefs.Add(reportDef);
                        return reportDefs;
                    },  null, null
                );
        }

        public ReportDefinitionReportDetail GetReportDetail(Guid id)
        {
            return this.cacheReportDetail.Fetch
                (
                    String.Format("ReortRepository.GetReportDetail.{0}", id),
                    () =>
                    {
                        return this.GetAll()
                            .Where(x => x.ReportNormal != null && x.ReportNormal.Detail != null && x.ReportNormal.Detail.Guid == id)
                            .Select(x => x.ReportNormal.Detail).First();
                    }, null, null
                );
        }

        public ReportDefinitionReportNormal GetReportNormal(Guid id)
        {
            return this.cacheReportNormal.Fetch
                (
                    String.Format("ReortRepository.GetReportNormal.{0}", id),
                    () =>
                    {
                        return this.GetAll()
                            .Where(x => x.ReportNormal != null && x.ReportNormal.Guid == id)
                            .Select(x => { x.ReportNormal.Parent = x; return x.ReportNormal; }).First();
                    }, null, null
                );
        }

        public ReportDefinitionReportReportCompareApplication GetReportCompareApplication(Guid id)
        {
            return this.cacheReportCompareApplication.Fetch
                (
                    String.Format("ReortRepository.GetReportCompareApplication.{0}", id),
                    () =>
                    {
                        return this.GetAll()
                            .Where(x => x.ReportCompareApplication != null && x.ReportCompareApplication.Guid == id)
                            .Select(x => { x.ReportCompareApplication.Parent = x; return x.ReportCompareApplication; }).First();
                    }, null, null
                );
        }

        public ReportDefinitionReportReportCompareVersion GetReportCompareVersion(Guid id)
        {
            return this.cacheReportCompareVersion.Fetch
                (
                    String.Format("ReortRepository.GetReportCompareVersion.{0}", id),
                    () =>
                    {
                        return this.GetAll()
                            .Where(x => x.ReportCompareVersion != null && x.ReportCompareVersion.Guid == id)
                            .Select(x => { x.ReportCompareVersion.Parent = x; return x.ReportCompareVersion; }).First();
                    }, null, null
                );
        }

        public ReportDefinitionReportComparePlatform GetReportComparePlatform(Guid id)
        {
            return this.cacheReportComparePlatform.Fetch
                (
                    String.Format("ReortRepository.GetReportComparePlatform.{0}", id),
                    () =>
                    {
                        return this.GetAll()
                            .Where(x => x.ReportComparePlatform != null && x.ReportComparePlatform.Guid == id)
                            .Select(x => { x.ReportComparePlatform.Parent = x; return x.ReportComparePlatform; }).First();
                    }, null, null
                );
        }

        public ReportDefinitionSummary GetReportSummary(Guid id)
        {
            return this.cacheReportSummary.Fetch
                (
                    String.Format("ReortRepository.GetReportSummary.{0}", id),
                    () =>
                    {
                        return this.GetAll()
                            .Where(x => x.Summary != null && x.Summary.Guid == id)
                            .Select(x => x.Summary).First();
                    }, null, null
                );
        }
    }
}
