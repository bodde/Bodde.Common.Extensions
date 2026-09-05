using System.Reflection;
using System.Text.RegularExpressions;

public static class RegexExtensions
{
    extension(Regex regex)
    {
        public string[] MatchingValues(string input, string groupName)
        {
            var matches = regex.Matches(input);
            var namedGroups = GetNamedGroups(matches, groupName);

            return namedGroups
                .Select(namedGroup => namedGroup.Group.Value)
                .ToArray();
        }

        public Dictionary<string, string[]> MatchingGroupsValues(string input, string[] groupNames)
        {
            var matches = regex.Matches(input);

            var namedGroups = groupNames
                .SelectMany(groupName => GetNamedGroups(matches, groupName))
                .ToArray();

            var valuesByGroup = namedGroups
                .GroupBy(namedGroup => namedGroup.Name)
                .ToDictionary(
                    groupsByName => groupsByName.Key, 
                    groupsByName => groupsByName.Select(namedGroup => namedGroup.Group.Value).ToArray()
                    );

            return valuesByGroup;
        }

        public T FromMatchingValues<T>(string input, Func<string[], string>? pickOne = null)
            where T: new()
        {
            pickOne ??= values => values.FirstOrDefault();

            var setProperties = BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty;
            var propertyInfos = typeof(T).GetPropertyInfos(flags: setProperties);
            var propertyNames = propertyInfos.Select(pi => pi.Name).ToArray();

            var matchingGroupValues = regex.MatchingGroupsValues(input, propertyNames);

            var instance = new T();
            foreach(var propertyInfo in propertyInfos)
            {
                if(matchingGroupValues.TryGetValue(propertyInfo.Name, out var groupValues) == false)
                    continue;

                var groupValue = pickOne(groupValues);
                if(groupValue == null)
                    continue;

                var propertyValue = Convert.ChangeType(groupValue, propertyInfo.PropertyType);
                propertyInfo.SetValue(instance, propertyValue);
            }

            return instance;
        }
    }

    private static NamedGroup[] GetNamedGroups(MatchCollection matchCollection, string groupName)
    {
        return matchCollection
            .Cast<Match>()
            .Select(match => new NamedGroup(groupName, match.Groups[groupName]))
            .Where(namedGroup => namedGroup.Group.Success)
            .ToArray();
    }

    private record NamedGroup(string Name, Group Group);
}