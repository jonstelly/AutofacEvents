
using System;

namespace Autofac.Events
{
    /// <summary>
    /// A class responsible for responding to when an event handler fails.
    /// </summary>
    public interface IEventFailureHandler
    {
        void HandleFailure(ILifetimeScope scope, Exception exception);
    }
}
