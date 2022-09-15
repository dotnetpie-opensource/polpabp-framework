using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PolpAbp.Extensions.MessageBird
{
    public static class ConfigurationExtension
    {
        public static void ConfigureMessageBird(this IServiceCollection services, IConfigurationSection section)
        {
            services.Configure<MessageBirdConfiguration>(section);
        }
    }
}
