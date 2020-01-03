using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.Events
{
    /// <summary>
    /// Publishes an event asynchronously
    /// </summary>
    public interface IAsyncEventPublisher
    {
        Task PublishAsync(object @event);
    }
}