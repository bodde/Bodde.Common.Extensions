using System.Linq.Expressions;

namespace Bodde.Common.Extensions;

public class FormatTableColumn<T>
{
    private IPropertyAccessor<T, object> propertyAccessor;

    internal string Header { get; }
    internal Func<object?, string>? Formatter { get; }
    internal bool RightAlign { get; }
    internal Func<T, object?> GetValue { get; }

    /// <summary>
    /// Initializes a column definition from a property selector.
    /// </summary>
    /// <param name="columnSelector">The expression that selects the column property.</param>
    /// <param name="header">The column header, or the selected property path when omitted.</param>
    /// <param name="rightAlign">Indicates whether values should be right-aligned. When omitted, numeric values are right-aligned.</param>
    /// <param name="formatter">The function used to format column values.</param>
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

