namespace Bodde.Common.Extensions.Test.StringExtensions;

public class IsNullOrEmpty
{
    [Theory]
    [InlineData(null, true)]
    [InlineData("", true)]
    [InlineData(" ", false)]
    [InlineData("123", false)]
    [InlineData("x", false)]
    public void Test(string? sut, bool expected)
    {
        var actual = sut.IsNullOrEmpty();

        Assert.Equal(expected, actual);
    }
}