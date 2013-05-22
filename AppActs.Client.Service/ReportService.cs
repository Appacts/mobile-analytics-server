using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Service.Interface;
using AppActs.Client.Model.Enum;
using AppActs.Model.Enum;
using AppActs.Client.Model;
using AppActs.Client.Repository.Interface;
using AppActs.Core.Exceptions;
using AppActs.Model;
using System.Collections.Concurrent;
using AppActs.Repository.Interface;
using System.Data;
using MongoDB.Bson;

namespace AppActs.Client.Service
{
    public class ReportService : IReportService
    {
        private readonly IDataRepository dataRepository;
        private readonly IReportRepository reportRepository;
        private readonly AppActs.Client.Repository.Interface.IApplicationRepository applicationRepository;
        private readonly AppActs.Client.Repository.Interface.IPlatformRepository platformRepository;

        public ReportService(IDataRepository dataRepository, AppActs.Client.Repository.Interface.IApplicationRepository applicationRepository, 
            AppActs.Client.Repository.Interface.IPlatformRepository platformRepository, 
            IReportRepository reportRepository)
        {
            this.dataRepository = dataRepository;
            this.applicationRepository = applicationRepository;
            this.platformRepository = platformRepository;
            this.reportRepository = reportRepository;
        }

        /// <summary>
        /// Comparing Application vs Application ( A v A )
        /// </summary>
        public GraphWithTabularCompare GetGraphApplications(Guid graphGuid, Guid applicationId, 
            IEnumerable<Guid> applicationIdsCompare,  DateTime dateStart, DateTime dateEnd)
        {
            GraphWithTabularCompare graphWithTabularCompare = new GraphWithTabularCompare();
  
            try
            {
                if (applicationIdsCompare.Count() > 0)
                {

                    ReportDefinitionReportReportCompareApplication reportApplication =
                        this.reportRepository.GetReportCompareApplication(graphGuid);

                    graphWithTabularCompare.Data = new Graph(reportApplication.Parent, reportApplication);

                    graphWithTabularCompare.Data.Series = this.dataRepository.GetGraphAxis(reportApplication.Query.Value,
                        applicationIdsCompare, dateStart, dateEnd);
                    graphWithTabularCompare.Tabular = graphWithTabularCompare.Data.Series;
                }
            }
            catch (DataAccessLayerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceLayerException(ex);
            }

            return graphWithTabularCompare;
        }

        /// <summary>
        /// Gets the graph with application compare.
        /// </summary>
        public GraphWithTabularCompare<ApplicationMeta, Guid> GetGraphWithApplicationCompare(Guid graphGuid, Guid applicationId,
            DateTime dateStart, DateTime dateEnd)
        {
            GraphWithTabularCompare<ApplicationMeta, Guid> graphWithApplicationCompare =
                new GraphWithTabularCompare<ApplicationMeta, Guid>();

            try
            {
                Application applicationMain = this.applicationRepository.Find(applicationId);

                IEnumerable<Application> applicationsToCompare = this.applicationRepository.FindAll();
                applicationsToCompare = applicationsToCompare.Where(x => x.Guid != applicationMain.Guid).ToList();

                List<ApplicationMeta> applications = new List<ApplicationMeta>();
                if (applicationsToCompare.Count() != 0)
                {
                    applications = applicationsToCompare.Cast<ApplicationMeta>().ToList();
                }
                applications.Add(applicationMain);
                graphWithApplicationCompare.Options = applications;


                List<Guid> applicationsSelected = new List<Guid>();
                if (applicationsToCompare.Count() != 0)
                {
                    applicationsSelected = applicationsToCompare.Select(x => x.Guid).Take(2).ToList();
                }
                applicationsSelected.Add(applicationId);
                graphWithApplicationCompare.Selected = applicationsSelected;
              
                graphWithApplicationCompare.Consume(
                    this.GetGraphApplications(graphGuid, applicationId, applicationsSelected, dateStart, dateEnd)
                );
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceLayerException(ex);
            }

            return graphWithApplicationCompare;
        }

        /// <summary>
        /// Comparing Application, Platform vs Platform A ( P v P )
        /// </summary>
        public GraphWithTabularCompare GetGraphPlatform(Guid graphGuid, Guid applicationId, 
            IEnumerable<PlatformType> platformTypes,  DateTime dateStart, DateTime dateEnd)
        {
            GraphWithTabularCompare graphWithTabularCompare = new GraphWithTabularCompare();

            try
            {
                if (platformTypes.Count() > 0)
                {
                    ReportDefinitionReportComparePlatform reportPlatform =
                        this.reportRepository.GetReportComparePlatform(graphGuid);

                    graphWithTabularCompare.Data = new Graph(reportPlatform.Parent, reportPlatform);

                    graphWithTabularCompare.Data.Series = this.dataRepository.GetGraphAxis(reportPlatform.Query.Value,
                        applicationId, platformTypes, dateStart, dateEnd);

                    graphWithTabularCompare.Tabular = graphWithTabularCompare.Data.Series;
                }
            }
            catch (DataAccessLayerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceLayerException(ex);
            }

            return graphWithTabularCompare;
        }

        /// <summary>
        /// Gets the graph.
        /// </summary>
        public GraphWithTabularCompare<Platform, PlatformType> GetGraphWithPlatformCompare(Guid graphGuid, Guid applicationId, 
            DateTime dateStart, DateTime dateEnd)
        {
            GraphWithTabularCompare<Platform, PlatformType> graphWithPlatformCompare =
                new GraphWithTabularCompare<Platform, PlatformType>();

            try
            {
                Application applicationMain = this.applicationRepository.Find(applicationId);

                IEnumerable<PlatformType> platformIdsToCompare = applicationMain.Platforms.Select(x => x.Type);
                IEnumerable<Platform> platformsToCompare = this.platformRepository.FindAll();
                platformsToCompare = platformsToCompare.Where(x => platformIdsToCompare.Contains(x.Type)).ToList();

                if (platformsToCompare.Count() != 0)
                {
                    graphWithPlatformCompare.Options = platformsToCompare.ToList();
                    graphWithPlatformCompare.Selected = platformsToCompare.Select(x => x.Type).Take(2).ToList();

                    graphWithPlatformCompare.Consume
                        (
                            this.GetGraphPlatform
                            (
                                graphGuid, 
                                applicationId,
                                graphWithPlatformCompare.Selected, 
                                dateStart, 
                                dateEnd
                            )
                        );

                    graphWithPlatformCompare.Tabular = graphWithPlatformCompare.Data.Series;
                }
                else
                {
                    graphWithPlatformCompare.NotEnoughData = true;
                }
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceLayerException(ex);
            }

            return graphWithPlatformCompare;
        }

        /// <summary>
        /// Comparing platform application A (V v V)
        /// </summary>
        public GraphWithTabularCompare GetGraphVersions(Guid graphGuid, Guid applicationId, 
            IEnumerable<string> versions,  DateTime dateStart, DateTime dateEnd)
        {
            GraphWithTabularCompare graphWithTabularCompare = new GraphWithTabularCompare();

            try
            {
                if (versions.Count() > 0)
                {
                    ReportDefinitionReportReportCompareVersion reportVersion = 
                        this.reportRepository.GetReportCompareVersion(graphGuid);

                    graphWithTabularCompare.Data = new Graph(reportVersion.Parent, reportVersion);

                    graphWithTabularCompare.Data.Series = 
                        this.dataRepository.GetGraphAxis
                        (
                            reportVersion.Query.Value, 
                            applicationId,
                            versions, 
                            dateStart, 
                            dateEnd
                        );

                    graphWithTabularCompare.Tabular = graphWithTabularCompare.Data.Series;
                }
            }
            catch (DataAccessLayerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceLayerException(ex);
            }

            return graphWithTabularCompare;
        }

        /// <summary>
        /// Gets the graph.
        /// </summary>
        public GraphWithTabularCompare<string, string> GetGraphWithVersionsCompare(Guid graphGuid, Guid applicationId, 
            DateTime dateStart, DateTime dateEnd)
        {
            GraphWithTabularCompare<string, string> graphWithVersionCompare = 
                new GraphWithTabularCompare<string, string>();

            try
            {
                graphWithVersionCompare.Options = this.applicationRepository.GetVersionsByApplication(applicationId).ToList();

                if (graphWithVersionCompare.Options.Count() != 0)
                {
                    graphWithVersionCompare.Selected = graphWithVersionCompare.Options.Take(3).ToList();
                    graphWithVersionCompare.Consume(
                        this.GetGraphVersions(graphGuid, applicationId, graphWithVersionCompare.Selected, dateStart, dateEnd)
                    );

                    graphWithVersionCompare.Tabular = graphWithVersionCompare.Data.Series;
                }
                else
                {
                    graphWithVersionCompare.NotEnoughData = true;
                }
            }
            catch (DataAccessLayerException)
            {
                throw;
            }
            catch (ServiceLayerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceLayerException(ex);
            }

            return graphWithVersionCompare;
        }

        /// <summary>
        /// Getting statistics for platform and application P A V
        /// </summary>
        public DataWithInfo GetGraphWithInfo(Guid graphGuid, Guid applicationId, 
            DateTime dateStart, DateTime dateEnd)
        {
            DataWithInfo dataWithInfo = new DataWithInfo();

            try
            {
                ReportDefinitionReportNormal reportNormal = this.reportRepository.GetReportNormal(graphGuid);
                dataWithInfo.Info = new GraphInfo(reportNormal.Parent);
                dataWithInfo.Data = new Graph(reportNormal.Parent, reportNormal);

                List<GraphSeries> graphSeries =
                    this.dataRepository.GetGraphAxis(reportNormal.Query.Value, applicationId, dateStart, dateEnd);

                dataWithInfo.Data.Series = graphSeries;
                dataWithInfo.Tabular = graphSeries.First();
            }
            catch (DataAccessLayerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceLayerException(ex);
            }

            return dataWithInfo;
        }

        public object GetDetail(Guid detailGuid, Guid applicationId, DateTime dateStart, DateTime dateEnd, string detailId)
        {
            try
            {
                ReportDefinitionReportDetail reportDetail =
                    this.reportRepository.GetReportDetail(detailGuid);

                Application application = this.applicationRepository.Find(applicationId);

                return this.dataRepository.GetDetail(reportDetail.Query.Value, applicationId, 
                    dateStart, dateEnd, detailId);
            }
            catch (DataAccessLayerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceLayerException(ex);
            }
        }

        public IEnumerable<ReportDefinition> GetReportDefinitions()
        {
            return this.reportRepository.GetAll();
        }
    }
}
