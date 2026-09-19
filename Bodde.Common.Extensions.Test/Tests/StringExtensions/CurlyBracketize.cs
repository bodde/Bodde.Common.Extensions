using Bodde.Common.Extensions;

namespace StringExtensions;

public class CurlyBracketize
{
    [Theory]
    [InlineData("", "{}")]
    [InlineData("john", "{john}")]
    public void Test(string sut, string expected)
    {
        var actual = sut.CurlyBracketize();

        Assert.Equal(expected, actual);
    }
}