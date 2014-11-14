using System;
using System.Collections.Generic;
using System.Text;

namespace Autofac.Events
{
    public interface IEventPublisher
    {
        void Publish(object @event);
    }
}
