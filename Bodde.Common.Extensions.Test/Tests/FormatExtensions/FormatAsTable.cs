using Bodde.Common.Extensions;
using Bodde.Common.Extensions.Test.Models;

namespace FormatExtensions;

public class FormatAsTable
{
    [Fact]
    public void Employees_Empty()
    {
        Employee[] sut = [];

        var actual = sut.FormatAsTable();

        string expected = CreateExpected([
            "Id  Name  Surname  Age  Department  Manager  ",
            "---------------------------------------------",
            "No data to display"
        ]);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Employees_NotEmpty()
    {
        var sut = CreateEmployees();

        var actual = sut.FormatAsTable();

        string expected = CreateExpected([
            "Id  Name   Surname  Age  Department       Manager         ",
            "----------------------------------------------------------",
            " 1  John   Smith     35  1 - Engineering  <null>          ",
            " 2  Mario  Rossi     23  1 - Engineering  1 - John Smith  "
        ]);

        Assert.Equal(expected, actual);
    }


    [Fact]
    public void Employees_Custom_Columns()
    {
        var sut = CreateEmployees();

        var actual = sut.FormatAsTable([ 
            new (_ => _.Name),
            new (_ => _.Surname),
            new (_ => _.Department.Name, "Department"),
            new (_ => _.Manager!, "Manager", rightAlign: true, formatter: v => v is Employee m ? $"{m.Name} {m.Surname}" : "<None>")
        ]);

        string expected = CreateExpected([
            "Name   Surname  Department   Manager     ",
            "-----------------------------------------",
            "John   Smith    Engineering      <None>  ",
            "Mario  Rossi    Engineering  John Smith  ",
        ]);

        Assert.Equal(expected, actual);
    }

    private static Employee[] CreateEmployees()
    {
        var engineering = new Department(1, "Engineering");

        var john = new Employee(1, "John", "Smith", 35, engineering);
        var mario = new Employee(2, "Mario", "Rossi", 23, engineering, john);

        Employee[] sut = [john, mario];
        return sut;
    }

    private static string CreateExpected(string[] expectedRows)
    {
        return string.Join(
            Environment.NewLine,
            expectedRows.Concat([string.Empty])
            );
    }
}