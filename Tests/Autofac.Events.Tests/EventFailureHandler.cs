
using System;

namespace Autofac.Events
{
    public class EventFailureHandler : IEventFailureHandler
    {

        public void HandleFailure(ILifetimeScope scope, Exception exception)
        {
            WasCalled = true;
            Exception = exception;
            Scope = scope;
        }

        //Using properties as passing in a callback via the constructor 
        //wasn't possible given the way the BeginScope registers all types in the assembly.
        public bool WasCalled { get; private set; }
        public Exception Exception { get; private set; }
        public ILifetimeScope Scope { get; private set; }
    }
}
