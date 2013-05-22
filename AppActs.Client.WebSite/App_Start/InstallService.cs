using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using AppActs.Client.Service.Interface;
using AppActs.Client.Service;

namespace AppActs.Client.WebSite
{
    public class InstallService : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<IUserService>().ImplementedBy<UserService>());
            container.Register(Component.For<IApplicationService>().ImplementedBy<ApplicationService>());
            container.Register(Component.For<IReportService>().ImplementedBy<ReportService>());
            container.Register(Component.For<IEmailService>().ImplementedBy<EmailService>());
            container.Register(Component.For<ITileService>().ImplementedBy<TileService>());
        }
    }
}
