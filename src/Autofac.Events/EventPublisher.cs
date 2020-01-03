using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.Events
{
    public class EventPublisher : IEventPublisher, IAsyncEventPublisher
    {
        public EventPublisher(ILifetimeScope scope)
        {
            _scope = scope;
        }

        private readonly ILifetimeScope _scope;

        public void Publish(object @event)
        {
            _scope.PublishEvent(@event);
        }

        public Task PublishAsync(object @event)
        {
            return _scope.PublishEventAsync(@event);
        }
    }
}
