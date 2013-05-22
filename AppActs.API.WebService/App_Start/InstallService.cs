using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using log4net;
using System.Collections.Concurrent;
using AppActs.API.Service.Interface;
using AppActs.API.Service;
using System.Threading;

namespace AppActs.API.WebService
{
    public class InstallService : IWindsorInstaller
    {
        public InstallService()
        {

        }

        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<ILog>().Instance(createLogger(log4net.Core.Level.All)).LifeStyle.Singleton);
            container.Register(Component.For<IDeviceService>().ImplementedBy<DeviceService>().LifeStyle.Singleton);
        }

        /// <summary>
        /// Creates the log.
        /// </summary>
        /// <param name="cfg">The CFG.</param>
        /// <returns></returns>
        private ILog createLogger(log4net.Core.Level logLevel)
        {
            log4net.Appender.RollingFileAppender rollingFileAppender = new log4net.Appender.RollingFileAppender();
            rollingFileAppender.Name = "API";
            rollingFileAppender.AppendToFile = true;
            rollingFileAppender.RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Size;
            rollingFileAppender.MaximumFileSize = "500KB";
            rollingFileAppender.MaxSizeRollBackups = 100;
            rollingFileAppender.StaticLogFileName = true;
            rollingFileAppender.File = "Logs\\API.log";
            rollingFileAppender.LockingModel = new log4net.Appender.FileAppender.MinimalLock();
            rollingFileAppender.Layout = new log4net.Layout.PatternLayout("%date [%thread] %-5level %logger - %message%newline");
            rollingFileAppender.ImmediateFlush = true;
            rollingFileAppender.Threshold = logLevel;
            rollingFileAppender.ActivateOptions();
            log4net.Config.BasicConfigurator.Configure(rollingFileAppender);
            return log4net.LogManager.GetLogger("API");
        }
    }
}
