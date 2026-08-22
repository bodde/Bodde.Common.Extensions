using Bodde.Common.Extensions.Test.Models;

namespace Bodde.Common.Extensions.Test.Tests.PropertyAccessor;

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
        var employee = CreateManager();

        var actual = sut.GetValue(employee);

        Assert.Equal(employee.Manager?.Manager?.Name, actual);
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