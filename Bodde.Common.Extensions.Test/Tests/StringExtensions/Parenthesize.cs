using Bodde.Common.Extensions;

namespace StringExtensions;

public class Parenthesize
{
    [Theory]
    [InlineData("", "()")]
    [InlineData("john", "(john)")]
    public void Test(string sut, string expected)
    {
        var actual = sut.Parenthesize();

        Assert.Equal(expected, actual);
    }
}