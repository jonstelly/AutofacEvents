using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.Events.Tests
{
    public class ThrowingThrowEventHandler : IHandleEvent<ThrowEvent>, IHandleAsyncEvent<ThrowAsyncEvent>
    {
        public bool ThrewException { get; private set; }

        public void Handle(ThrowEvent @event)
        {
            ThrewException = true;
            throw new InvalidOperationException("Intentional error for " + @event);
        }

        public Task HandleAsync(ThrowAsyncEvent @event)
        {
            ThrewException = true;
            throw new InvalidOperationException("Intentional error for " + @event);
        }
    }
}