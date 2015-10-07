using System.Threading.Tasks;

namespace Autofac.Events
{
    public class AsyncEventPublisher
    {
        public AsyncEventPublisher(ILifetimeScope scope)
        {
            _scope = scope;
        }

        private readonly ILifetimeScope _scope;

        public Task Publish(object @event)
        {
            return _scope.PublishAsyncEvent(@event);
        }
    }
}
