using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Events.Tests.Handlers;
using Xunit;

namespace Autofac.Events.Tests
{
    public class HandlerLocationTests : UnitTests
    {
        public class BaseEvent { }
        public class DerivedEvent : BaseEvent { }

        public class BaseEventHandler : TestHandler<BaseEvent> { }
        public class DerivedEventHandler : TestHandler<DerivedEvent> { }

        [Fact]
        public void BaseEventBaseHandlesDerivedDoesNotHandle()
        {
            using (var scope = BeginScope())
            {
                var @event = new BaseEvent();

                scope.PublishEvent(@event);

                var baseEvent = Assert.Single(scope.Resolve<BaseEventHandler>().Events);
                Assert.Equal(@event, baseEvent);

                Assert.Empty(scope.Resolve<DerivedEventHandler>().Events);
            }
        }

        [Fact]
        public void DerivedEventBaseHandlesDerivedHandles()
        {
            using (var scope = BeginScope())
            {
                var @event = new DerivedEvent();

                scope.PublishEvent(@event);

                var baseEvent = Assert.Single(scope.Resolve<BaseEventHandler>().Events);
                Assert.Equal(@event, baseEvent);

                var derivedEvent = Assert.Single(scope.Resolve<DerivedEventHandler>().Events);
                Assert.Equal(@event, derivedEvent);
            }
        }
    }
}