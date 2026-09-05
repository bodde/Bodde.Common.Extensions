using Bodde.Common.Extensions.Test.Models;

namespace TypeExtensions;

public class IsCollection
{
    [Theory]
    [InlineData(typeof(int[]), true)]
    [InlineData(typeof(List<int>), true)]
    [InlineData(typeof(Dictionary<int, string>), true)]
    [InlineData(typeof(IEnumerable<int>), true)]
    [InlineData(typeof(string), false)]
    [InlineData(typeof(int), false)]
    [InlineData(typeof(int?), false)]
    [InlineData(typeof(Task<int>), false)]
    [InlineData(typeof(Tuple<int, string>), false)]
    [InlineData(typeof(Employee), false)]
    public void Test(Type sut, bool expected)
    {
        var actual = sut.IsCollection();

        Assert.Equal(expected, actual);
    }
}