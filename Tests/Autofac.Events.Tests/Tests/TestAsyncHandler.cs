using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.Events.Tests
{
    public class TestAsyncHandler<TEvent> : IHandleAsyncEvent<TEvent>
    {
        protected TestAsyncHandler()
        {
            Events = new List<TEvent>();
        }

        public List<TEvent> Events { get; private set; }
        public TEvent LastEvent { get { return Events.LastOrDefault(); } }


        public Task HandleAsync(TEvent @event)
        {
            Events.Add(@event);
            return Task.FromResult((object) null);
        }
    }
}
