namespace Bodde.Common.Extensions.Test.StringExtensions;

public class Capitalize
{
    [Theory]
    [InlineData("", "")]
    [InlineData("123", "123")]
    [InlineData("john", "John")]
    [InlineData("John", "John")]
    [InlineData("JOHN", "JOHN")]
    public void Test(string sut, string expected)
    {
        var actual = sut.Capitalize();

        Assert.Equal(expected, actual);
    }
}