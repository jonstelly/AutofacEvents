using System;
using System.Threading.Tasks;

namespace Autofac.Events
{
    /// <summary>
    /// A class responsible for responding, in an asynchronous manner, to when an event handler fails.
    /// </summary>
    public interface IAsyncEventFailureHandler
    {
        Task HandleFailure(ILifetimeScope scope, Exception exception);
    }
}
