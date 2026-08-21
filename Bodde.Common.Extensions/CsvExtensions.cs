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
        public string ToCsv(string separator = ",")
        {
            return string.Join(separator, me);
        }

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



}


