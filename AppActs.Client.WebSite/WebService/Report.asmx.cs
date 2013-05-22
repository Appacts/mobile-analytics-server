using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Castle.Windsor;
using AppActs.Mvp;
using AppActs.Client.Service.Interface;
using log4net;
using AppActs.Client.Model;
using AppActs.Core.Exceptions;
using AppActs.Model;
using AppActs.Model.Enum;
using AppActs.Core.Di;
using System.Data;
using MongoDB.Bson;

namespace AppActs.Client.WebSite.WebService
{
    /// <summary>
    /// Summary description for Report
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Report : 
        System.Web.Services.WebService
    {
        private IServiceLocator serviceLocator
        {
            get
            {
                return (IServiceLocator)this.Application[ContainerKeys.APPLICATION];
            }
        }
        private ILog iLog
        {
            get
            {
                return this.serviceLocator.Resolve<ILog>();
            }
        }
        private IReportService iReportService
        {
            get
            {
                return this.serviceLocator.Resolve<IReportService>();
            }
        }


        [WebMethod]
        public GraphWithTabularCompare GetGraphApplications(Guid graphGuid, Guid applicationId, 
            List<Guid> applicationIdCompare, DateTime dateStart, DateTime dateEnd)
        {
            GraphWithTabularCompare graphWithTabularCompare = null;
            try
            {
                graphWithTabularCompare = this.iReportService.GetGraphApplications(graphGuid, applicationId, 
                    applicationIdCompare, dateStart, dateEnd);
            }
            catch (Exception ex)
            {
                this.iLog.Error(ex);
            }
            return graphWithTabularCompare;
        }

        [WebMethod]
        public GraphWithTabularCompare<ApplicationMeta, Guid> GetGraphWithApplicationCompare(Guid graphGuid, Guid applicationId, DateTime dateStart, 
            DateTime dateEnd)
        {
            GraphWithTabularCompare<ApplicationMeta, Guid> graphWithApplicationCompare = null;
            try
            {
                graphWithApplicationCompare = this.iReportService.GetGraphWithApplicationCompare(graphGuid, applicationId, dateStart, dateEnd);
            }
            catch (Exception ex)
            {
                this.iLog.Error(ex);
            }
            return graphWithApplicationCompare;
        }

        [WebMethod]
        public GraphWithTabularCompare GetGraphPlatform(Guid graphGuid, Guid applicationId, 
            List<PlatformType> platformTypes, DateTime dateStart, DateTime dateEnd)
        {
            GraphWithTabularCompare graphWithTabularCompare = null;
            try
            {
                graphWithTabularCompare = this.iReportService.GetGraphPlatform(graphGuid, applicationId,
                    platformTypes, dateStart, dateEnd);
            }
            catch (Exception ex)
            {
                this.iLog.Error(ex);
            }
            return graphWithTabularCompare;
        }

        [WebMethod]
        public GraphWithTabularCompare<Platform, PlatformType> GetGraphWithPlatformCompare(Guid graphGuid, Guid applicationId, 
            DateTime dateStart, DateTime dateEnd)
        {
            GraphWithTabularCompare<Platform, PlatformType> graphWithPlatformCompare = null;
            try
            {
                graphWithPlatformCompare = this.iReportService.GetGraphWithPlatformCompare(graphGuid, applicationId, dateStart, dateEnd);
            }
            catch (Exception ex)
            {
                this.iLog.Error(ex);
            }
            return graphWithPlatformCompare;
        }

        [WebMethod]
        public GraphWithTabularCompare GetGraphVersions(Guid graphGuid, Guid applicationId, List<string> versions, DateTime dateStart, DateTime dateEnd)
        {
            GraphWithTabularCompare graph = null;
            try
            {
                graph = this.iReportService.GetGraphVersions(graphGuid, applicationId, versions, dateStart, dateEnd);
            }
            catch (Exception ex)
            {
                this.iLog.Error(ex);
            }
            return graph;
        }

        [WebMethod]
        public GraphWithTabularCompare<string, string> GetGraphWithVersionsCompare(Guid graphGuid, Guid applicationId, 
            DateTime dateStart, DateTime dateEnd)
        {
            GraphWithTabularCompare<string, string> graphWithVersionsCompare = null;
            try
            {
                graphWithVersionsCompare = this.iReportService.GetGraphWithVersionsCompare(graphGuid, applicationId, dateStart, dateEnd);
            }
            catch (Exception ex)
            {
                this.iLog.Error(ex);
            }
            return graphWithVersionsCompare;
        }

        [WebMethod]
        public DataWithInfo GetGraphWithInfo(Guid graphGuid, Guid applicationId, DateTime dateStart, DateTime dateEnd)
        {
            DataWithInfo graphWithInfo = null;
            try
            {
                graphWithInfo = this.iReportService.GetGraphWithInfo(graphGuid, applicationId, dateStart, dateEnd);
            }
            catch (Exception ex)
            {
                this.iLog.Error(ex);
            }
            return graphWithInfo;
        }

        [WebMethod]
        public object GetDetail(Guid reportGuid, Guid applicationId, DateTime dateStart, DateTime dateEnd, string detailId)
        {
            try
            {
                return this.iReportService.GetDetail(reportGuid, applicationId, dateStart, dateEnd, detailId);
            }
            catch (Exception ex)
            {
                this.iLog.Error(ex);
            }

            return null;
        }
    }
}
