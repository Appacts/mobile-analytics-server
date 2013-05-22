using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using CacheProvider.Interface;
using AppActs.Client.Model.Enum;
using CacheProvider;
using Castle.Facilities.TypedFactory;

namespace AppActs.Client.WebSite
{
    public class InstallCache : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof(ICacheProvider<>))
                .ImplementedBy(typeof(CacheProvider<>))
                .LifeStyle.Transient);

            container.AddFacility<TypedFactoryFacility>();

            container.Register(
                Component.For<ICacheDependencyFactory>()
                .AsFactory());
        }
    }
}
