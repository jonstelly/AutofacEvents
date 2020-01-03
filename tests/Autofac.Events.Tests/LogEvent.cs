using System;
using System.Collections.Generic;
using System.Text;

namespace Autofac.Events
{
    public abstract class LogEvent : InfrastructureEvent
    {
        public string Message { get; set; }
    }
}