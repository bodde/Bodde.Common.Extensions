namespace Bodde.Common.Extensions;

public static class CsvExtensions
{
    extension<T>(IEnumerable<T> me)
    {    
        /// <summary>
        /// Converts a sequence of values to a CSV-formatted string.
        /// </summary>
        /// <param name="separator">The separator to place between values.</param>
        /// <returns>A CSV-formatted string containing the values.</returns>
        public string ToCsv(string separator = ",") => string.Join(separator, me);

        /// <summary>
        /// Converts a sequence of values to a CSV-formatted string using a custom conversion function.
        /// </summary>
        /// <param name="formatter">The function used to convert each value to a string.</param>
        /// <param name="separator">The separator to place between converted values.</param>
        /// <returns>A CSV-formatted string containing the converted values.</returns>
        public string ToCsv(Func<T, string> formatter, string separator = ",")
        {
            return string.Join(separator, me.Select(formatter));
        }
    }

    extension(string me)
    {
        /// <summary>
        /// Converts a CSV-formatted string into an array of strings.
        /// </summary>
        /// <param name="separator">The separator between values.</param>
        /// <param name="trim">Indicates whether leading and trailing whitespace should be removed from each value.</param>
        /// <param name="removeEmpty">Indicates whether empty values should be removed from the result.</param>
        /// <returns>An array containing the values extracted from the CSV-formatted string.</returns>
        public string[] FromCsv(string separator = ",", bool trim = true, bool removeEmpty = false)
            => me.Tokenize(separator, trim, removeEmpty);

        /// <summary>
        /// Converts a CSV-formatted string into an array of convertible values of the specified type.
        /// </summary>
        /// <typeparam name="T">The type to which each value should be converted.</typeparam>
        /// <param name="separator">The separator between values.</param>
        /// <param name="trim">Indicates whether leading and trailing whitespace should be removed from each value.</param>
        /// <param name="removeEmpty">Indicates whether empty values should be removed from the result.</param>
        /// <returns>An array containing the converted values.</returns>
        public T[] FromCsv<T>(string separator = ",", bool trim = true, bool removeEmpty = false) 
            where T: IConvertible
            => me
                .FromCsv(separator, trim, removeEmpty)
                .Select(t => Convert.ChangeType(t, typeof(T)))
                .Cast<T>()
                .ToArray();

         /// <summary>
        /// Converts a CSV-formatted string into an array of values of the specified type.
        /// </summary>
        /// <typeparam name="T">The type to which each value should be converted.</typeparam>
        /// <param name="parser">The function used to convert each string to a value of the specified type.</param>
        /// <param name="separator">The separator between values.</param>
        /// <param name="trim">Indicates whether leading and trailing whitespace should be removed from each value.</param>
        /// <param name="removeEmpty">Indicates whether empty values should be removed from the result.</param>
        /// <returns>An array containing the converted values.</returns>
        public T[] FromCsv<T>(Func<string, T> parser, string separator = ",", bool trim = true, bool removeEmpty = false) 
            => me
                .FromCsv(separator, trim, removeEmpty)
                .Select(t => parser(t))
                .ToArray();
    }
}


