
using System;

namespace Autofac.Events
{
    public class EventFailureHandler : IEventFailureHandler
    {
        private readonly Action<ILifetimeScope, Exception> _verificationCallback;

        public EventFailureHandler(Action<ILifetimeScope, Exception> verificationCallback)
        {
            if (verificationCallback == null)
                throw new ArgumentNullException(nameof(verificationCallback));

            _verificationCallback = verificationCallback;

        }
        public void HandleFailure(ILifetimeScope scope, Exception exception)
        {
            _verificationCallback(scope, exception);
            WasCalled = true;
        }

        public bool WasCalled { get; private set; }
    }
}
