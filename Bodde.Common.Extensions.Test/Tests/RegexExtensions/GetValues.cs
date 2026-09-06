using System.Text.RegularExpressions;
using Bodde.Common.Extensions;

namespace RegexExtensions;

public class GetValues
{
    [Theory]
    [InlineData("", "", "")]
    [InlineData(@"(Sally)", "Call me Sally.", "Sally")]
    [InlineData(@"[Cc]all me (\w+)", "Call me Sarah, don't call me Sally!", "Sarah,Sally")]
    [InlineData(@"(\w+)\s+(\w+)\s(\w+)\.", "Call me Sally.", "Call,me,Sally")]
    [InlineData(@"(\w+)", "Call me Sally.", "Call,me,Sally")]
    public void From_Regex(string pattern, string input, string expectedCsv)
    {
        var sut = new Regex(pattern);
        var actual = sut.GetValues(input);

        Assert.Equal(expectedCsv, actual.ToCsv());
    }

    [Theory]
    [InlineData("", "", "")]
    [InlineData(@"(Sally)", "Call me Sally.", "Sally")]
    [InlineData(@"[Cc]all me (\w+)", "Call me Sarah, don't call me Sally!", "Sarah,Sally")]
    [InlineData(@"(\w+)\s+(\w+)\s(\w+)\.", "Call me Sally.", "Call,me,Sally")]
    [InlineData(@"(\w+)", "Call me Sally.", "Call,me,Sally")]
    public void From_MatchCollection(string pattern, string input, string expectedCsv)
    {
        var sut = new Regex(pattern).Matches(input);
        var actual = sut.GetValues();

        Assert.Equal(expectedCsv, actual.ToCsv());
    }
}