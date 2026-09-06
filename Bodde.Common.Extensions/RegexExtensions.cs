using System.Text.RegularExpressions;

namespace Bodde.Common.Extensions;

public static class RegexExtensions
{
    extension(Regex regex)
    {
        /// <summary>
        /// Gets the matched text values for every match in the input.
        /// </summary>
        /// <param name="input">The input text to search.</param>
        /// <returns>An array containing the matched text for each match.</returns>
        public string[] GetValues(string input)
            => regex.Matches(input).GetValues();

        /// <summary>
        /// Gets all successful captures for the specified named group across every match in the input text.
        /// </summary>
        /// <param name="input">The input text to search.</param>
        /// <param name="groupName">The name of the capturing group to extract.</param>
        /// <returns>An array containing the captured values for the named group.</returns>
        public string[] GetGroupValues(string input, string groupName)
            => regex.Matches(input).GetGroupValues(groupName);
    }

    extension(MatchCollection matchCollection)
    {
        /// <summary>
        /// Gets the matched text for each regular expression match.
        /// </summary>
        /// <returns>An array containing the matched text for each match.</returns>
        public string[] GetValues()
        {
            return matchCollection
                .Cast<Match>()
                .SelectMany(_ => _.Groups.Cast<Group>().Skip(1))
                .Select(match => match.Value)       
                .ToArray();
        }

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