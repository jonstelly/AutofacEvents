using System;
using System.Collections.Generic;
using System.Text;

namespace Autofac.Events
{
    public class EventService : IEventService
    {
        public EventService(IComponentContext context)
        {
            _Context = context;
        }

        private readonly IComponentContext _Context;

        public void Publish(object @event)
        {
            _Context.PublishEvent(@event);
        }
    }
}
