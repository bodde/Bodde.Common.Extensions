using System.Text.RegularExpressions;
using Bodde.Common.Extensions;

namespace RegexExtensions;

public class GetMatchValues
{
    [Theory]
    [InlineData("", "", "")]
    [InlineData(@"(Sally)", "Call me Sally.", "Sally")]
    [InlineData(@"[Cc]all me (\w+)", "Call me Sarah, don't call me Sally!", "Call me Sarah,call me Sally")]
    [InlineData(@"(\w+)\s+(\w+)\s(\w+)\.", "Call me Sally.", "Call me Sally.")]
    [InlineData(@"(\w+)", "Call me Sally.", "Call,me,Sally")]
    public void From_Regex(string pattern, string input, string expectedCsv)
    {
        var sut = new Regex(pattern);
        var actual = sut.GetMatchValues(input);

        Assert.Equal(expectedCsv, actual.ToCsv());
    }

    [Theory]
    [InlineData("", "", "")]
    [InlineData(@"(Sally)", "Call me Sally.", "Sally")]
    [InlineData(@"[Cc]all me (\w+)", "Call me Sarah, don't call me Sally!", "Call me Sarah,call me Sally")]
    [InlineData(@"(\w+)\s+(\w+)\s(\w+)\.", "Call me Sally.", "Call me Sally.")]
    [InlineData(@"(\w+)", "Call me Sally.", "Call,me,Sally")]
    public void From_MatchCollection(string pattern, string input, string expectedCsv)
    {
        var sut = new Regex(pattern).Matches(input);
        var actual = sut.GetMatchValues();

        Assert.Equal(expectedCsv, actual.ToCsv());
    }

}