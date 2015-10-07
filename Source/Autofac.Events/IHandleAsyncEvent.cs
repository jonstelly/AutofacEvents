using System.Threading.Tasks;

namespace Autofac.Events
{
    public interface IHandleAsyncEvent<in TEvent>
    {
        Task HandleAsync(TEvent @event);
    }
}
