using System;
using System.Collections.Generic;
using System.Text;

namespace Bodde.Common.Extensions
{
    public static class StringExtensions
    {

        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);


        public static bool IsEmpty(this string str) => str.Length == 0;

        public static bool IsCapitalized(this string str)
        {
            if (str.Length == 0)
                return false;

            return char.IsUpper(str[0]);
        }

        public static string Capitalize(this string str)
        {
            if (str.IsNullOrEmpty())
                return str;

            return char.ToUpper(str[0]) + str.Substring(1);
        }

        public static string Uncapitalize(this string str)
        {
            if (str.IsNullOrEmpty())
                return str;

            return char.ToLower(str[0]) + str.Substring(1);
        }

        public static string Pluralize(this string str)
        {
            if (str.IsNullOrEmpty())
                return str;

            bool allUppercase = str == str.ToUpper();
            bool isCapitalized = str.IsCapitalized();

            var result = PluralizeInternal(str);

            if (allUppercase)
                return result.ToUpper();

            if (isCapitalized)
                return result.Capitalize();

            return result.Uncapitalize();
        }


        public static string Hyphenize(this string str)
        {
            if (str.IsNullOrEmpty())
                return str;

            var sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsUpper(str[i]) && i > 0)
                {
                    sb.Append('-');
                }
                sb.Append(char.ToLower(str[i]));
            }
            return sb.Replace(" ", "-").ToString();
        }

        public static string Dehyphenize(this string str)
        {
            if (str.IsNullOrEmpty())
                return str;

            var sb = new StringBuilder();
            bool capitalizeNext = false;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '-')
                {
                    capitalizeNext = true;
                }
                else
                {
                    sb.Append(capitalizeNext ? char.ToUpper(str[i]) : str[i]);
                    capitalizeNext = false;
                }
            }
            return sb.ToString();
        }


        private static string PluralizeInternal(string str)
        {
            if (CommonIrregularPlurals.TryGetValue(str, out var pluralized))
                return pluralized;

            if (str.EndsWith("y", StringComparison.OrdinalIgnoreCase) && str.Length > 1 && !IsVowel(str[str.Length - 2]))
                return $"{str.Substring(0, str.Length - 1)}ies";

            if (str.EndsWith("s", StringComparison.OrdinalIgnoreCase) ||
                     str.EndsWith("x", StringComparison.OrdinalIgnoreCase) ||
                     str.EndsWith("z", StringComparison.OrdinalIgnoreCase) ||
                     str.EndsWith("ch", StringComparison.OrdinalIgnoreCase) ||
                     str.EndsWith("sh", StringComparison.OrdinalIgnoreCase) ||
                     str.EndsWith("o", StringComparison.OrdinalIgnoreCase))
                return $"{str}es";

            return $"{str}s";
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
}
