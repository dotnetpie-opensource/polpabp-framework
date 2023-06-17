using System;
using System.Collections.Generic;

namespace PolpAbp.Framework.Mock
{
    public class SharedMemory
    {
        public static SharedMemory Data = new SharedMemory();

        public IServiceProvider ServiceProvider = null;

        public string BackgroundJobName = string.Empty;

        public Dictionary<string, object> ExtraProperties = new Dictionary<string, object>();

        public List<object> DistributedEvents = new List<object>();
    }
}
