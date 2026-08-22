using System.Linq.Expressions;

namespace Bodde.Common.Extensions;

public static class ExpressionExtensions
{
    public static IPropertyAccessor<T, TProperty> GetPropertyAccessor<T, TProperty>(this Expression<Func<T, TProperty>> expression)
        => new PropertyAccessor<T, TProperty>(expression);
}