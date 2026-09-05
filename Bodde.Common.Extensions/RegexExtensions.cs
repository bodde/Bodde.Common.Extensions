using System.Text.RegularExpressions;

public static class RegexExtensions
{
    extension(Regex regex)
    {
        public string[] MatchingValues(string input, string groupName)
        {
            var matches = regex.Matches(input);
            var namedGroups = matches
                .Cast<Match>()
                .Select(match => match.Groups[groupName])
                .Where(_ => _.Success)
                //.Where(_ => _.Success && _.Captures.Count == 1)
                .ToArray();

            return namedGroups.Select(_ => _.Value).ToArray();
        }

        public Dictionary<string, string[]> MatchingValues(string input, string[] groupNames)
        {
            var matches = regex.Matches(input);
            var namedGroups = matches
                .Cast<Match>()
                .SelectMany(match => GetNamedGroups(match, groupNames))
                .ToArray();

            return namedGroups
                .GroupBy(namedGroup => namedGroup.Name)
                .ToDictionary(
                    namedGroups => namedGroups.Key, 
                    namedGroups => namedGroups.Select(_ => _.Group.Value).ToArray()
                    );
        }
    }

    private static IEnumerable<(string Name, Group Group)> GetNamedGroups(Match match, string[] groupNames)
    {
        return groupNames
            .Select(name => (Name: name, Group: match.Groups[name]))
            .Where(namedGroup => namedGroup.Group.Success);
    }
}