using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Settings
{
    /// <summary>
    /// Provides the default implementation (identity mapping).
    /// </summary>
    public class TrivialSettingConvertor : ISettingConvertor, ISingletonDependency
    {
        public string Encode(string input)
        {
            return input;
        }

        public string Decode(string input)
        {
            return input;
        }
    }
}
