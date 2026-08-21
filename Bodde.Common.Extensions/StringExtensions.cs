using System.Text;

namespace Bodde.Common.Extensions;


public static class StringExtensions
{
    extension (string? me)
    {
        /// <summary>
        /// Determines whether the string is null or empty.
        /// </summary>
        public bool IsNullOrEmpty() => string.IsNullOrEmpty(me);

        /// <summary>
        /// Determines whether the string is null, empty, or contains only white-space characters.
        /// </summary>
        public bool IsNullOrWhiteSpace() => string.IsNullOrWhiteSpace(me);
    }

    extension (string me)
    {
        /// <summary>
        /// Determines whether the string is empty.
        /// </summary>
        public bool IsEmpty() => me.Length == 0;

        /// <summary>
        /// Determines whether the string is empty or contains only white-space characters.
        /// </summary>
        public bool IsEmptyOrWhiteSpace() => me.Length == 0 || string.IsNullOrWhiteSpace(me);

        /// <summary>
        /// Determines whether the first character of the string is uppercase.
        /// </summary>
        public bool IsCapitalized()
        {
            if (me.Length == 0)
                return false;

            return char.IsUpper(me[0]);
        }

        /// <summary>
        /// Converts the first character of the string to uppercase.
        /// </summary>
        public string Capitalize()
        {
            if (me.IsNullOrEmpty())
                return me;

            return char.ToUpper(me[0]) + me.Substring(1);
        }

        /// <summary>
        /// Converts the first character of the string to lowercase.
        /// </summary>
        public string Uncapitalize()
        {
            if (me.IsNullOrEmpty())
                return me;

            return char.ToLower(me[0]) + me.Substring(1);
        }

        /// <summary>
        /// Returns the plural form of the string.
        /// </summary>
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
        /// Converts the string to kebab-case using hyphens as separators.
        /// </summary>
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


        private string PluralizeInternal()
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
}

