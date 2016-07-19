
using System;
using System.Threading.Tasks;

namespace Autofac.Events
{
    public class AsyncEventFailureHandler : IAsyncEventFailureHandler
    {
        public async Task HandleFailure(ILifetimeScope scope, Exception exception)
        {
            await Task.Yield();
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
