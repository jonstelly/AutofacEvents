using System;
using System.Collections.Generic;
using System.Text;

namespace Autofac.Events
{
    public interface IEventService
    {
        void Publish(object @event);
    }
}
