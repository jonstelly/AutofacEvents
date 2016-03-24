using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Autofac.Events.Tests
{
    public class AsyncEventPublisherTests : UnitTests
    {
        [Fact]
        public async Task AsyncEventPublisherPublishes()
        {
            using (var scope = BeginScope())
            {
                var start = new StartEvent();
                var stop = new StopEvent();
                var pub = scope.Resolve<IEventPublisher>();
                var handler = scope.Resolve<InfrastructureEventHandler>();
                var asyncHandler = scope.Resolve<InfrastructureAsyncEventHandler>();
                Assert.Equal(0, handler.Events.Count);
                Assert.Equal(0, asyncHandler.Events.Count);
                await pub.PublishAsync(start);
                await pub.PublishAsync(stop);
                
                Assert.Equal(2, handler.Events.Count);
                Assert.Equal(start, handler.Events[0]);
                Assert.Equal(stop, handler.Events[1]);
                Assert.Equal(2, asyncHandler.Events.Count);
                Assert.Equal(start, asyncHandler.Events[0]);
                Assert.Equal(stop, asyncHandler.Events[1]);
            }
        }

        [Fact]
        public void EventPublisherPublishesAsyncHandlerHandles()
        {
            using (var scope = BeginScope())
            {
                var start = new StartEvent();
                var stop = new StopEvent();
                var pub = scope.Resolve<IEventPublisher>();
                var asyncHandler = scope.Resolve<InfrastructureAsyncEventHandler>();
                Assert.Equal(0, asyncHandler.Events.Count);
                pub.Publish(start);
                pub.Publish(stop);

                Assert.Equal(2, asyncHandler.Events.Count);
                Assert.Equal(start, asyncHandler.Events[0]);
                Assert.Equal(stop, asyncHandler.Events[1]);
            }
        }
    }
}
