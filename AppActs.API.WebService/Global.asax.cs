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
using System.Threading;
using log4net;
using AppActs.API.Model;

namespace AppActs.API.WebService
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string database = ConfigurationManager.AppSettings["database"];

            IServiceLocator serviceLocator = new ServiceLocatorCastle(
                new InstallRepository(connectionString, database),
                new InstallService()
            );

            Settings settings = new Settings();
            settings.DataLoggingRecordRaw = Boolean.Parse(ConfigurationManager.AppSettings["data.logging.record.rawData"]);
            settings.DataLoggingRecordSystemErrors = Boolean.Parse(ConfigurationManager.AppSettings["data.logging.record.systemErrors"]);

            serviceLocator.Register<Settings>(settings);

            this.Application.Add(ContainerKeys.APPLICATION, serviceLocator);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            IServiceLocator serviceLocator =
                this.Application[ContainerKeys.APPLICATION] as IServiceLocator;

            serviceLocator.Dispose();
        }
    }
}