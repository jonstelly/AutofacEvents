using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Autofac.Events.Tests
{
    public class InfrastructureEventTests : UnitTests
    {
        [Fact]
        public void AllInfrastructureEventsAreReceived()
        {
            using (var scope = BeginScope())
            {
                var @event = new DebugEvent()
                {
                    Message = "Test"
                };
                scope.PublishEvent(@event);
                var handler = scope.Resolve<InfrastructureEventHandler>();
                Assert.Equal(@event, handler.LastEvent);
            }
        }
    }
}
