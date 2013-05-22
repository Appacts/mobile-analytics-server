using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CacheProvider.Interface;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using CacheProvider;

namespace AppActs.API.WebService
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