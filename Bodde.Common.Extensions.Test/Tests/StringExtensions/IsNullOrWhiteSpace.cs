namespace Bodde.Common.Extensions.Test.Tests.StringExtensions;

public class IsNullOrWhiteSpace
{
    [Theory]
    [InlineData(null, true)]
    [InlineData("", true)]
    [InlineData(" ", true)]
    [InlineData("  ", true)]
    [InlineData("\t", true)]
    [InlineData("\t ", true)]
    [InlineData("123", false)]
    [InlineData("x", false)]
    public void Test(string? sut, bool expected)
    {
        var actual = sut.IsNullOrWhiteSpace();

        Assert.Equal(expected, actual);
    }
}