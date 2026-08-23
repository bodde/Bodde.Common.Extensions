# Bodde.Common.Extensions

[![.NET Standard](https://img.shields.io/badge/.NET%20Standard-2.0-512BD4)](https://dotnet.microsoft.com/platform/dotnet-standard)
[![Build](https://github.com/bodde/Bodde.Common.Extensions/actions/workflows/dotnet.yml/badge.svg)](https://github.com/bodde/Bodde.Common.Extensions/actions)
[![Code coverage](https://img.shields.io/badge/code%20coverage-100%25-brightgreen)](https://github.com/bodde/Bodde.Common.Extensions/tree/main/Bodde.Common.Extensions.Test)

This package contains a collection of lightweight extension methods for the most common uses.
It does not depend on any other package.


## Getting Started

Install the package using the .NET CLI:

```bash
dotnet add package Bodde.Common.Extensions
```

Then add the following using statement to your C# code:

```csharp
using Bodde.Common.Extensions;
```

## Projects reference

| Project | Description | GitHub |
| --- | --- | --- |
| `Bodde.Common.Extensions` | Main library containing the extension methods. | [View project](https://github.com/bodde/Bodde.Common.Extensions/tree/main/Bodde.Common.Extensions) |
| `Bodde.Common.Extensions.Test` | Automated tests for the library. | [View project](https://github.com/bodde/Bodde.Common.Extensions/tree/main/Bodde.Common.Extensions.Test) |
| `Samples.ConsoleApp` | Console application demonstrating package usage. | [View project](https://github.com/bodde/Bodde.Common.Extensions/tree/main/Samples/Samples.ConsoleApp) |

## API reference

### ArrayExtensions.IsEmpty

Determines whether an array contains no elements.

| Parameter | Type | Description |
| --- | --- | --- |
| `me` | `T[]` | The array to check. |

**Return type:** `bool` - `true` when the array is empty; otherwise, `false`.

```csharp
var values = Array.Empty<int>();
var isEmpty = values.IsEmpty(); // true
```

### ArrayExtensions.OrEmpty

Returns the original array, or an empty array when the value is `null`.

| Parameter | Type | Description |
| --- | --- | --- |
| `me` | `T[]?` | The array to return or replace. |

**Return type:** `T[]` - The original array, or an empty array when the value is `null`.

```csharp
int[]? values = null;
var safeValues = values.OrEmpty(); // []
```

### CsvExtensions.ToCsv

Converts a sequence of values to a CSV-formatted string.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `me` | `IEnumerable<T>` | Required | The sequence to convert. |
| `separator` | `string` | `","` | The separator placed between values. |

**Return type:** `string` - The sequence formatted as a CSV string.

```csharp
var values = new[] { "A", "B", "C" };
var csv = values.ToCsv(); // A,B,C
var semicolonCsv = values.ToCsv("; "); // A; B; C
```

### CsvExtensions.ToCsv with formatter

Converts a sequence to CSV using a custom function to format each value.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `me` | `IEnumerable<T>` | Required | The sequence to convert. |
| `formatter` | `Func<T, string>` | Required | Converts each value to a string. |
| `separator` | `string` | `","` | The separator placed between converted values. |

**Return type:** `string` - The formatted values joined into a CSV string.

```csharp
var employees = new[] { new { Name = "John", Age = 35 } };
var csv = employees.ToCsv(employee => $"{employee.Name} ({employee.Age})");
// John (35)
```

### CsvExtensions.FromCsv

Splits a CSV-formatted string into an array of strings.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `me` | `string` | Required | The CSV-formatted string. |
| `separator` | `string` | `","` | The separator between values. |
| `trim` | `bool` | `true` | Removes surrounding whitespace from each value. |
| `removeEmpty` | `bool` | `false` | Removes empty values from the result. |

**Return type:** `string[]` - The values extracted from the CSV string.

```csharp
var values = "A, B, C".FromCsv(); // ["A", "B", "C"]
```

### CsvExtensions.FromCsv<T>

Converts a CSV-formatted string into an array of values implementing `IConvertible`.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `me` | `string` | Required | The CSV-formatted string. |
| `separator` | `string` | `","` | The separator between values. |
| `trim` | `bool` | `true` | Removes surrounding whitespace from each value. |
| `removeEmpty` | `bool` | `false` | Removes empty values from the result. |

**Return type:** `T[]` - The CSV values converted to `T`.

```csharp
var values = "10, 20, 30".FromCsv<int>(); // [10, 20, 30]
```

### CsvExtensions.FromCsv<T> with parser

Converts each CSV value with a custom parser function.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `me` | `string` | Required | The CSV-formatted string. |
| `parser` | `Func<string, T>` | Required | Converts each string value to `T`. |
| `separator` | `string` | `","` | The separator between values. |
| `trim` | `bool` | `true` | Removes surrounding whitespace from each value. |
| `removeEmpty` | `bool` | `false` | Removes empty values from the result. |

**Return type:** `T[]` - The CSV values converted by the parser.

```csharp
var values = "yes,no".FromCsv(value => value == "yes"); // [true, false]
```

### FormatExtensions.FormatAsTable

Formats a sequence as a text table. When no columns are supplied, public properties of the item type are used automatically.

| Parameter | Type | Description |
| --- | --- | --- |
| `me` | `IEnumerable<T>` | The sequence to format. |
| `columns` | `params FormatTableColumn<T>[]` | Optional column definitions. |

**Return type:** `string` - The sequence formatted as a text table.

```csharp
var employees = new[]
{
	new { Name = "John", Age = 35 },
	new { Name = "Maria", Age = 29 }
};

var table = employees.FormatAsTable();
```

### StringExtensions.IsNullOrEmpty

Determines whether a string is `null` or empty.

| Parameter | Type | Description |
| --- | --- | --- |
| `me` | `string?` | The string to check. |

**Return type:** `bool` - `true` when the string is `null` or empty; otherwise, `false`.

```csharp
string? value = null;
var result = value.IsNullOrEmpty(); // true
```

### StringExtensions.IsNullOrWhiteSpace

Determines whether a string is `null`, empty, or contains only whitespace characters.

| Parameter | Type | Description |
| --- | --- | --- |
| `me` | `string?` | The string to check. |

**Return type:** `bool` - `true` when the string is `null`, empty, or whitespace; otherwise, `false`.

```csharp
var result = "  \t".IsNullOrWhiteSpace(); // true
```

### StringExtensions.IsEmpty

Determines whether a string is empty.

| Parameter | Type | Description |
| --- | --- | --- |
| `me` | `string` | The string to check. |

**Return type:** `bool` - `true` when the string is empty; otherwise, `false`.

```csharp
var result = "".IsEmpty(); // true
```

### StringExtensions.IsEmptyOrWhiteSpace

Determines whether a string is empty or contains only whitespace characters.

| Parameter | Type | Description |
| --- | --- | --- |
| `me` | `string` | The string to check. |

**Return type:** `bool` - `true` when the string is empty or whitespace; otherwise, `false`.

```csharp
var result = "  ".IsEmptyOrWhiteSpace(); // true
```

### StringExtensions.IsCapitalized

Determines whether the first character of a string is uppercase. An empty string returns `false`.

| Parameter | Type | Description |
| --- | --- | --- |
| `me` | `string` | The string to check. |

**Return type:** `bool` - `true` when the first character is uppercase; otherwise, `false`.

```csharp
var result = "Hello".IsCapitalized(); // true
```

### StringExtensions.Capitalize

Converts the first character of a string to uppercase. `null` and empty values are returned unchanged.

| Parameter | Type | Description |
| --- | --- | --- |
| `me` | `string` | The string to convert. |

**Return type:** `string` - The string with its first character converted to uppercase.

```csharp
var result = "hello".Capitalize(); // Hello
```

### StringExtensions.Uncapitalize

Converts the first character of a string to lowercase. `null` and empty values are returned unchanged.

| Parameter | Type | Description |
| --- | --- | --- |
| `me` | `string` | The string to convert. |

**Return type:** `string` - The string with its first character converted to lowercase.

```csharp
var result = "Hello".Uncapitalize(); // hello
```

### StringExtensions.Pluralize

Returns a basic plural form of a word, including a set of common irregular plurals, while preserving the input capitalization style.

| Parameter | Type | Description |
| --- | --- | --- |
| `me` | `string` | The word to pluralize. |

**Return type:** `string` - The pluralized word.

```csharp
var regular = "box".Pluralize(); // boxes
var irregular = "Child".Pluralize(); // Children
```

### StringExtensions.Hyphenize

Converts a string to kebab-case by inserting hyphens before uppercase characters and replacing spaces with hyphens.

| Parameter | Type | Description |
| --- | --- | --- |
| `me` | `string` | The string to convert. |

**Return type:** `string` - The string converted to kebab-case.

```csharp
var result = "HelloWorld".Hyphenize(); // hello-world
```

### StringExtensions.Dehyphenize

Removes hyphens and capitalizes the character following each hyphen.

| Parameter | Type | Description |
| --- | --- | --- |
| `me` | `string` | The hyphenated string to convert. |

**Return type:** `string` - The string with hyphens removed and following characters capitalized.

```csharp
var result = "hello-world".Dehyphenize(); // helloWorld
```

### StringExtensions.Tokenize by character

Splits a string into tokens using a character separator.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `me` | `string` | Required | The string to split. |
| `separator` | `char` | Required | The character separating tokens. |
| `trim` | `bool` | `false` | Removes surrounding whitespace from each token. |
| `removeEmpty` | `bool` | `false` | Removes empty tokens from the result. |

**Return type:** `string[]` - The tokens extracted from the string.

```csharp
var values = "A | B | C".Tokenize('|', trim: true); // ["A", "B", "C"]
```

### StringExtensions.Tokenize by string

Splits a string into tokens using a string separator. An empty separator throws `ArgumentOutOfRangeException`.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `me` | `string` | Required | The string to split. |
| `separator` | `string` | Required | The string separating tokens. |
| `trim` | `bool` | `false` | Removes surrounding whitespace from each token. |
| `removeEmpty` | `bool` | `false` | Removes empty tokens from the result. |

**Return type:** `string[]` - The tokens extracted from the string.

```csharp
var values = "A<->B<->C".Tokenize("<->"); // ["A", "B", "C"]
```

### TypeExtensions.IsNullable

Determines whether a type can contain a null value. Reference types and nullable value types return `true`.

| Parameter | Type | Description |
| --- | --- | --- |
| `type` | `Type` | The type to inspect. |

**Return type:** `bool` - `true` when the type is a reference type or nullable value type; otherwise, `false`.

```csharp
var result = typeof(int?).IsNullable(); // true
```

### TypeExtensions.IsNumeric

Determines whether a type is one of the supported numeric types, including nullable numeric types.

| Parameter | Type | Description |
| --- | --- | --- |
| `type` | `Type` | The type to inspect. |

**Return type:** `bool` - `true` when the type is a supported numeric type; otherwise, `false`.

```csharp
var result = typeof(decimal).IsNumeric(); // true
```

### TypeExtensions.GetPropertyInfos

Gets the properties matching the specified reflection binding flags. Results can be cached by type and flags.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `type` | `Type` | Required | The type to inspect. |
| `useCache` | `bool` | `true` | Indicates whether cached property information may be used. |
| `flags` | `BindingFlags` | `Public \| Instance \| GetProperty` | The binding flags used to find properties. |

**Return type:** `PropertyInfo[]` - The properties matching the specified binding flags.

```csharp
var properties = typeof(Employee).GetPropertyInfos();
```

### TypeExtensions.GetPropertyNames

Gets the names of properties matching the specified reflection binding flags.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `type` | `Type` | Required | The type to inspect. |
| `useCache` | `bool` | `true` | Indicates whether cached property information may be used. |
| `flags` | `BindingFlags` | `Public \| Instance \| GetProperty` | The binding flags used to find properties. |

**Return type:** `string[]` - The names of properties matching the specified binding flags.

```csharp
var propertyNames = typeof(Employee).GetPropertyNames(); // ["Id", "Name", ...]
```
