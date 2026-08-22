using System.Linq.Expressions;

namespace Bodde.Common.Extensions;


public interface IPropertyAccessor<T, TProperty>
{
    string Path { get; }

    Type Type { get; }

    TProperty? GetValue(T instance);
}

internal class PropertyAccessor<T, TProperty>(Expression<Func<T, TProperty>> expression) : IPropertyAccessor<T, TProperty>
{
    private readonly Expression<Func<T, TProperty>> expression = expression;
    private readonly Lazy<MemberExpression[]> memberExpressions = new(() => GetPropertyExpressions(expression));
    private readonly Lazy<Func<T, TProperty>> getter = new(() => AddTestForNull(expression).Compile())
;

    public string Path => memberExpressions.Value.ToCsv(me => me.Member.Name, ".");

    public Type Type => memberExpressions.Value.Last().Type;

    public TProperty? GetValue(T instance) => instance == null ? default : getter.Value(instance);

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
        public override Expression? Visit(Expression? node)
        {
            if (node is MemberExpression nme && nme.Expression != null && nme.Type.IsNullable())
            {
                var nullTestExpression = Expression.MakeBinary(ExpressionType.Equal, nme.Expression, Expression.Constant(null, nme.Expression.Type));

                return Expression.Condition(nullTestExpression, Expression.Constant(null, nme.Type), nme);
            }

            return base.Visit(node);
        }
    }
}