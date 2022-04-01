using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace PolpAbp.Framework
{
    public static class ConfigureSwaggerHelper
    {
        public static void ConfigureSwaggerConventionally(this IServiceCollection services, 
            string title, string version, 
            Action<SwaggerGenOptions>? customAction = null)
        {
            services.AddSwaggerGen(
                options =>
                {
                    options.DocumentFilter<ShowInSwaggerAttribute>(); // <- ADD THIS!
                    options.SwaggerDoc(version, new OpenApiInfo { Title = title, Version = version });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                    if (customAction != null)
                    {
                        customAction(options);
                    }
                }
            );
        }

    }
}
