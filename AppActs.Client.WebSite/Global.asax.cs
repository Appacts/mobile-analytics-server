using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using AppActs.Mvp;
using Castle.Windsor;
using AppActs.Core.Di;
using System.Configuration;
using AppActs.Client.Model.Enum;
using AppActs.Client.Model;
using CacheProvider.Interface;

namespace AppActs.Client.WebSite
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string database = ConfigurationManager.AppSettings["database"];

            AppActs.Client.Model.Settings settings = new Model.Settings
            (
                ConfigurationManager.AppSettings["websiteEmailFrom"],
                ConfigurationManager.AppSettings["websiteEmailTo"],
                ConfigurationManager.AppSettings["websiteUrl"],
                Boolean.Parse(ConfigurationManager.AppSettings["websiteSupportSecureConnection"]),
                ConfigurationManager.AppSettings["securityKey"],
                ConfigurationManager.AppSettings["websiteEmailFromDisplayName"]
            );

            ReportDefinitionsSection reportDefinitionsSection;
            try
            {
                reportDefinitionsSection = (ReportDefinitionsSection)ConfigurationManager.GetSection("ReportDefinitionsSection");
            }
            catch (Exception ex)
            {
                throw new Exception("Report definitions are not parsing, please ensure that they have been updated correctly.", ex);
            }

            IServiceLocator iServiceLocator = new ServiceLocatorCastle(
                new InstallRepository(connectionString, database, reportDefinitionsSection),
                new InstallService(),
                new InstallPresenter(settings),
                new InstallCache()
            );

            this.Application.Add(ContainerKeys.APPLICATION, iServiceLocator);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            IServiceLocator iServiceLocator =
                this.Application[ContainerKeys.APPLICATION] as IServiceLocator;

            iServiceLocator.Dispose();
        }
    }
}