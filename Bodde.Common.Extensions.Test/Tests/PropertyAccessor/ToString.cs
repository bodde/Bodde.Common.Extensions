using Bodde.Common.Extensions;
using Bodde.Common.Extensions.Test.Models;

namespace PropertyAccessor;

public class ToString
{
    [Fact]
    public void Typed_EmployeeId()
    {
        var sut = new PropertyAccessor<Employee, int>(_ => _.Id);

        var actual = sut.ToString();

        Assert.Equal("Id", actual);
    }

    [Fact]
    public void Typed_EmployeeDepartmentName()
    {
        var sut = new PropertyAccessor<Employee, string>(_ => _.Department.Name);

        var actual = sut.ToString();

        Assert.Equal("Department.Name", actual);
    }

    [Fact]
    public void Untyped_EmployeeId()
    {
        var sut = new PropertyAccessor<Employee, object>(_ => _.Id);

        var actual = sut.ToString();

        Assert.Equal("Id", actual);
    }

    [Fact]
    public void Untyped_EmployeeDepartmentName()
    {
        var sut = new PropertyAccessor<Employee, object>(_ => _.Department.Name);

        var actual = sut.ToString();

        Assert.Equal("Department.Name", actual);
    }
}