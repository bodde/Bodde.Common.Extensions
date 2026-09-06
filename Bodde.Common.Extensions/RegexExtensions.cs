using System.Text.RegularExpressions;

namespace Bodde.Common.Extensions;

public static class RegexExtensions
{
    extension(Regex regex)
    {
        /// <summary>
        /// Gets the matched text for every match in the input.
        /// </summary>
        /// <param name="input">The input text to search.</param>
        /// <returns>An array containing the matched text for each match.</returns>
        public string[] GetMatchValues(string input)
            => regex.Matches(input).GetMatchValues();


        /// <summary>
        /// Gets the matched text values for every match in the input.
        /// </summary>
        /// <param name="input">The input text to search.</param>
        /// <returns>An array containing the matched text for each match.</returns>
        /// <remarks>The first group of every match is skipped because it contains the complete match.</remarks>
        public string[] GetGroupValues(string input)
            => regex.Matches(input).GetGroupValues();

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
        public string[] GetMatchValues()
        {
            return matchCollection
                .Cast<Match>()
                .Select(_ => _.Value)   
                .ToArray();
        }

        /// <summary>
        /// Gets the matched text for each regular expression match.
        /// </summary>
        /// <returns>An array containing the matched text for each match.</returns>
        /// <remarks>The first group of every match is skipped because it contains the complete match.</remarks>
        public string[] GetGroupValues()
        {
            return matchCollection
               .Cast<Match>()
               .SelectMany(_ => _.Groups.Cast<Group>().Skip(1)) // skip full match group
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