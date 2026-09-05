using System.Text.RegularExpressions;
using Bodde.Common.Extensions;

namespace RegexExtensions;

public class MatchingValues
{
    [Theory]
    [InlineData("", "", "test", "")]
    [InlineData(@"(?<name>Sally)", "Call me Sally.", "name", "Sally")]
    [InlineData(@"[Cc]all me (?<name>\w+)", "Call me Sarah, don't call me Sally!", "name", "Sarah,Sally")]
    [InlineData(@"(?<verb>\w+)\s+(?<pronoun>\w+)\s(?<name>\w+)\.", "Call me Sally.", "name", "Sally")]
    public void Test(string pattern, string input, string groupName, string expectedCsv)
    {
        var sut = new Regex(pattern);
        var actual = sut.MatchingValues(input, groupName);

        Assert.Equal(expectedCsv, actual.ToCsv());
    }
}