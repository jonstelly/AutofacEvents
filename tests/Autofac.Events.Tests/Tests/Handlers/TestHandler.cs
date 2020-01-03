using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autofac.Events.Tests.Handlers
{
    public abstract class TestHandler<TEvent> : IHandleEvent<TEvent>
    {
        protected TestHandler()
        {
            Events = new List<TEvent>();
        }

        public List<TEvent> Events { get; }
        public TEvent LastEvent => Events.LastOrDefault();

        public void Handle(TEvent @event)
        {
            Events.Add(@event);
        }
    }
}
