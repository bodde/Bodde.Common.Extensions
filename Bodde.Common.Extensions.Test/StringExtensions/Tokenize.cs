namespace Bodde.Common.Extensions.Test.StringExtensions;

public class Tokenize
{
    [Theory]
    [InlineData("", ' ', false, false, "")]
    [InlineData("A|B", '|', false, false, "A,B")]
    [InlineData(" A| B |C ", '|', false, false, " A, B ,C ")]
    [InlineData(" A| B |C ", '|', true, false, "A,B,C")]
    [InlineData("A||C", '|', false, true, "A,C")]
    [InlineData("A|  |C", '|', false, true, "A,  ,C")]
    [InlineData("A|  |C", '|', true, true, "A,C")]
    [InlineData("Hello world", ' ', false, false, "Hello,world")]
    [InlineData("Hello  world", ' ', false, false, "Hello,,world")]
    [InlineData("Hello  world", ' ', false, true, "Hello,world")]
    public void Test(string sut, char separator, bool trim, bool removeEmpty, string expectedCsv)
    {
        var actual = sut.Tokenize(separator, trim, removeEmpty);

        Assert.Equal(expectedCsv, actual.ToCsv());
    }
}