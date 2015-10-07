using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac.Events;

// ReSharper disable once CheckNamespace
namespace Autofac
{
    public static class LifetimeScopeExtensions
    {
        #region Regular Eventing

        public static void PublishEvent(this ILifetimeScope scope, object @event)
        {
            var exceptions = new List<Exception>();
            foreach (dynamic handler in scope.ResolveHandlers(@event))
            {
                try
                {
                    handler.Handle((dynamic)@event);
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }
            }
            if (exceptions.Count > 0)
                throw new AggregateException(exceptions);
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

        #endregion

        #region Async Eventing

        public static async Task PublishAsyncEvent(this ILifetimeScope scope, object @event)
        {
            var exceptions = new List<Exception>();
            foreach (dynamic handler in scope.ResolveAsyncHandlers(@event))
            {
                try
                {
                    await (Task)handler.HandleAsync((dynamic)@event);
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }
            }
            if (exceptions.Count > 0)
                throw new AggregateException(exceptions);
        }

        public static IEnumerable<dynamic> ResolveAsyncHandlers<TEvent>(this ILifetimeScope scope, TEvent @event)
        {
            var eventType = @event.GetType();
            return scope.ResolveConcreteAsyncHandlers(eventType)
                .Union(scope.ResolveInterfaceAsyncHandlers(eventType));
        }

        private static IEnumerable<dynamic> ResolveConcreteAsyncHandlers(this ILifetimeScope scope, Type eventType)
        {
            return (IEnumerable<dynamic>)scope.Resolve(MakeAsyncHandlerType(eventType));
        }

        private static IEnumerable<dynamic> ResolveInterfaceAsyncHandlers(this ILifetimeScope scope, Type eventType)
        {
            return eventType.GetTypeInfo().ImplementedInterfaces.SelectMany(i => (IEnumerable<dynamic>)scope.Resolve(MakeAsyncHandlerType(i))).Distinct();
        }

        private static Type MakeAsyncHandlerType(Type type)
        {
            return typeof(IEnumerable<>).MakeGenericType(typeof(IHandleEvent<>).MakeGenericType(type));
        }

        #endregion
    }
}
