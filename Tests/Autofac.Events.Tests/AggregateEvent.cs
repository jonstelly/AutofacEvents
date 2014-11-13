using System;
using System.Collections.Generic;
using System.Text;

namespace Autofac.Events
{
    public abstract class AggregateEvent<TAggregate>
        where TAggregate : Aggregate
    {
        protected AggregateEvent(TAggregate aggregate)
        {
            Aggregate = aggregate;
        }

        protected AggregateEvent()
        {
        }

        public TAggregate Aggregate { get; set; }
    }
}