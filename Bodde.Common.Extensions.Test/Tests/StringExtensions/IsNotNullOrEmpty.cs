using Bodde.Common.Extensions;

namespace StringExtensions;

public class IsNotNullOrEmpty
{
    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", true)]
    [InlineData("123", true)]
    [InlineData("x", true)]
    public void Test(string? sut, bool expected)
    {
        var actual = sut.IsNotNullOrEmpty();

        Assert.Equal(expected, actual);
    }
}