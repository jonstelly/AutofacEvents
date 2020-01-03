using System;
using System.Collections.Generic;
using System.Text;

namespace Autofac.Events
{
    /// <summary>
    /// Publishes an event
    /// </summary>
    public interface IEventPublisher
    {
        void Publish(object @event);
    }
}
