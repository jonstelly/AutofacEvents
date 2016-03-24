using System.Threading.Tasks;

namespace Autofac.Events
{
    public interface IEventPublisher
    {
        void Publish(object @event);
        Task PublishAsync(object @event);
    }
}
