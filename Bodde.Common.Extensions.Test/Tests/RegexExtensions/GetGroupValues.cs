using System.Text.RegularExpressions;
using Bodde.Common.Extensions;

namespace RegexExtensions;

public class GetGroupValues
{
    [Theory]
    [InlineData("", "", "test", "")]
    [InlineData(@"(?<name>Sally)", "Call me Sally.", "name", "Sally")]
    [InlineData(@"[Cc]all me (?<name>\w+)", "Call me Sarah, don't call me Sally!", "name", "Sarah,Sally")]
    [InlineData(@"(?<verb>\w+)\s+(?<pronoun>\w+)\s(?<name>\w+)\.", "Call me Sally.", "verb", "Call")]
    [InlineData(@"(?<verb>\w+)\s+(?<pronoun>\w+)\s(?<name>\w+)\.", "Call me Sally.", "name", "Sally")]
    [InlineData(@"(?<verb>\w+)\s+(?<pronoun>\w+)\s(?<name>\w+)\.", "Call me Sally.", "pronoun", "me")]
    public void From_Regex(string pattern, string input, string groupName, string expectedCsv)
    {
        var sut = new Regex(pattern);
        var actual = sut.GetGroupValues(input, groupName);

        Assert.Equal(expectedCsv, actual.ToCsv());
    }

    [Theory]
    [InlineData("", "", "test", "")]
    [InlineData(@"(?<name>Sally)", "Call me Sally.", "name", "Sally")]
    [InlineData(@"[Cc]all me (?<name>\w+)", "Call me Sarah, don't call me Sally!", "name", "Sarah,Sally")]
    [InlineData(@"(?<verb>\w+)\s+(?<pronoun>\w+)\s(?<name>\w+)\.", "Call me Sally.", "verb", "Call")]
    [InlineData(@"(?<verb>\w+)\s+(?<pronoun>\w+)\s(?<name>\w+)\.", "Call me Sally.", "name", "Sally")]
    [InlineData(@"(?<verb>\w+)\s+(?<pronoun>\w+)\s(?<name>\w+)\.", "Call me Sally.", "pronoun", "me")]
    public void From_MatchCollection(string pattern, string input, string groupName, string expectedCsv)
    {
        var sut = new Regex(pattern).Matches(input);
        var actual = sut.GetGroupValues(groupName);

        Assert.Equal(expectedCsv, actual.ToCsv());
    }
}