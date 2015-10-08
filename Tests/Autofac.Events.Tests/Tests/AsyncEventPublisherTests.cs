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
                var pub = scope.Resolve<IAsyncEventPublisher>();
                var handler = scope.Resolve<InfrastructureEventHandler>();
                Assert.Equal(0, handler.Events.Count);
                await pub.Publish(start);
                await pub.Publish(stop);
                
                Assert.Equal(2, handler.Events.Count);
                Assert.Equal(start, handler.Events[0]);
                Assert.Equal(stop, handler.Events[1]);
            }
        }
    }
}
