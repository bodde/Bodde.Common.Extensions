using Bodde.Common.Extensions;

namespace StringExtensions;

public class OrEmpty
{
    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData("123", "123")]
    [InlineData("x", "x")]
    public void Test(string? sut, string expected)
    {
        var actual = sut.OrEmpty();

        Assert.Equal(expected, actual);
    }
}