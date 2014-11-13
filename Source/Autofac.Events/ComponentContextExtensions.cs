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
    public static class ComponentContextExtensions
    {
        public static void PublishEvent(this IComponentContext context, object @event)
        {
            foreach (dynamic handler in context.ResolveHandlers(@event))
            {
                handler.Handle((dynamic)@event);
            }            
        }

        public static IEnumerable<dynamic> ResolveHandlers<TEvent>(this IComponentContext context, TEvent @event)
        {
            var eventType = @event.GetType();
            return context.ResolveConcreteHandlers(eventType)
                .Union(context.ResolveInterfaceHandlers(eventType));
        }

        private static IEnumerable<dynamic> ResolveConcreteHandlers(this IComponentContext context, Type eventType)
        {
            return (IEnumerable<dynamic>)context.Resolve(MakeHandlerType(eventType));
        }

        private static IEnumerable<dynamic> ResolveInterfaceHandlers(this IComponentContext context, Type eventType)
        {
            return eventType.GetTypeInfo().ImplementedInterfaces.SelectMany(i => (IEnumerable<dynamic>)context.Resolve(MakeHandlerType(i))).Distinct();
        }

        private static Type MakeHandlerType(Type type)
        {
            return typeof(IEnumerable<>).MakeGenericType(typeof(IHandleEvent<>).MakeGenericType(type));
        }
    }
}
