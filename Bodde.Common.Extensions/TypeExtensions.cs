    public static class TypeExtensions
    {
        public static bool IsNullable(this Type type)
            => type.IsClass || Nullable.GetUnderlyingType(type) != null;
    }