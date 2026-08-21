namespace Bodde.Common.Extensions.Test.StringExtensions;

public class Tokenize
{
    [Theory]
    [InlineData("", ' ', false, false, "")]
    [InlineData("ABC", '|', false, false, "ABC")]
    [InlineData("ABC ", '|', false, true, "ABC ")]
    [InlineData("ABC ", '|', true, false, "ABC")]
    [InlineData("A|B", '|', false, false, "A,B")]
    [InlineData(" A| B |C ", '|', false, false, " A, B ,C ")]
    [InlineData(" A| B |C ", '|', true, false, "A,B,C")]
    [InlineData("A||C", '|', false, true, "A,C")]
    [InlineData("A|  |C", '|', false, true, "A,  ,C")]
    [InlineData("A|  |C", '|', true, true, "A,C")]
    [InlineData("Hello world", ' ', false, false, "Hello,world")]
    [InlineData("Hello  world", ' ', false, false, "Hello,,world")]
    [InlineData("Hello  world", ' ', false, true, "Hello,world")]
    public void CharSeparator(string sut, char separator, bool trim, bool removeEmpty, string expectedCsv)
    {
        var actual = sut.Tokenize(separator, trim, removeEmpty);

        Assert.Equal(expectedCsv, actual.ToCsv());
    }

    [Fact]
    public void StringSeparator_Empty_Throw()
    {
        var sut = "ABC";
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => sut.Tokenize(string.Empty));

        Assert.Equal("Empty separators are invalid. (Parameter 'separator')", ex.Message);
    }

    [Theory]
    [InlineData("", " ", false, false, "")]
    [InlineData("ABC", "|", false, false, "ABC")]
    [InlineData("ABC ", "|", false, true, "ABC ")]
    [InlineData("ABC ", "|", true, false, "ABC")]
    [InlineData("A|B", "|", false, false, "A,B")]
    [InlineData(" A| B |C ", "|", false, false, " A, B ,C ")]
    [InlineData(" A| B |C ", "|", true, false, "A,B,C")]
    [InlineData("A||C", "|", false, true, "A,C")]
    [InlineData("A|  |C", "|", false, true, "A,  ,C")]
    [InlineData("A|  |C", "|", true, true, "A,C")]
    [InlineData("Hello world", " ", false, false, "Hello,world")]
    [InlineData("Hello  world", " ", false, false, "Hello,,world")]
    [InlineData("Hello  world", " ", false, true, "Hello,world")]
    [InlineData("ABC", "|@|", false, false, "ABC")]
    [InlineData("ABC ", "|@|", false, true, "ABC ")]
    [InlineData("ABC ", "|@|", true, false, "ABC")]
    [InlineData("A|@|B", "|@|", false, false, "A,B")]
    [InlineData(" A|@| B |@|C ", "|@|", false, false, " A, B ,C ")]
    [InlineData(" A|@| B |@|C ", "|@|", true, false, "A,B,C")]
    [InlineData("A|@||@|C", "|@|", false, true, "A,C")]
    [InlineData("A|@|  |@|C", "|@|", false, true, "A,  ,C")]
    [InlineData("A|@|  |@|C", "|@|", true, true, "A,C")]
    public void StringSeparator(string sut, string separator, bool trim, bool removeEmpty, string expectedCsv)
    {
        var actual = sut.Tokenize(separator, trim, removeEmpty);

        Assert.Equal(expectedCsv, actual.ToCsv());
    }
}