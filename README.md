AutofacEvents
=============
AutofacEvents is an event pub/sub extension for Autofac.  Primary usage is to publish domain events between classes, where subscribing to the events is as easy as implementing an interface in any interested classes and ensuring that they are registered with Autofac.  A nuget package is available as [Autofac.Events](http://www.nuget.org/packages/Autofac.Events/).

## Status
![](https://github.com/jonstelly/AutofacEvents/workflows/Autofac%20Events%20Build/badge.svg?branch=master)


## Configuration

During your normal autofac configuration, register the ContravariantRegistrationSource, and call RegisterEventing().

```csharp
var builder = new ContainerBuilder();
builder.RegisterSource(new ContravariantRegistrationSource());
builder.RegisterEventing();
```

## Publishing
There are two options for publishing.  Option one is to call the PublishEvent() extension method off of ILifetimeScope.  For those that would prefer not to have to add a dependency to ILifetimeScope to publish, there's also an IEventPublisher interface that has a simple Publish method.  Personally I prefer to use the IEventPublisher because a dependency on the IEventPublisher makes it clear what your class intends to use the dependency for, while a dependency on ILifetimeScope is less clear.

```csharp
public class SomeMessage
{
  public string Text { get; set; }
}

public class WorkDoer
{
  public WorkDoer(IEventPublisher eventPublisher)
  {
    _EventPublisher = eventPublisher;
  }
  private readonly IEventPublisher _EventPublisher;
  
  public void DoWork()
  {
    //... Do something
    _EventPublisher.Publish(new SomeMessage { Text = "We did something" });
  }
}
```

###Async
You can also publish events asynchronously as follows:

```csharp
public class SomeMessage
{
  public string Text { get; set; }
}

public class WorkDoer
{
  public WorkDoer(IAsyncEventPublisher eventPublisher)
  {
    _EventPublisher = eventPublisher;
  }
  private readonly IAsyncEventPublisher _EventPublisher;
  
  public async Task DoWorkAsync()
  {
    //... Do something
    await _EventPublisher.PublishAsync(new SomeMessage { Text = "We did something" });
  }
}
```

##Subscribing
Subscribing is done by implementing the `IHandleEvent<TEvent>` interface.  The interface has a single method,
```csharp
void Handle<TEvent>(TEvent @event)
```
that you must implement.  You then simply have to register the subscriber with Autofac, ensuring you use AsImplementedInterfaces() and events will be published accordingly.

```csharp
public class WorkListener : IHandleEvent<SomeMessage>
{
  public void Handle(SomeMessage)
  {
    //React to SomeMessage here
  }
}
```

###Async
You can also handle events asynchronously by implementing `IHandleEventAsync<TEvent>`.  This interface also has a single method,
```csharp
Task HandleAsync<TEvent>(TEvent @event)
```

```csharp
public class WorkListener : IHandleEventAsync<SomeMessage>
{
  public async Task HandleAsync(SomeMessage)
  {
    //React to SomeMessage here
  }
}
```

###Contravariance
Configuring AutoFac's ContravariantRegistrationSource initially means that if we implement `IHandleEvent<object>` or `IHandleEventAsync<object>` then we would get all events of any type.  You can also have your event types implement interfaces and subscribe to those interfaces.
