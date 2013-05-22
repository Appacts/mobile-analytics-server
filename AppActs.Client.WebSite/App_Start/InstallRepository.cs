using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using AppActs.Client.Model;
using AppActs.Client.Repository.Interface;
using AppActs.Client.Repository;
using MongoDB.Driver;

namespace AppActs.Client.WebSite
{
    public class InstallRepository : IWindsorInstaller
    {
        string connectionString;
        string database;
        ReportDefinitionsSection reportDefinitionsSection;

        public InstallRepository(string connectionString, string database, ReportDefinitionsSection reportDefinitionsSection)
        {
            this.connectionString = connectionString;
            this.database = database;
            this.reportDefinitionsSection = reportDefinitionsSection;
        }

        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            MongoClient client = new MongoClient(this.connectionString);

            container.Register(Component.For<ReportDefinitionsSection>().Instance(this.reportDefinitionsSection));
            container.Register(Component.For<IUserRepository>().Instance(new UserRepository(client, this.database)));
            container.Register(Component.For<AppActs.Repository.Interface.IApplicationRepository>().ImplementedBy<AppActs.Repository.ApplicationWithCacheRepository>());
            container.Register(Component.For<AppActs.Repository.Interface.IApplicationRepository>().Instance(new AppActs.Repository.ApplicationRepository(client, this.database)));
            container.Register(Component.For<IApplicationRepository>().Instance(new ApplicationRepository(client, this.database)));
            container.Register(Component.For<IPlatformRepository>().Instance(new PlatformRepository(client, this.database)));
            container.Register(Component.For<IReportRepository>().ImplementedBy<ReportRepository>());
            container.Register(Component.For<IDataRepository>().Instance(new DataRepository(client, this.database)));
            container.Register(Component.For<ITileRepository>().Instance(new TileRepository(client, this.database)));
        }
    }
}
