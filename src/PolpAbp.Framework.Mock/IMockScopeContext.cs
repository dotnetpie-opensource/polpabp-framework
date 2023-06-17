using System;
using System.Collections.Generic;
using System.Text;

namespace PolpAbp.Framework.Mock
{
    public interface IMockScopeContext
    {
        IServiceProvider ServiceProvider { get; set; }
        List<Tuple<string, object>> ExtraProperties { get; }
        List<object> DistributedEvents { get; }
        List<string> BackgroundJobs { get; }
        Dictionary<System.Type, System.Type> Arg2HandlerMappings { get; }

        void AddProperty(string key, object value);
    }
}
