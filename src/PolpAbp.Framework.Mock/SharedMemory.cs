using System;
using System.Collections.Generic;

namespace PolpAbp.Framework.Mock
{
    public class SharedMemory
    {
        public static SharedMemory Instance = new SharedMemory();

        public IServiceProvider ServiceProvider = null;

        public string BackgroundJobName = string.Empty;

        public List<Tuple<string, object>> ExtraProperties = new List<Tuple<string, object>>();

        public List<object> DistributedEvents = new List<object>();

        public void AddProperty(string key, object value)
        {
            ExtraProperties.Add(new Tuple<string, object>(key, value));
        }
    }
}
