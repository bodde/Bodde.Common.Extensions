using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Bodde.Common.Extensions;

public static class FormatExtensions
{
    extension<T>(IEnumerable<T> me)
    {
        public string FormatAsTable(params FormatTableColumn<T>[] columns)
        {
            columns = columns.IsEmpty() 
                ? GetFormatTableColumns<T>()
                : columns;

            var columnCount = columns.Length;

            var rows = me
                .Select(item => GetRowValues(item, columns))
                .ToArray();

            var columnLengths = columns.Select((col, columnIndex) => Math.Max(col.Header.Length, GetMaxColumnLength(rows, columnIndex))).ToArray();

            var sb = new StringBuilder();
            var rowLength = 0;
            var spacing = 2;
            for (var colIndex = 0; colIndex < columnCount; colIndex++)
            {
                var header = columns[colIndex].Header;
                var columnLengthWithSpacing = columnLengths[colIndex] + spacing;
                rowLength += columnLengthWithSpacing;

                sb.Append(header.PadRight(columnLengthWithSpacing));
            }
            sb.AppendLine();

            sb.AppendLine(new string('-', rowLength));

            for (var rowIndex = 0; rowIndex < rows.Length; rowIndex++)
            {
                var row = rows[rowIndex];
                for (var colIndex = 0; colIndex < columnCount; colIndex++)
                {
                    var value = row[colIndex];
                    var columnLength = columnLengths[colIndex];

                    var rightAlign = columns[colIndex].RightAlign;
                    var formattedValue = rightAlign ? value.PadLeft(columnLength) : value.PadRight(columnLength);

                    sb.Append(formattedValue);
                    sb.Append(new string(' ', spacing));
                }

                sb.AppendLine();
            }

            if (rows.Length == 0)
                sb.AppendLine("No data to display");

            return sb.ToString();
        }
    }

    private static FormatTableColumn<T>[] GetFormatTableColumns<T>()
    {
        var type = typeof(T);
        var propertyInfos = typeof(T).GetPropertyInfos();
        var parameter = Expression.Parameter(typeof(T), "x");

        return propertyInfos
            .Select(pi => CreateLambdaExpression<T>(pi, parameter))
            .Select(expr => new FormatTableColumn<T>(expr))
            .ToArray();
    }

    private static Expression<Func<T, object>> CreateLambdaExpression<T>(PropertyInfo pi, ParameterExpression parameter)
    {
        var expression = Expression.Convert(Expression.Property(parameter, pi), typeof(object));

        return Expression.Lambda<Func<T, object>>(expression, parameter);
    }

    private static int GetMaxColumnLength(string[][] values, int columnIndex)
    {
        var columnValues = GetColumnValues(values, columnIndex);
        if (columnValues.Length == 0)
            return 0;

        return columnValues.Max(v => v.Length);
    }

    private static string[] GetColumnValues(string[][] values, int columnIndex)
    {
        return [.. values.Select(row => row[columnIndex])];
    }

    private static string[] GetRowValues<T>(T item, FormatTableColumn<T>[] columnSelectors)
    {
        return [.. columnSelectors.Select(col => GetPropertyValue(item, col.GetValue, col.Formatter))];
    }

    private static string GetPropertyValue<T>(T item, Func<T, object?> valueGetter, Func<object?, string>? valueFormatter = null)
    {
        var value = item == null ? null : valueGetter(item);
        return valueFormatter?.Invoke(value) ?? value?.ToString() ?? "<null>";
    }
}


