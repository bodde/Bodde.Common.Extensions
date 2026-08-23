using System.Collections.Concurrent;
using System.Reflection;

public static class TypeExtensions
{
    extension(Type type)
    {
        /// <summary>
        /// Determines whether the type can contain a null value.
        /// </summary>
        /// <returns><see langword="true"/> if the type is a reference type or a nullable value type; otherwise, <see langword="false"/>.</returns>
        public bool IsNullable()
            => type.IsClass || Nullable.GetUnderlyingType(type) != null;

        /// <summary>
        /// Determines whether the type represents a numeric value.
        /// </summary>
        /// <returns><see langword="true"/> if the type is a supported numeric type; otherwise, <see langword="false"/>.</returns>
        public bool IsNumeric()
        {
            var nonNullableType = Nullable.GetUnderlyingType(type) ?? type;

            return NumericTypes.Contains(nonNullableType);
        }

        /// <summary>
        /// Gets the properties that match the specified binding flags.
        /// </summary>
        /// <param name="useCache">Indicates whether cached property information may be used.</param>
        /// <param name="flags">The binding flags used to find properties.</param>
        /// <returns>An array containing the matching property information.</returns>
        public PropertyInfo[] GetPropertyInfos(
            bool useCache = true,
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty
            )
        {
            if(!useCache)
            {
                return GetPropertyInfos(type, flags);
            }

            var key = $"{type}_{flags}";
            return PropertiesInfosCache.GetOrAdd(key, _ => GetPropertyInfos(type, flags));
        }


        /// <summary>
        /// Gets the names of the properties that match the specified binding flags.
        /// </summary>
        /// <param name="useCache">Indicates whether cached property information may be used.</param>
        /// <param name="flags">The binding flags used to find properties.</param>
        /// <returns>An array containing the matching property names.</returns>
        public string[] GetPropertyNames(
            bool useCache = true,
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty
            )
        {
            var propertyInfos = type.GetPropertyInfos(useCache, flags);

            return propertyInfos
                .Select(_ => _.Name)
                .ToArray();
        }
    }

    private static PropertyInfo[] GetPropertyInfos(Type type, BindingFlags flags)
    {
        return type
            .GetProperties(flags)
            .ToArray();
    }

    private static readonly ConcurrentDictionary<string, PropertyInfo[]> PropertiesInfosCache = new();

    private static readonly Type[] NumericTypes = [
        typeof(sbyte),
        typeof(byte),
        typeof(short),
        typeof(ushort),
        typeof(int),
        typeof(uint),
        typeof(long),
        typeof(ulong),
        typeof(float),
        typeof(double),
        typeof(decimal)
    ];
}