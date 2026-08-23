using Bodde.Common.Extensions;

namespace StringExtensions;

public class Uncapitalize
{
    [Theory]
    [InlineData("", "")]
    [InlineData("123", "123")]
    [InlineData("John", "john")]
    [InlineData("john", "john")]
    public void Test(string sut, string expected)
    {
        var actual = sut.Uncapitalize();

        Assert.Equal(expected, actual);
    }
}