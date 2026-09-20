using Bodde.Common.Extensions;

namespace StringExtensions;

public class DoubleQuote
{
    [Theory]
    [InlineData("", "\"\"")]
    [InlineData("john", "\"john\"")]
    public void Test(string sut, string expected)
    {
        var actual = sut.DoubleQuote();

        Assert.Equal(expected, actual);
    }
}