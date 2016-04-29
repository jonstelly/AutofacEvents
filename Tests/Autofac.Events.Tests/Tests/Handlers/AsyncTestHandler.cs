using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.Events.Tests.Handlers
{
    public abstract class AsyncTestHandler<TEvent> : IHandleEventAsync<TEvent>
    {
        protected AsyncTestHandler()
        {
            Events = new List<TEvent>();
        }

        public List<TEvent> Events { get; private set; }
        public TEvent LastEvent { get { return Events.LastOrDefault(); } }

        public async Task HandleAsync(TEvent @event)
        {
            Events.Add(@event);
        }
    }
}