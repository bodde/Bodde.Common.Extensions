using Bodde.Common.Extensions;
using Bodde.Common.Extensions.Test.Models;

namespace PropertyAccessor;

public class GetValue
{
    [Fact]
    public void Typed_EmployeeId()
    {
        var sut = new PropertyAccessor<Employee, int>(_ => _.Id);
        var employee = CreateEmployee();

        var actual = sut.GetValue(employee);

        Assert.Equal(employee.Id, actual);
    }

    [Fact]
    public void Typed_EmployeeDepartmentName()
    {
        var sut = new PropertyAccessor<Employee, string>(_ => _.Department.Name);
        var employee = CreateEmployee();

        var actual = sut.GetValue(employee);

        Assert.Equal(employee.Department.Name, actual);
    }

    [Fact]
    public void Untyped_EmployeeId()
    {
        var sut = new PropertyAccessor<Employee, object>(_ => _.Id);
        var employee = CreateEmployee();

        var actual = sut.GetValue(employee);

        Assert.Equal(employee.Id, actual);
    }

    [Fact]
    public void Untyped_EmployeeManagerName()
    {
        var sut = new PropertyAccessor<Employee, object>(_ => _.Manager!.Name);
        var employee = CreateEmployee();

        var actual = sut.GetValue(employee);

        Assert.Equal(employee.Manager?.Name, actual);
    }   

    [Fact]
    public void Untyped_EmployeeManagerManagerName()
    {
        var sut = new PropertyAccessor<Employee, object>(_ => _.Manager!.Manager!.Name);
        var employee = CreateEmployee();

        var actual = sut.GetValue(employee);

        Assert.Null(actual);
    }   

       [Fact]
    public void Untyped_ManagerManagerName()
    {
        var sut = new PropertyAccessor<Employee, object>(_ => _.Manager!.Name);
        var employee = CreateManager();

        var actual = sut.GetValue(employee);

        Assert.Null(actual);
    }   


       [Fact]
    public void Untyped_ManagerManagerManagerName()
    {
        var sut = new PropertyAccessor<Employee, object>(_ => _.Manager!.Manager!.Name);
        var employee = CreateManager();

        var actual = sut.GetValue(employee);

        Assert.Null(actual);
    }   


    private static Employee CreateManager()
    {
        return new Employee(
            Id: 1,
            Name: "John",
            Surname: "Smith",
            Age: 35,
            Department: new(Id: 1, Name: "Engineering"),
            Manager: null
        );
    } 
    
    private static Employee CreateEmployee()
    {
        return new Employee(
            Id: 2,
            Name: "Mario",
            Surname: "Rossi",
            Age: 23,
            Department: new(Id: 1, Name: "Engineering"),
            Manager: CreateManager()
        );
    }
}