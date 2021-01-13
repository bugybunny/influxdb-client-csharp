using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace InfluxDB.Client.Core.Flux.Internal
{
    public class AttributesCache
    {
        // Reflection results are cached for poco type property and attribute lookups as an optimization since
        // calls are invoked continuously for a given type and will not change over library lifetime
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> PropertyCache =
            new ConcurrentDictionary<Type, PropertyInfo[]>();

        private static readonly ConcurrentDictionary<PropertyInfo, Column> AttributeCache =
            new ConcurrentDictionary<PropertyInfo, Column>();

        public PropertyInfo[] GetProperties(Type type)
        {
            Arguments.CheckNotNull(type, nameof(type));

            return PropertyCache.GetOrAdd(type, _ => type.GetProperties());
        }
        public Column GetAttribute(PropertyInfo property)
        {
            Arguments.CheckNotNull(property, nameof(property));

            return AttributeCache.GetOrAdd(property, _ =>
            {
                var attributes = property.GetCustomAttributes(typeof(Column), false);
                return attributes.Length > 0 ? attributes[0] as Column : null;
            });
        }

        public string GetColumnName(PropertyInfo property)
        {
            return GetColumnName(GetAttribute(property), property);
        }
        
        public string GetColumnName(Column attribute, PropertyInfo property)
        {
            Arguments.CheckNotNull(property, nameof(property));

            if (attribute != null && !string.IsNullOrEmpty(attribute.Name))
            {
                return attribute.Name;
            }

            return property.Name;
        }
    }
}