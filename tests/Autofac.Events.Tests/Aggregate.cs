using System;
using System.Collections.Generic;
using System.Text;

namespace Autofac.Events
{
    public abstract class Aggregate
    {
        protected Aggregate()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}