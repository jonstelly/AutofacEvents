using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.Events.Tests
{
    public abstract class TestHandler<TEvent> : IHandleEvent<TEvent>, IHandleAsyncEvent<TEvent>
    {
        protected TestHandler()
        {
            Events = new List<TEvent>();
        }

        public List<TEvent> Events { get; private set; }
        public TEvent LastEvent { get { return Events.LastOrDefault(); } }

        public void Handle(TEvent @event)
        {
            Events.Add(@event);
        }

        public Task HandleAsync(TEvent @event)
        {
            Events.Add(@event);
            return Task.Delay(0);
        }
    }
}
