using System;
using System.Threading.Tasks;

namespace Autofac.Events
{
    /// <summary>
    /// Defines behavior for responding, in an asynchronous manner, to when an event handler fails.
    /// </summary>
    public interface IAsyncEventFailureHandler
    {
        /// <summary>
        /// Handles a failed event handler.
        /// </summary>
        /// <param name="scope">The current lifetime scope.</param>
        /// <param name="exception">The exception that occurred during the handling of the event.</param>
        Task HandleFailure(ILifetimeScope scope, Exception exception);
    }
}
