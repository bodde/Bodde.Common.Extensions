using System;
using System.Collections.Generic;
using System.Linq;

namespace Bodde.Common.Extensions
{
    public static class CsvExtensions
    {
        public static string ToCsv<TItem>(this IEnumerable<TItem> values, string separator = ",")
        {
            return string.Join(separator, values);
        }

        public static string ToCsv<T>(this IEnumerable<T> values, Func<T, string> toStringFunc, string separator = ",")
        {
            return string.Join(separator, values.Select(toStringFunc));
        }
    }
}


