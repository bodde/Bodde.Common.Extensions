using System.Collections.Concurrent;
using System.Reflection;

public static class TypeExtensions
{
    extension(Type type)
    {
        public bool IsNullable()
            => type.IsClass || Nullable.GetUnderlyingType(type) != null;

        public bool IsNumeric()
        {
            var nonNullableType = Nullable.GetUnderlyingType(type) ?? type;

            return NumericTypes.Contains(nonNullableType);
        }

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