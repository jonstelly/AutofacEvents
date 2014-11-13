using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac.Events;

// ReSharper disable once CheckNamespace
namespace Autofac
{
    public static class LifetimeScopeExtensions
    {
        public static void PublishEvent(this ILifetimeScope scope, object @event)
        {
            foreach (dynamic handler in scope.ResolveHandlers(@event))
            {
                handler.Handle((dynamic)@event);
            }            
        }

        public static IEnumerable<dynamic> ResolveHandlers<TEvent>(this ILifetimeScope scope, TEvent @event)
        {
            var eventType = @event.GetType();
            return scope.ResolveConcreteHandlers(eventType)
                .Union(scope.ResolveInterfaceHandlers(eventType));
        }

        private static IEnumerable<dynamic> ResolveConcreteHandlers(this ILifetimeScope scope, Type eventType)
        {
            return (IEnumerable<dynamic>)scope.Resolve(MakeHandlerType(eventType));
        }

        private static IEnumerable<dynamic> ResolveInterfaceHandlers(this ILifetimeScope scope, Type eventType)
        {
            return eventType.GetTypeInfo().ImplementedInterfaces.SelectMany(i => (IEnumerable<dynamic>)scope.Resolve(MakeHandlerType(i))).Distinct();
        }

        private static Type MakeHandlerType(Type type)
        {
            return typeof(IEnumerable<>).MakeGenericType(typeof(IHandleEvent<>).MakeGenericType(type));
        }
    }
}
