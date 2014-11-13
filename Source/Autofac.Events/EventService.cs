using System;
using System.Collections.Generic;
using System.Text;

namespace Autofac.Events
{
    public class EventService : IEventService
    {
        public EventService(ILifetimeScope scope)
        {
            _scope = scope;
        }

        private readonly ILifetimeScope _scope;

        public void Publish(object @event)
        {
            _scope.PublishEvent(@event);
        }
    }
}
