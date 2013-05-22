using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using AppActs.API.Repository.Interface;
using AppActs.API.Repository;
using MongoDB.Driver;
using AppActs.API.DataMapper.Interface;
using AppActs.API.DataMapper;

namespace AppActs.API.WebService
{
    public class InstallRepository : IWindsorInstaller
    {
        string connectionString;
        string database;

        public InstallRepository(string connectionString, string database)
        {
            this.connectionString = connectionString;
            this.database = database;
        }

        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            MongoClient client = new MongoClient(this.connectionString);

            container.Register(Component.For<IAppUserMapper>().Instance(new AppUserMapper(client, this.database)));
            container.Register(Component.For<ICrashMapper>().Instance(new CrashMapper(client, this.database)));
            container.Register(Component.For<IDeviceMapper>().Instance(new DeviceMapper(client, this.database)));
            container.Register(Component.For<IErrorMapper>().Instance(new ErrorMapper(client, this.database)));
            container.Register(Component.For<IEventMapper>().Instance(new EventMapper(client, this.database)));
            container.Register(Component.For<IFeedbackMapper>().Instance(new FeedbackMapper(client, this.database)));
            container.Register(Component.For<ISystemErrorMapper>().Instance(new SystemErrorMapper(client, this.database)));

            container.Register(Component.For<AppActs.Repository.Interface.IApplicationRepository>().Instance(new AppActs.Repository.ApplicationRepository(client, this.database)));
            container.Register(Component.For<IDeviceRepository>().ImplementedBy<DeviceRepository>());
            container.Register(Component.For<IFeedbackRepository>().ImplementedBy<FeedbackRepository>());
            container.Register(Component.For<IEventRepository>().ImplementedBy<EventRepository>());
            container.Register(Component.For<ICrashRepository>().ImplementedBy<CrashRepository>());
            container.Register(Component.For<IAppUserRepository>().ImplementedBy<AppUserRepository>());
            container.Register(Component.For<IErrorRepository>().ImplementedBy<ErrorRepository>());
            container.Register(Component.For<ISystemErrorRepository>().ImplementedBy<SystemErrorRepository>());
        }
    }
}
