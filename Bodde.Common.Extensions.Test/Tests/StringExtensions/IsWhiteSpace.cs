using Bodde.Common.Extensions;

namespace StringExtensions;

public class IsEmptyOrWhiteSpace
{
    [Theory]
    [InlineData("", true)]
    [InlineData(" ", true)]
    [InlineData("  ", true)]
    [InlineData("\t", true)]
    [InlineData("\t ", true)]
    [InlineData(" \t ", true)]
    [InlineData("123", false)]
    [InlineData("x", false)]
    public void Test(string sut, bool expected)
    {
        var actual = sut.IsEmptyOrWhiteSpace();

        Assert.Equal(expected, actual);
    }
}