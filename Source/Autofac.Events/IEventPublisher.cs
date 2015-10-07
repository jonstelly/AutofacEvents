namespace Autofac.Events
{
    public interface IEventPublisher
    {
        void Publish(object @event);
    }
}
