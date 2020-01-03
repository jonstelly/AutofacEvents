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
            if (@event == null)
                return;

            var exceptions = new List<Exception>();
            var handleMethod = typeof(IHandleEvent<>).MakeGenericType(@event.GetType()).GetMethod("Handle");
            var handleAsyncMethod = typeof(IHandleEventAsync<>).MakeGenericType(@event.GetType()).GetMethod("HandleAsync");

            foreach (var handler in scope.ResolveHandlers(@event))
            {
                try
                {
                    //Invoke sync handler
                    handleMethod.Invoke(handler, new[] {@event});
                }
                catch (TargetInvocationException ti)
                {
                    //Unwrap TargetInvocationException
                    exceptions.Add(ti.InnerException ?? ti);
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }
            }
            foreach (var asyncHandler in scope.ResolveAsyncHandlers(@event))
            {
                try
                {
                    //Invoke async handler
                    var task = (Task)handleAsyncMethod.Invoke(asyncHandler, new[] { @event });
                    task.GetAwaiter().GetResult();
                }
                catch (TargetInvocationException ti)
                {
                    //Unwrap TargetInvocationException
                    exceptions.Add(ti.InnerException ?? ti);
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }
            }
            if (exceptions.Count > 0)
                throw new AggregateException(exceptions);
        }

        public static async Task PublishEventAsync(this ILifetimeScope scope, object @event)
        {
            if (@event == null)
                return;

            var exceptions = new List<Exception>();
            //TODO: Investigate synchronous vs. synchronous handling here, ConfigureAwait()?  Seems problematic given reliance on scope
            var handleMethod = typeof(IHandleEvent<>).MakeGenericType(@event.GetType()).GetMethod("Handle");
            var handleAsyncMethod = typeof(IHandleEventAsync<>).MakeGenericType(@event.GetType()).GetMethod("HandleAsync");

            foreach (var handler in scope.ResolveHandlers(@event))
            {
                try
                {
                    //Invoke sync handler
                    handleMethod.Invoke(handler, new[] { @event });
                }
                catch (TargetInvocationException ti)
                {
                    //Unwrap TargetInvocationException
                    exceptions.Add(ti.InnerException ?? ti);
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }
            }

            foreach (var asyncHandler in scope.ResolveAsyncHandlers(@event))
            {
                try
                {
                    //Invoke async handler
                    var task = (Task)handleAsyncMethod.Invoke(asyncHandler, new[] { @event });
                    await task;
                }
                catch (TargetInvocationException ti)
                {
                    //Unwrap TargetInvocationException
                    exceptions.Add(ti.InnerException ?? ti);
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }
            }
            if (exceptions.Count > 0)
                throw new AggregateException(exceptions);
        }

        public static IEnumerable<object> ResolveHandlers<TEvent>(this ILifetimeScope scope, TEvent @event)
        {
            var eventType = @event.GetType();
            return scope.ResolveConcreteHandlers(eventType, MakeHandlerType)
                .Union(scope.ResolveInterfaceHandlers(eventType, MakeHandlerType));
        }

        public static IEnumerable<object> ResolveAsyncHandlers<TEvent>(this ILifetimeScope scope, TEvent @event)
        {
            var eventType = @event.GetType();
            return scope.ResolveConcreteHandlers(eventType, MakeAsyncHandlerType)
                .Union(scope.ResolveInterfaceHandlers(eventType, MakeAsyncHandlerType));
        }

        private static IEnumerable<object> ResolveConcreteHandlers(this ILifetimeScope scope, Type eventType, Func<Type, Type> handlerFactory)
        {
            return (IEnumerable<dynamic>)scope.Resolve(handlerFactory(eventType));
        }

        private static IEnumerable<object> ResolveInterfaceHandlers(this ILifetimeScope scope, Type eventType, Func<Type, Type> handlerFactory)
        {
            return eventType.GetTypeInfo().ImplementedInterfaces.SelectMany(i => (IEnumerable<dynamic>)scope.Resolve(handlerFactory(i))).Distinct();
        }

        private static Type MakeHandlerType(Type type)
        {
            return typeof(IEnumerable<>).MakeGenericType(typeof(IHandleEvent<>).MakeGenericType(type));
        }

        private static Type MakeAsyncHandlerType(Type type)
        {
            return typeof(IEnumerable<>).MakeGenericType(typeof(IHandleEventAsync<>).MakeGenericType(type));
        }
    }
}
