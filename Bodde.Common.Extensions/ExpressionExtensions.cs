using System.Linq.Expressions;

namespace Bodde.Common.Extensions;

public static class ExpressionExtensions
{
    /// <summary>
    /// Creates an accessor for a property selected by the expression.
    /// </summary>
    /// <typeparam name="T">The type containing the property.</typeparam>
    /// <typeparam name="TProperty">The type of the selected property.</typeparam>
    /// <param name="expression">The expression that selects the property.</param>
    /// <returns>An accessor for the selected property.</returns>
    public static IPropertyAccessor<T, TProperty> GetPropertyAccessor<T, TProperty>(this Expression<Func<T, TProperty>> expression)
        => new PropertyAccessor<T, TProperty>(expression);
}