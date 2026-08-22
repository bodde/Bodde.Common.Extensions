namespace Bodde.Common.Extensions.Test.Tests.StringExtensions;

public class Hyphenize
{
    [Theory]
    [InlineData("", "")]
    [InlineData("CamelCase", "camel-case")]
    [InlineData("camelCaseLower", "camel-case-lower")]
    public void Test(string sut, string expected)
    {
        var actual = sut.Hyphenize();

        Assert.Equal(expected, actual);
    }
}