using System.Linq.Expressions;
using System.Text;

namespace Bodde.Common.Extensions;

public static class FormatExtensions
{
    extension<T>(IEnumerable<T> me)
    {
        public string FormatAsTable(params FormatTableColumn<T>[] columnSelectors)
        {
            var columnCount = columnSelectors.Length;
            if (columnCount == 0)
                return "Please select at least one column to display";

            var rows = me
                .Select(item => GetRowValues(item, columnSelectors))
                .ToArray();

            var columnLengths = columnSelectors.Select((col, columnIndex) => Math.Max(col.Header.Length, GetMaxColumnLength(rows, columnIndex))).ToArray();

            var sb = new StringBuilder();
            var rowLength = 0;
            var spacing = 2;
            for (var colIndex = 0; colIndex < columnCount; colIndex++)
            {
                var header = columnSelectors[colIndex].Header;
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

                    var rightAlign = columnSelectors[colIndex].RightAlign;
                    var formattedValue = rightAlign ? value.PadLeft(columnLength) : value.PadRight(columnLength);

                    sb.Append(formattedValue);
                    sb.Append(new string(' ', spacing));
                }

                if (rowIndex < rows.Length - 1)
                {
                    sb.AppendLine();
                }
            }

            if (rows.Length == 0)
                sb.AppendLine("No data to display");

            return sb.ToString();
        }
    }

    public class FormatTableColumn<T>
    {
        private IPropertyAccessor<T, object> propertyAccessor;

        public string Header { get; }
        public Func<object?, string>? Formatter { get; }
        public bool RightAlign { get; }
        public Func<T, object?> GetValue { get; }

        public FormatTableColumn(
            Expression<Func<T, object>> columnSelector, 
            string? header = null, 
            bool? rightAlign = null,
            Func<object?, string>? formatter = null
            )
        {
            propertyAccessor = columnSelector.GetPropertyAccessor();

            Header = header ?? propertyAccessor.Path;
            Formatter = formatter;
            RightAlign = rightAlign ?? propertyAccessor.Type.IsNumeric();
            GetValue = propertyAccessor.GetValue;
        }
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

    private static string GetPropertyValue<T>(T item, Func<T, object> valueGetter, Func<object?, string>? valueFormatter = null)
    {
        var value = item == null ? null : valueGetter(item);
        return valueFormatter?.Invoke(value) ?? value?.ToString() ?? "<null>";
    }
}


