using Bodde.Common.Extensions.Test.Helpers;
using Bodde.Common.Extensions.Test.Models;

namespace Bodde.Common.Extensions.Test.Tests.ExpressionExtensions;

public class GetPropertyAccessor
{
    [Fact]
    public void Typed_EmployeeId()
    {
        var sut = ExpressionBuilder.TypedProperty<Employee, int>(_ => _.Id);
        var actual = sut.GetPropertyAccessor();

        Assert.Equal("Id", actual.Path);
        Assert.Equal(typeof(int), actual.Type);
    }

    [Fact]
    public void Typed_EmployeeName()
    {
        var sut = ExpressionBuilder.TypedProperty<Employee, string>(_ => _.Name);
        var actual = sut.GetPropertyAccessor();

        Assert.Equal("Name", actual.Path);
        Assert.Equal(typeof(string), actual.Type);
    }

    [Fact]
    public void Typed_EmployeeDepartmentName()
    {
        var sut = ExpressionBuilder.TypedProperty<Employee, string>(_ => _.Department.Name);
        var actual = sut.GetPropertyAccessor();

        Assert.Equal("Department.Name", actual.Path);
        Assert.Equal(typeof(string), actual.Type);
    }

    [Fact]
    public void Untyped_EmployeeId()
    {
        var sut = ExpressionBuilder.UntypedProperty<Employee>(_ => _.Id);
        var actual = sut.GetPropertyAccessor();

        Assert.Equal("Id", actual.Path);
        Assert.Equal(typeof(int), actual.Type);
    }

    [Fact]
    public void Untyped_EmployeeName()
    {
        var sut = ExpressionBuilder.UntypedProperty<Employee>(_ => _.Name);
        var actual = sut.GetPropertyAccessor();

        Assert.Equal("Name", actual.Path);
        Assert.Equal(typeof(string), actual.Type);
    }

    [Fact]
    public void Untyped_EmployeeDepartmentName()
    {
        var sut = ExpressionBuilder.UntypedProperty<Employee>(_ => _.Department.Name);
        var actual = sut.GetPropertyAccessor();

        Assert.Equal("Department.Name", actual.Path);
        Assert.Equal(typeof(string), actual.Type);
    }

}