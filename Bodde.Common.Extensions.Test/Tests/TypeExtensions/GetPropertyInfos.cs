using Bodde.Common.Extensions.Test.Models;

namespace TypeExtensions;

public class GetPropertyInfos
{
    [Fact]
    public void Employee()
    {
        var sut = typeof(Employee);

        var actual = sut.GetPropertyInfos(useCache: false);

        var expected = GetExpectedResultForEmployee();

        Assert.Equal(expected, actual.Select(_ => _.ToString()));
    }

    [Fact]
    public void EmployeeWithCache()
    {
        var sut = typeof(Employee);

        sut.GetPropertyInfos(useCache: true);
        var actual = sut.GetPropertyInfos(useCache: true);
        
        var expected = GetExpectedResultForEmployee();
        
        Assert.Equal(expected, actual.Select(_ => _.ToString()));
    }

        private static string[] GetExpectedResultForEmployee() => 
        [
            "Int32 Id",
            "System.String Name",
            "System.String Surname",
            "Int32 Age",
            "Bodde.Common.Extensions.Test.Models.Department Department",
            "Bodde.Common.Extensions.Test.Models.Employee Manager"
        ];
}