using Bodde.Common.Extensions;

namespace StringExtensions;

public class SquareBracketize
{
    [Theory]
    [InlineData("", "[]")]
    [InlineData("john", "[john]")]
    public void Test(string sut, string expected)
    {
        var actual = sut.SquareBracketize();

        Assert.Equal(expected, actual);
    }
}