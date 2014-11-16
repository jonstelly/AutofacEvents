using System;
using System.Collections.Generic;
using System.Text;

namespace Autofac.Events.Tests
{
    public class ThrowingThrowEventHandler : IHandleEvent<ThrowEvent>
    {
        public bool ThrewException { get; private set; }

        public void Handle(ThrowEvent @event)
        {
            ThrewException = true;
            throw new InvalidOperationException("Intentional error for " + @event);
        }
    }
}