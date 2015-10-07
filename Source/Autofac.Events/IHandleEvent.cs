namespace Autofac.Events
{
    public interface IHandleEvent<in TEvent>
    {
        void Handle(TEvent @event);
    }
}
