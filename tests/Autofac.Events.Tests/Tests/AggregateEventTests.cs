using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Events.Tests.Handlers;
using Xunit;

namespace Autofac.Events.Tests
{
    public class AggregateEventTests : UnitTests
    {
        [Fact]
        public void GenericBaseTargetEventPublished()
        {
            using(var scope= BeginScope())
            {
                var @event = new AggregateSaved<Aggregate>(new Person());
                
                scope.PublishEvent(@event);
                
                var eventHandlerEvent = Assert.Single(scope.Resolve<AggregateEventHandler>().Events);
                var savedHandlerEvent = Assert.Single(scope.Resolve<AggregateSavedHandler>().Events);
                Assert.Equal(@event, eventHandlerEvent);
                Assert.Equal(@event, savedHandlerEvent);
            }
        }

        [Fact]
        public void GenericDerivedTargetNotPublished()
        {
            using (var scope = BeginScope())
            {
                var @event = new AggregateSaved<Person>(new Person());

                scope.PublishEvent(@event);

                Assert.Null(scope.Resolve<AggregateEventHandler>().LastEvent);
                Assert.Null(scope.Resolve<AggregateSavedHandler>().LastEvent);
                Assert.Equal(@event, scope.Resolve<PersonEventHandler>().LastEvent);
                Assert.Equal(@event, scope.Resolve<PersonSavedHandler>().LastEvent);
            }
        }

        public class AggregateEventHandler : TestHandler<AggregateEvent<Aggregate>> 
        {
        }

        public class AggregateSavedHandler : TestHandler<AggregateSaved<Aggregate>>
        {
        }

        public class PersonEventHandler : TestHandler<AggregateEvent<Person>>
        {
        }

        public class PersonSavedHandler : TestHandler<AggregateSaved<Person>>
        {
        }
    }
}
