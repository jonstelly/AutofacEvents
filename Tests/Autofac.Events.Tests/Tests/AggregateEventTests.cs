using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                var aggregateEventHandler = scope.Resolve<AggregateEventHandler>();
                var aggregateSavedHandler = scope.Resolve<AggregateSavedHandler>();
                Assert.NotNull(aggregateEventHandler.LastEvent);
                Assert.NotNull(aggregateSavedHandler.LastEvent);
            }
        }

        [Fact]
        public void GenericDerivedTargetNotPublished()
        {
            using (var scope = BeginScope())
            {
                var @event = new AggregateSaved<Person>(new Person());
                scope.PublishEvent(@event);
                var aggregateEvent = scope.Resolve<AggregateEventHandler>();
                var aggregateSaved = scope.Resolve<AggregateSavedHandler>();
                var personEvent = scope.Resolve<PersonEventHandler>();
                var personSaved = scope.Resolve<PersonSavedHandler>();
                Assert.Null(aggregateEvent.LastEvent);
                Assert.Null(aggregateSaved.LastEvent);
                Assert.NotNull(personEvent.LastEvent);
                Assert.NotNull(personSaved.LastEvent);
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
