using System;
using System.Collections.Generic;
using System.Reflection;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;

namespace AutoMapper
{
    public static class AutoMapperExtensions
    {
        /// <summary>
        /// Uses it safely.
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TDestination">Dst type</typeparam>
        /// <param name="mappingExpression">Expression</param>
        /// <param name="definitionChecks">Check</param>
        /// <param name="ignoredProperties">todo</param>
        /// <param name="mapToRegularProperties">dodo</param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination> SafelyMapExtraProperties<TSource, TDestination>(
               this IMappingExpression<TSource, TDestination> mappingExpression,
               MappingPropertyDefinitionChecks? definitionChecks = null,
               string[] ignoredProperties = null,
               bool mapToRegularProperties = false)
               where TDestination : IHasExtraProperties
               where TSource : IHasExtraProperties
        {
            return mappingExpression
                .ForMember(
                    x => x.ExtraProperties,
                    y => y.MapFrom(
                        (source, destination, extraProps) =>
                        {
                            var result = extraProps.IsNullOrEmpty()
                                ? new Dictionary<string, object>()
                                : new Dictionary<string, object>(extraProps);

                            if (source.ExtraProperties != null)
                            {
                                foreach(var keyValue in source.ExtraProperties)
                                {
                                    result[keyValue.Key] = keyValue.Value;
                                }
                            }

                            return result;
                        })
                );
        }

        /// <summary>
        /// Configure a map to ignore the non-existing properties in the source entry.
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TDestination">Target type</typeparam>
        /// <param name="expression">Mapping expression</param>
        /// <returns>Mapping extension</returns>
        public static IMappingExpression<TSource, TDestination> IgnoreSourceMissingProperties<TSource, TDestination>
            (this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties(flags);

            foreach (var property in destinationProperties)
            {
                if (sourceType.GetProperty(property.Name, flags) == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;
        }
    }
}
