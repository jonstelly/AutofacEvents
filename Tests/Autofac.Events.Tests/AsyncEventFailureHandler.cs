
using System;
using System.Threading.Tasks;

namespace Autofac.Events
{
    public class AsyncEventFailureHandler : IAsyncEventFailureHandler
    {
        private readonly Action<ILifetimeScope, Exception> _verificationCallback;

        public AsyncEventFailureHandler(Action<ILifetimeScope, Exception> verificationCallback)
        {
            if (verificationCallback == null)
                throw new ArgumentNullException(nameof(verificationCallback));

            _verificationCallback = verificationCallback;
        }

        public async Task HandleFailure(ILifetimeScope scope, Exception exception)
        {
            await Task.Yield();

            _verificationCallback(scope, exception);
            WasCalled = true;
        }

        public bool WasCalled { get; private set; }
    }
}
