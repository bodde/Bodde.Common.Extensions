using Bodde.Common.Extensions.Test.Models;

namespace TypeExtensions;

public class GetPropertyNames
{
    [Fact]
    public void Employee()
    {
        var sut = typeof(Employee);

        var actual = sut.GetPropertyNames(useCache: false);

        var expected = GetExpectedResultForEmployee();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EmployeeWithCache()
    {
        var sut = typeof(Employee);

        sut.GetPropertyNames(useCache: true);
        var actual = sut.GetPropertyNames(useCache: true);
        
        var expected = GetExpectedResultForEmployee();
        
        Assert.Equal(expected, actual);
    }

        private static string[] GetExpectedResultForEmployee() => 
        [
            "Id",
            "Name",
            "Surname",
            "Age",
            "Department",
            "Manager"
        ];
}