#if NETSTANDARD2_0

namespace System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class NotNullWhenAttribute(bool returnValue) : Attribute
{
    public bool ReturnValue { get; } = returnValue;
}

#endif