namespace Bodde.Common.Extensions.Test.StringExtensions;

public class Dehyphenize
{
    [Theory]
    [InlineData("", "")]
    [InlineData("kebab-case", "kebabCase")]
    public void Test(string sut, string expected)
    {
        var actual = sut.Dehyphenize();

        Assert.Equal(expected, actual);
    }
}