using Bodde.Common.Extensions;

namespace StringExtensions;

public class IsNotNullOrWhiteSpace
{
    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("  ", false)]
    [InlineData("\t", false)]
    [InlineData("\t ", false)]
    [InlineData("123", true)]
    [InlineData("x", true)]
    public void Test(string? sut, bool expected)
    {
        var actual = sut.IsNotNullOrWhiteSpace();

        Assert.Equal(expected, actual);
    }
}