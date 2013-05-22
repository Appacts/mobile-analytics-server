using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using AppActs.Client.Presenter;
using System.Collections.Concurrent;
using Mosaic.Mvp.Pipeline.Interface;
using Mosaic.Mvp.Pipeline;
using log4net;
using AppActs.Core.Di;
using AppActs.Model;
using AppActs.Client.Model;

namespace AppActs.Client.WebSite
{
    public class InstallPresenter : IWindsorInstaller
    {
        AppActs.Client.Model.Settings settings;

        public InstallPresenter(AppActs.Client.Model.Settings settings)
        {
            this.settings = settings;
        }

        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<User>().ImplementedBy<User>().LifeStyle.Custom<PerSessionLifestyleManager>());
            container.Register(Component.For<ReportFilter>().ImplementedBy<ReportFilter>().LifeStyle.Custom<PerSessionLifestyleManager>());

            container.Register(Component.For<IPipeline>().ImplementedBy<Pipeline>().LifeStyle.PerWebRequest);
            container.Register(Component.For<ILog>().Instance(this.createLogger(log4net.Core.Level.All)));
            container.Register(Component.For<AppActs.Client.Model.Settings>().Instance(settings));

            container.Register(Component.For<SetupPresenter>().ImplementedBy<SetupPresenter>().LifeStyle.PerWebRequest);
            container.Register(Component.For<SetupAppViewPresenter>().ImplementedBy<SetupAppViewPresenter>().LifeStyle.PerWebRequest);
            container.Register(Component.For<SetupAppUpdatePresenter>().ImplementedBy<SetupAppUpdatePresenter>().LifeStyle.PerWebRequest);
            container.Register(Component.For<SetupAppAddPresenter>().ImplementedBy<SetupAppAddPresenter>().LifeStyle.PerWebRequest);
            container.Register(Component.For<SettingsPresenter>().ImplementedBy<SettingsPresenter>().LifeStyle.PerWebRequest);
            container.Register(Component.For<MainPresenter>().ImplementedBy<MainPresenter>().LifeStyle.PerWebRequest);
            container.Register(Component.For<ErrorPresenter>().ImplementedBy<ErrorPresenter>().LifeStyle.PerWebRequest);
            container.Register(Component.For<AccountUserUpdatePresenter>().ImplementedBy<AccountUserUpdatePresenter>().LifeStyle.PerWebRequest);
            container.Register(Component.For<AccountUserManagementPresenter>().ImplementedBy<AccountUserManagementPresenter>().LifeStyle.PerWebRequest);
            container.Register(Component.For<AccountUserLoginPresenter>().ImplementedBy<AccountUserLoginPresenter>().LifeStyle.PerWebRequest);
            container.Register(Component.For<AccountUserLoggedInPresenter>().ImplementedBy<AccountUserLoggedInPresenter>().LifeStyle.PerWebRequest);
            container.Register(Component.For<AccountUserForgotPasswordPresenter>().ImplementedBy<AccountUserForgotPasswordPresenter>().LifeStyle.PerWebRequest);
            container.Register(Component.For<AccountUserForgotPasswordChangePresenter>().ImplementedBy<AccountUserForgotPasswordChangePresenter>().LifeStyle.PerWebRequest);
            container.Register(Component.For<DefaultPresenter>().ImplementedBy<DefaultPresenter>().LifeStyle.PerWebRequest);
        }

        //TODO: move this in to the config file
        private ILog createLogger(log4net.Core.Level logLevel)
        {
            log4net.Appender.RollingFileAppender rollingFileAppender = new log4net.Appender.RollingFileAppender();
            rollingFileAppender.Name = "Client";
            rollingFileAppender.AppendToFile = true;
            rollingFileAppender.RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Size;
            rollingFileAppender.MaximumFileSize = "500KB";
            rollingFileAppender.MaxSizeRollBackups = 100;
            rollingFileAppender.StaticLogFileName = true;
            rollingFileAppender.File = "Logs\\Client.log";
            rollingFileAppender.LockingModel = new log4net.Appender.FileAppender.MinimalLock();
            rollingFileAppender.Layout = new log4net.Layout.PatternLayout("%date [%thread] %-5level %logger - %message%newline");
            rollingFileAppender.ImmediateFlush = true;
            rollingFileAppender.Threshold = logLevel;
            rollingFileAppender.ActivateOptions();
            log4net.Config.BasicConfigurator.Configure(rollingFileAppender);
            return log4net.LogManager.GetLogger("Client");
        }
    }
}
