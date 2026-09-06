using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace Bodde.Common.Extensions;

public static class StringExtensions
{
    extension([NotNullWhen(false)]string? me)
    {
        /// <summary>
        /// Determines whether the string is null or empty.
        /// </summary>
        /// <returns><see langword="true"/> if the string is null or empty; otherwise, <see langword="false"/>.</returns>
        public bool IsNullOrEmpty() => string.IsNullOrEmpty(me);

        /// <summary>
        /// Determines whether the string is null, empty, or contains only white-space characters.
        /// </summary>
        /// <returns><see langword="true"/> if the string is null, empty, or contains only white-space characters; otherwise, <see langword="false"/>.</returns>
        public bool IsNullOrWhiteSpace() => string.IsNullOrWhiteSpace(me);
    }

    extension([NotNullWhen(true)]string? me)
    {
        /// <summary>
        /// Determines whether the string is not null and not empty.
        /// </summary>
        /// <returns><see langword="true"/> if the string is not null and not empty; otherwise, <see langword="false"/>.</returns>
        public bool IsNotNullOrEmpty() => string.IsNullOrEmpty(me) == false;

        /// <summary>
        /// Determines whether the string is not null and contains at least one non-whitespace character.
        /// </summary>
        /// <returns><see langword="true"/> if the string is not null and contains at least one non-whitespace character; otherwise, <see langword="false"/>.</returns>
        public bool IsNotNullOrWhiteSpace() => string.IsNullOrWhiteSpace(me) == false;
    }

    extension(string? me)
    {
        /// <summary>
        /// Returns an empty string if the string is null; otherwise, returns the original string.
        /// </summary>
        /// <returns>The original string or an empty string if the value is null.</returns>
        public string OrEmpty() => me.IsNullOrEmpty() ? string.Empty : me;
    }

    extension(string me)
    {
        /// <summary>
        /// Determines whether the string is empty.
        /// </summary>
        /// <returns><see langword="true"/> if the string is empty; otherwise, <see langword="false"/>.</returns>
        public bool IsEmpty() => me.Length == 0;

        /// <summary>
        /// Determines whether the string is empty or contains only white-space characters.
        /// </summary>
        /// <returns><see langword="true"/> if the string is empty or only contains white-space characters; otherwise, <see langword="false"/>.</returns>
        public bool IsEmptyOrWhiteSpace() => me.Length == 0 || string.IsNullOrWhiteSpace(me);

        /// <summary>
        /// Determines whether the first character of the string is uppercase.
        /// </summary>
        /// <returns><see langword="true"/> if the first character is uppercase; otherwise, <see langword="false"/>.</returns>
        public bool IsCapitalized()
        {
            if (me.Length == 0)
                return false;

            return char.IsUpper(me[0]);
        }

        /// <summary>
        /// Converts the first character of the string to uppercase.
        /// </summary>
        /// <returns>A copy of the string with its first character converted to uppercase.</returns>
        public string Capitalize()
        {
            if (me.IsNullOrEmpty())
                return me;

            return char.ToUpper(me[0]) + me.Substring(1);
        }

        /// <summary>
        /// Converts the first character of the string to lowercase.
        /// </summary>
        /// <returns>A copy of the string with its first character converted to lowercase.</returns>
        public string Uncapitalize()
        {
            if (me.IsNullOrEmpty())
                return me;

            return char.ToLower(me[0]) + me.Substring(1);
        }

        /// <summary>
        /// Returns the plural form of the string.
        /// </summary>
        /// <returns>The pluralized version of the string.</returns>
        public string Pluralize()
        {
            if (me.IsNullOrEmpty())
                return me;

            bool allUppercase = me == me.ToUpper();
            bool isCapitalized = me.IsCapitalized();

            var result = PluralizeInternal(me);

            if (allUppercase)
                return result.ToUpper();

            if (isCapitalized)
                return result.Capitalize();

            return result.Uncapitalize();
        }

        /// <summary>
        /// Converts the string to kebab-case by inserting hyphens before uppercase letters and lowercasing the content.
        /// </summary>
        /// <returns>A hyphenized version of the string.</returns>
        public string Hyphenize()
        {
            if (me.IsNullOrEmpty())
                return me;

            var sb = new StringBuilder();
            for (int i = 0; i < me.Length; i++)
            {
                if (char.IsUpper(me[i]) && i > 0)
                {
                    sb.Append('-');
                }
                sb.Append(char.ToLower(me[i]));
            }
            return sb.Replace(" ", "-").ToString();
        }

        /// <summary>
        /// Removes hyphens and capitalizes the following character.
        /// </summary>
        /// <returns>A de-hyphenized version of the string.</returns>
        public string Dehyphenize()
        {
            if (me.IsNullOrEmpty())
                return me;

            var sb = new StringBuilder();
            bool capitalizeNext = false;
            for (int i = 0; i < me.Length; i++)
            {
                if (me[i] == '-')
                {
                    capitalizeNext = true;
                }
                else
                {
                    sb.Append(capitalizeNext ? char.ToUpper(me[i]) : me[i]);
                    capitalizeNext = false;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Splits the string into tokens using the specified separator.
        /// </summary>
        /// <param name="separator">The character used to separate tokens.</param>
        /// <param name="trim">Indicates whether leading and trailing whitespace should be removed from each token.</param>
        /// <param name="removeEmpty">Indicates whether empty tokens should be removed from the result.</param>
        /// <returns>An array containing the tokens extracted from the string.</returns>
        public string[] Tokenize(char separator, bool trim = false, bool removeEmpty = false)
            => ProcessTokens(me.Split(separator), trim, removeEmpty);

        /// <summary>
        /// Splits the string into tokens using the specified separator.
        /// </summary>
        /// <param name="separator">The string used to separate tokens.</param>
        /// <param name="trim">Indicates whether leading and trailing whitespace should be removed from each token.</param>
        /// <param name="removeEmpty">Indicates whether empty tokens should be removed from the result.</param>
        /// <returns>An array containing the tokens extracted from the string.</returns>
        public string[] Tokenize(string separator, bool trim = false, bool removeEmpty = false)
        {
            if (separator.IsEmpty())
                throw new ArgumentOutOfRangeException(nameof(separator), "Empty separators are invalid.");

            return ProcessTokens(me.Split([separator], StringSplitOptions.None), trim, removeEmpty);
        }

        /// <summary>
        /// Converts the string to the specified type using <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        /// <typeparam name="T">The target type. Supported types include types implementing <see cref="IConvertible"/>, enumerations, <see cref="TimeSpan"/>, <see cref="DateTime"/>, <see cref="DateTimeOffset"/>, and their nullable forms.</typeparam>
        /// <returns>The converted value.</returns>
        public T ConvertTo<T>()
            => (T)me.ConvertTo(typeof(T));

        /// <summary>
        /// Converts the string to the specified type using <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        /// <param name="targetType">The target type. Supported types include types implementing <see cref="IConvertible"/>, enumerations, <see cref="TimeSpan"/>, <see cref="DateTime"/>, <see cref="DateTimeOffset"/>, and their nullable forms.</param>
        /// <returns>The converted value.</returns>
        public object ConvertTo(Type targetType)
        {
            targetType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            if (targetType.IsEnum) 
                return Enum.Parse(targetType, me);

            if (targetType == typeof(TimeSpan)) 
                return TimeSpan.Parse(me, CultureInfo.InvariantCulture);

            if (targetType == typeof(DateTime)) 
                return DateTime.Parse(me, CultureInfo.InvariantCulture);
                
            if (targetType == typeof(DateTimeOffset)) 
                return DateTimeOffset.Parse(me, CultureInfo.InvariantCulture);
            
            return Convert.ChangeType(me, targetType, CultureInfo.InvariantCulture);
        }
    }

    private static string PluralizeInternal(string me)
    {
        if (CommonIrregularPlurals.TryGetValue(me, out var pluralized))
            return pluralized;

        if (me.EndsWith("y", StringComparison.OrdinalIgnoreCase) && me.Length > 1 && !IsVowel(me[me.Length - 2]))
            return $"{me.Substring(0, me.Length - 1)}ies";

        if (me.EndsWith("s", StringComparison.OrdinalIgnoreCase) ||
                    me.EndsWith("x", StringComparison.OrdinalIgnoreCase) ||
                    me.EndsWith("z", StringComparison.OrdinalIgnoreCase) ||
                    me.EndsWith("ch", StringComparison.OrdinalIgnoreCase) ||
                    me.EndsWith("sh", StringComparison.OrdinalIgnoreCase) ||
                    me.EndsWith("o", StringComparison.OrdinalIgnoreCase))
            return $"{me}es";

        return $"{me}s";
    }

    private static bool IsVowel(char c)
    {
        return "aeiouAEIOU".IndexOf(c) >= 0;
    }

    private static Dictionary<string, string> CommonIrregularPlurals = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "Man", "Men" },
        { "Woman", "Women" },
        { "Foot", "Feet" },
        { "Tooth", "Teeth" },
        { "Goose", "Geese" },
        { "Mouse", "Mice" },
        { "Child", "Children" },
        { "Person", "People" },
        { "Ox", "Oxen" },
        { "Sheep", "Sheep" },
        { "Fish", "Fish" },
        { "Deer", "Deer" },
        { "Species", "Species" },
        { "Series", "Series" },
        { "Life", "Lives" },
        { "Knife", "Knives" },
        { "Wolf", "Wolves" },
        { "Thief", "Thieves" },
        { "Leaf", "Leaves" },
        { "Wife", "Wives" },
        { "Half", "Halves" },
        { "Focus", "Foci" },
        { "Phenomenon", "Phenomena" },
        { "Criterion", "Criteria" },
        { "Crisis", "Crises" },
        { "Analysis", "Analyses" },
        { "Cactus", "Cacti" }
     };

    private static string[] ProcessTokens(string[] tokens, bool trim, bool removeEmpty)
    {
        if (trim)
            tokens = tokens.Select(_ => _.Trim()).ToArray();

        if (removeEmpty)
            tokens = tokens.Where(_ => _.IsNullOrEmpty() == false).ToArray();

        return tokens;
    }
}

