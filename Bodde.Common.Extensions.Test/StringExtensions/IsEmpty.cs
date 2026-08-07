namespace Bodde.Common.Extensions.Test.StringExtensions;

public class IsEmpty
{
    [Theory]
    [InlineData("", true)]
    [InlineData("123", false)]
    [InlineData("x", false)]
    public void Test(string sut, bool expected)
    {
        var actual = sut.IsEmpty();

        Assert.Equal(expected, actual);
    }
}