using Bodde.Common.Extensions.Test.Models;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

namespace Bodde.Common.Extensions.Test.Tests.PropertyAccessor;

public class Path
{
    [Fact]
    public void Typed_EmployeeId()
    {
        var sut = new PropertyAccessor<Employee, int>(_ => _.Id);

        var actual = sut.Path;

        Assert.Equal("Id", actual);
    }

    [Fact]
    public void Typed_EmployeeDepartmentName()
    {
        var sut = new PropertyAccessor<Employee, string>(_ => _.Department.Name);

        var actual = sut.Path;

        Assert.Equal("Department.Name", actual);
    }

    [Fact]
    public void Untyped_EmployeeId()
    {
        var sut = new PropertyAccessor<Employee, object>(_ => _.Id);

        var actual = sut.Path;

        Assert.Equal("Id", actual);
    }

    [Fact]
    public void Untyped_EmployeeDepartmentName()
    {
        var sut = new PropertyAccessor<Employee, object>(_ => _.Department.Name);

        var actual = sut.Path;

        Assert.Equal("Department.Name", actual);
    }

    [Fact]
    public void CheckedConversion_EmployeeId()
    {
        var sut = new PropertyAccessor<Employee, short>(employee => checked((short)employee.Id));

        var actual = sut.Path;

        Assert.Equal("Id", actual);
    }

    [Fact]
    public void Expression_Not_Property_Accessor_Throw()
    {
        var sut = new PropertyAccessor<Employee, bool>(employee => employee.Id == 1);

        Assert.Throws<InvalidOperationException>(() => sut.Path);

    }
}