using System;
using System.Collections.Generic;
using Volo.Abp.Data;

namespace PolpAbp.Framework
{
	public static class Extensions
	{
        public static T GetPropertyExt<T>(this IHasExtraProperties dataSource,
                string key, Dictionary<string, object> defaultDataSource)
        {
            try
            {
                var val = dataSource.GetProperty<T>(key);
                if (val == null)
                {
                    // Try the default data source
                    defaultDataSource.TryGetValue(key, out var defaultVal);
                    if (defaultVal != null)
                    {
                        // Convert the object to the type T
                        val = (T)Convert.ChangeType(defaultVal, typeof(T));
                    }
                }

                return val;
            }
            catch (Exception _)
            {
                return default(T);
            }
        }
    }
}

