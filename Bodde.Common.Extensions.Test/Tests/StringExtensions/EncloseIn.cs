using Bodde.Common.Extensions;

namespace StringExtensions;

public class EncloseIn
{
    [Theory]
    [InlineData("", "", "")]
    [InlineData("john", "'", "'john'")]
    [InlineData("john", "[]", "[]john[]")]
    public void Test(string sut, string text, string expected)
    {
        var actual = sut.EncloseIn(text);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("", "", "", "")]
    [InlineData("john", "[", "]", "[john]")]
    [InlineData("john", "<<", ">>", "<<john>>")]
    public void Test_WithDifferentDelimiters(string sut, string left, string right, string expected)
    {
        var actual = sut.EncloseIn(left, right);

        Assert.Equal(expected, actual);
    }
}