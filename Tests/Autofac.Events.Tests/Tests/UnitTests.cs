using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.Variance;

namespace Autofac.Events.Tests
{
    public abstract class UnitTests
    {
        public ILifetimeScope BeginScope()
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterEventing();
            builder.RegisterInstance(this).AsSelf().AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(IHandleEvent<>).Assembly, GetType().Assembly)
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            return builder.Build();
        }
    }
}
