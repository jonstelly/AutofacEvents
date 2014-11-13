using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.Events.Tests
{
    public abstract class TestHandler<TEvent> : IHandleEvent<TEvent>
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
    }
}
