using System.Linq.Expressions;

namespace Bodde.Common.Extensions.Test.Helpers;

internal static class ExpressionBuilder
{

    public static Expression<Func<T, TProperty>> TypedProperty<T, TProperty>(Expression<Func<T, TProperty>> expression)
        => expression;

    public static Expression<Func<T, object>> UntypedProperty<T>(Expression<Func<T, object>> expression)
        => expression;
}
