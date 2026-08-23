using System.Linq.Expressions;

namespace Bodde.Common.Extensions;

internal class PropertyAccessor<T, TProperty>(Expression<Func<T, TProperty>> expression)
{
    private readonly Lazy<MemberExpression[]> memberExpressions = new(() => GetPropertyExpressions(expression));
    private readonly Lazy<Func<T, TProperty>> getter = new(() => AddTestForNull(expression).Compile());

    public string Path => memberExpressions.Value.ToCsv(me => me.Member.Name, ".");

    public Type Type => memberExpressions.Value.Last().Type;

    public TProperty? GetValue(T instance) => instance == null ? default : getter.Value(instance);

    /// <summary>
    /// Returns the selected property path.
    /// </summary>
    /// <returns>The dot-separated property path.</returns>
    public override string ToString() => Path;

    private static MemberExpression[] GetPropertyExpressions(Expression<Func<T, TProperty>> expression)
    {
        var members = new List<MemberExpression>();
        Expression? current = expression.Body;

        current = UnwrapConversions(current);

        while (current is MemberExpression member)
        {
            members.Add(member);
            current = UnwrapConversions(member.Expression);
        }

        if (members.Count == 0 || current is not ParameterExpression)
            throw new InvalidOperationException("Expression is not a property access expression");

        members.Reverse();
        return members.ToArray();
    }

    private static Expression UnwrapConversions(Expression current)
    {
        while (current is UnaryExpression unary &&
               (unary.NodeType == ExpressionType.Convert ||
                unary.NodeType == ExpressionType.ConvertChecked))
        {
            current = unary.Operand;
        }

        return current;
    }

    private static TExpression AddTestForNull<TExpression>(TExpression expression)
        where TExpression : Expression
    {
        return (TExpression)new NullTestVisitor().Visit(expression)!;
    }

    /// <summary>
    /// ExpressionVisitor to replace a obj.member Expression with a null test ((obj == null) ? null : obj.member)
    /// </summary>
    private class NullTestVisitor : ExpressionVisitor
    {
        protected override Expression VisitMember(MemberExpression node)
        {
            var expression = Visit(node.Expression);
            var member = node.Update(expression);

            if (expression != null && node.Type.IsNullable())
            {
                var nullTestExpression = Expression.MakeBinary(ExpressionType.Equal, expression, Expression.Constant(null, expression.Type));

                return Expression.Condition(nullTestExpression, Expression.Constant(null, node.Type), member);
            }

            return member;
        }
    }
}