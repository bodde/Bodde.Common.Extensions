using System.Linq.Expressions;

namespace Bodde.Common.Extensions;

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

