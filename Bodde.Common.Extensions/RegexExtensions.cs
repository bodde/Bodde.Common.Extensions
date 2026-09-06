using System.Text.RegularExpressions;

public static class RegexExtensions
{
    extension(Regex regex)
    {
        /// <summary>
        /// Gets all successful captures for the specified named group across every match in the input text.
        /// </summary>
        /// <param name="input">The input text to search.</param>
        /// <param name="groupName">The name of the capturing group to extract.</param>
        /// <returns>An array containing the captured values for the named group.</returns>
        public string[] GetGroupValues(string input, string groupName)
        {
            return regex
                .Matches(input)
                .GetGroupValues(groupName);
        }
    }

    extension(MatchCollection matchCollection)
    {
        /// <summary>
        /// Gets all successful captures for the specified named group from a collection of matches.
        /// </summary>
        /// <param name="groupName">The name of the capturing group to extract.</param>
        /// <returns>An array containing the captured values for the named group.</returns>
        public string[] GetGroupValues(string groupName)
        {
            return matchCollection
                .Cast<Match>()
                .Select(match => match.Groups[groupName])
                .Where(group => group.Success)
                .Select(group => group.Value)
                .ToArray();
        }
    }
}