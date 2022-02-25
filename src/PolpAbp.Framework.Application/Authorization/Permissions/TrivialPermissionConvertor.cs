using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Authorization.Permissions
{
    /// <summary>
    /// Provides the default implementation (identity mapping) for IPermissionConvertor.
    /// </summary>
    public class TrivialPermissionConvertor : IPermissionConvertor, ISingletonDependency
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
