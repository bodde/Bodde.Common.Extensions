using Bodde.Common.Extensions;

namespace StringExtensions;

public class SingleQuote
{
    [Theory]
    [InlineData("", "''")]
    [InlineData("john", "'john'")]
    public void Test(string sut, string expected)
    {
        var actual = sut.SingleQuote();

        Assert.Equal(expected, actual);
    }
}