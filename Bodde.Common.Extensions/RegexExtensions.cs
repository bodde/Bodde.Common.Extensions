using System.Text.RegularExpressions;

public static class RegexExtensions
{
    extension(Regex regex)
    {
        public string[] GetGroupValues(string input, string groupName)
        {
            return regex
                .Matches(input)
                .GroupValues(groupName);
        }
    }
    extension(MatchCollection matchCollection)
    {
        public string[] GroupValues(string groupName)
        {
            return matchCollection
                .Cast<Match>()
                .Select(match =>  match.Groups[groupName])
                .Where(group => group.Success)
                .Select(group => group.Value)
                .ToArray();
        }
    }
}