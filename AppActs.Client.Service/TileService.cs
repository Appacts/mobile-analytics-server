using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Service.Interface;
using AppActs.Client.Repository.Interface;
using AppActs.Client.Model;
using AppActs.Model;
using System.Collections.Concurrent;
using AppActs.Repository.Interface;
using MongoDB.Bson;
using AppActs.Client.Model.Enum;

namespace AppActs.Client.Service
{
    public class TileService : ITileService
    {
        private readonly ITileRepository tileRepository;
        private readonly AppActs.Repository.Interface.IApplicationRepository applicationRepository;
        private readonly IReportRepository reportRepository;

        public TileService(ITileRepository tileRepository, 
           AppActs.Repository.Interface.IApplicationRepository applicationRepository, IReportRepository reportRepository)
        {
            this.tileRepository = tileRepository;
            this.applicationRepository = applicationRepository;
            this.reportRepository = reportRepository;
        }

        public object GetTile(Guid tileGuid, Guid applicationId, DateTime startDate, DateTime endDate, 
            DateTime? startDateCompare, DateTime? endDateCompare)
        {
            ReportDefinitionSummary reportSummary = this.reportRepository.GetReportSummary(tileGuid);
            if (reportSummary.Type == TileType.TrendAndValueOne ||
                reportSummary.Type == TileType.TrendAndValueTwo ||
                reportSummary.Type == TileType.TrendAndValueThree)
            {
                if (!startDateCompare.HasValue && !endDateCompare.HasValue)
                {
                    startDateCompare = startDate.AddMonths(6);
                    endDateCompare = endDate.AddMonths(6);
                }

                return this.tileRepository.Get(reportSummary.Type, reportSummary.Query.Value, applicationId, startDate, endDate, startDateCompare.Value, endDateCompare.Value);
            }
            else
            {
                return this.tileRepository.Get(reportSummary.Type, reportSummary.Query.Value, applicationId, startDate, endDate);
            }
        }

    }
}
