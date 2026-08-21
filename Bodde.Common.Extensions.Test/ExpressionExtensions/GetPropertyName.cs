using System.Linq.Expressions;

namespace Bodde.Common.Extensions.Test.ExpressionExtensions;

public class GetPropertyName
{
    [Fact]
    public void EmployeeId()
    {
        var sut = CreateExpression<Employee>(_ => _.Id);
        var actual = sut.GetPropertyName();

        Assert.Equal("Id", actual);
    }

    [Fact]
    public void EmployeeName()
    {
        var sut = CreateExpression<Employee>(_ => _.Name);
        var actual = sut.GetPropertyName();

        Assert.Equal("Name", actual);
    }

    [Fact]
    public void EmployeeDepartmentName()
    {
        var sut = CreateExpression<Employee>(_ => _.Department.Name);
        var actual = sut.GetPropertyName();

        Assert.Equal("Department.Name", actual);
    }


    private static Expression<Func<Employee, object>> CreateExpression<T>(Expression<Func<Employee, object>> expression) 
        => expression;

    
    private record Department(int Id, string Name)
    {
    }
    private record Employee(int Id, string Name, string Surname, int Age, Department Department)
    {
    }
}