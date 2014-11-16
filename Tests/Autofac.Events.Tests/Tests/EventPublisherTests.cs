using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Autofac.Events.Tests
{
    public class EventPublisherTests : UnitTests
    {
        [Fact]
        public void EventPublisherPublishes()
        {
            using (var scope = BeginScope())
            {
                var start = new StartEvent();
                var stop = new StopEvent();
                var pub = scope.Resolve<IEventPublisher>();
                pub.Publish(start);
                pub.Publish(stop);
                var handler = scope.Resolve<InfrastructureEventHandler>();
                Assert.Equal(2, handler.Events.Count);
                Assert.Equal(start, handler.Events[0]);
                Assert.Equal(stop, handler.Events[1]);
            }
        }

        [Fact]
        public void ThrowingHandlerThrowsAndAllHandlersAreCalled()
        {
            using (var scope = BeginScope())
            {
                var thrower = scope.Resolve<ThrowingThrowEventHandler>();
                var handler = scope.Resolve<NonThrowingThrowEventHandler>();
                var exception = Assert.Throws<AggregateException>(() => scope.PublishEvent(new ThrowEvent()));
                Assert.Equal(1, handler.Events.Count);
                Assert.True(thrower.ThrewException);
                Assert.Equal(1, exception.InnerExceptions.Count);
                Assert.True(exception.InnerExceptions[0].Message.StartsWith("Intentional error for "));
            }
        }

    }
}

