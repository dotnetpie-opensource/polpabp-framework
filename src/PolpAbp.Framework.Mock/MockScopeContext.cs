using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Mock
{
    public class MockScopeContext : IMockScopeContext, IScopedDependency
    {
        public IServiceProvider ServiceProvider { get; set; }

        public List<Tuple<string, object>> ExtraProperties { get; }

        public List<object> DistributedEvents { get; }

        public List<string> BackgroundJobs { get; }

        public Dictionary<System.Type, System.Type> Arg2HandlerMappings { get; }


        public MockScopeContext()
        {
            BackgroundJobs = new List<string>();
            DistributedEvents = new List<object>();
            ExtraProperties = new List<Tuple<string, object>>();
            Arg2HandlerMappings = new Dictionary<Type, Type>();
        }

        public void AddProperty(string key, object value)
        {
            ExtraProperties.Add(new Tuple<string, object>(key, value));
        }
    }
}
