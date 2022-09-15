using Microsoft.Extensions.Configuration;

namespace PolpAbp.Extensions.MessageBird
{
    public static class ConfigurationExtension
    {
        public static void ConfigureMessageBird(this IConfiguration configuration, IConfigurationSection section)
        {
            configuration.Bind(section);
        }
    }
}
