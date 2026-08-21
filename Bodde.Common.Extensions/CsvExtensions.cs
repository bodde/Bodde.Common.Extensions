using System;
using System.Collections.Generic;
using System.Linq;

namespace Bodde.Common.Extensions
{
    public static class CsvExtensions
    {
        /// <summary>
        /// Converts a sequence of values to a CSV-formatted string.
        /// </summary>
        /// <typeparam name="TItem">The type of the items in the sequence.</typeparam>
        /// <param name="values">The sequence of values to convert.</param>
        /// <param name="separator">The separator to place between values.</param>
        /// <returns>A CSV-formatted string containing the values.</returns>
        public static string ToCsv<TItem>(this IEnumerable<TItem> values, string separator = ",")
        {
            return string.Join(separator, values);
        }

        /// <summary>
        /// Converts a sequence of values to a CSV-formatted string using a custom conversion function.
        /// </summary>
        /// <typeparam name="T">The type of the items in the sequence.</typeparam>
        /// <param name="values">The sequence of values to convert.</param>
        /// <param name="formatter">The function used to convert each value to a string.</param>
        /// <param name="separator">The separator to place between converted values.</param>
        /// <returns>A CSV-formatted string containing the converted values.</returns>
        public static string ToCsv<T>(this IEnumerable<T> values, Func<T, string> formatter, string separator = ",")
        {
            return string.Join(separator, values.Select(formatter));
        }
    }
}

