public static class TypeExtensions
{
    public static bool IsNullable(this Type type)
        => type.IsClass || Nullable.GetUnderlyingType(type) != null;

    public static bool IsNumeric(this Type type)
    {
        var nonNullableType = Nullable.GetUnderlyingType(type) ?? type;

        return NumericTypes.Contains(nonNullableType);
    }

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