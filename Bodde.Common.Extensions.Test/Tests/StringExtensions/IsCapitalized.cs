using Bodde.Common.Extensions;

namespace StringExtensions;

public class IsCapitalized
{
    [Theory]
    [InlineData("", false)]
    [InlineData("123", false)]
    [InlineData("john", false)]
    [InlineData("John", true)]
    public void Test(string sut, bool expected)
    {
        var actual = sut.IsCapitalized();

        Assert.Equal(expected, actual);
    }
}