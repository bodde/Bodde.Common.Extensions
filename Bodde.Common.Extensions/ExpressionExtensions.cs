using System.Linq.Expressions;
using Bodde.Common.Extensions;

public static class ExpressionExtensions
{

    public static string GetPropertyName<T, TProperty>(this Expression<Func<T, TProperty>> expression)
    {
        var memberExpressions = GetMemberExpressions(expression);

        return memberExpressions.ToCsv(me => me.Member.Name, ".");
    }

    public static Type GetPropertyType<T>(this Expression<Func<T, object>> expression)
    {
        var memberExpressions = GetMemberExpressions(expression);

        return memberExpressions.Last().Type;
    }

    public static ExpressionType? AddTestForNull<ExpressionType>(this ExpressionType orig) 
        where ExpressionType : Expression 
        => (ExpressionType?)new NullTestVisitor().Visit(orig);

    private static MemberExpression[] GetMemberExpressions<T, TProperty>(Expression<Func<T, TProperty>> expression)
    {
        if (expression.Body is UnaryExpression unaryExpression)
        {
            return [(MemberExpression)unaryExpression.Operand];
        }

        if (expression.Body is MemberExpression memberExpression)
        {
            var memberExpressions = new List<MemberExpression>();
            var innerExpression = memberExpression.Expression as MemberExpression;
            while (innerExpression != null)
            {
                memberExpressions.Add(innerExpression);
                innerExpression = innerExpression.Expression as MemberExpression;
            }

            memberExpressions.Add(memberExpression);

            return memberExpressions.ToArray();
        }

        throw new ArgumentException("Expression is not a property access expression");
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