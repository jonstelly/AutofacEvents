using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Builder;
using Autofac.Events;

// ReSharper disable once CheckNamespace
namespace Autofac
{
    public static class BuilderExtensions
    {
        public static IRegistrationBuilder<EventPublisher, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterEventing(this ContainerBuilder builder)
        {
            return builder.RegisterType<EventPublisher>().AsSelf().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
