namespace Bodde.Common.Extensions.Test;

public class CsvExtensions_ToCsv
{
    [Fact]
    public void ABC_Array_ToCsv()
    {
        string[] sut = ["A", "B", "C"];

        var actual = sut.ToCsv();

        Assert.Equal("A,B,C", actual);
    }

    [Fact]
    public void ABC_Array_ToCsv_Custom_Separator()
    {
        string[] sut = ["A", "B", "C"];

        var actual = sut.ToCsv("|");

        Assert.Equal("A|B|C", actual);
    }


    [Fact]
    public void Employees_Array_ToCsv_ToString()
    {
        Employee[] sut = [
            new Employee("John", "Smith", 35),
            new Employee("Mario", "Rossi", 23),
            new Employee("Mr", "Bodde", 55),
        ];

        var actual = sut.ToCsv(", ");

        Assert.Equal("John Smith, Mario Rossi, Mr Bodde", actual);
    }

    [Fact]
    public void Employees_Array_ToCsv_Custom_ToString()
    {
        Employee[] sut = [
            new Employee("John", "Smith", 35),
            new Employee("Mario", "Rossi", 23),
            new Employee("Mr", "Bodde", 55),
        ];

        var actual = sut.ToCsv(_ => $"{_.Surname}, {_.Name} ({_.Age})", "; ");

        Assert.Equal("Smith, John (35); Rossi, Mario (23); Bodde, Mr (55)", actual);
    }

    private record Employee(string Name, string Surname, int Age)
    {
        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }
}
