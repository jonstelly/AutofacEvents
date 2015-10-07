using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.Events
{
    public interface IAsyncEventPublisher
    {
        Task Publish(object @event);
    }
}
