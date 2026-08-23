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

## API Reference

| Class | Method | Description |
| --- | --- | --- |
| `T[]` | [`IsEmpty<T>`](#tisemptyt) | Determines whether an array contains no elements. |
| `T[]?` | [`OrEmpty<T>`](#toremptyt) | Returns the original array, or an empty array when the value is `null`. |
| `IEnumerable<T>` | [`ToCsv<T>`](#ienumerablettocsvt) | Converts a sequence of values to a CSV-formatted string. |
| `IEnumerable<T>` | [`ToCsv<T> with formatter`](#ienumerablettocsvt-with-formatter) | Converts a sequence to CSV using a custom function to format each value. |
| `string` | [`FromCsv`](#stringfromcsv) | Splits a CSV-formatted string into an array of strings. |
| `string` | [`FromCsv<T>`](#stringfromcsv-1) | Converts a CSV-formatted string into an array of values implementing `IConvertible`. |
| `string` | [`FromCsv<T> with parser`](#stringfromcsv-with-parser) | Converts each CSV value with a custom parser function. |
| `IEnumerable<T>` | [`FormatAsTable`](#ienumerableformatastable) | Formats a sequence as a text table. |
| `string?` | [`IsNullOrEmpty`](#stringisnullorempty) | Determines whether a string is `null` or empty. |
| `string?` | [`IsNullOrWhiteSpace`](#stringisnullorwhitespace) | Determines whether a string is `null`, empty, or contains only whitespace characters. |
| `string` | [`IsEmpty`](#stringisempty) | Determines whether a string is empty. |
| `string` | [`IsEmptyOrWhiteSpace`](#stringisemptyorwhitespace) | Determines whether a string is empty or contains only whitespace characters. |
| `string` | [`IsCapitalized`](#stringiscapitalized) | Determines whether the first character of a string is uppercase. |
| `string` | [`Capitalize`](#stringcapitalize) | Converts the first character of a string to uppercase. |
| `string` | [`Uncapitalize`](#stringuncapitalize) | Converts the first character of a string to lowercase. |
| `string` | [`Pluralize`](#stringpluralize) | Returns a basic plural form of a word, including common irregular plurals. |
| `string` | [`Hyphenize`](#stringhyphenize) | Converts a string to kebab-case. |
| `string` | [`Dehyphenize`](#stringdehyphenize) | Removes hyphens and capitalizes the character following each hyphen. |
| `string` | [`Tokenize by character`](#stringtokenize-by-character) | Splits a string into tokens using a character separator. |
| `string` | [`Tokenize by string`](#stringtokenize-by-string) | Splits a string into tokens using a string separator. |
| `Type` | [`IsNullable`](#typeisnullable) | Determines whether a type can contain a null value. |
| `Type` | [`IsNumeric`](#typeisnumeric) | Determines whether a type is one of the supported numeric types. |
| `Type` | [`GetPropertyInfos`](#typegetpropertyinfos) | Gets the properties matching the specified reflection binding flags. |
| `Type` | [`GetPropertyNames`](#typegetpropertynames) | Gets the names of properties matching the specified reflection binding flags. |

### T[].IsEmpty\<T\>

Determines whether an array contains no elements.

**Return type:** `bool` - `true` when the array is empty; otherwise, `false`.

```csharp
var values = Array.Empty<int>();
var isEmpty = values.IsEmpty(); // true
```

### T[]?.OrEmpty\<T\>

Returns the original array, or an empty array when the value is `null`.

**Return type:** `T[]` - The original array, or an empty array when the value is `null`.

```csharp
int[]? values = null;
var safeValues = values.OrEmpty(); // []
```

### IEnumerable\<T\>.ToCsv\<T\>

Converts a sequence of values to a CSV-formatted string.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `separator` | `string` | `","` | The separator placed between values. |

**Return type:** `string` - The sequence formatted as a CSV string.

```csharp
var values = new[] { "A", "B", "C" };
var csv = values.ToCsv(); // A,B,C
var semicolonCsv = values.ToCsv("; "); // A; B; C
```

### IEnumerable\<T\>.ToCsv\<T\> (with formatter)

Converts a sequence to CSV using a custom function to format each value.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `formatter` | `Func<T, string>` | Required | Converts each value to a string. |
| `separator` | `string` | `","` | The separator placed between converted values. |

**Return type:** `string` - The formatted values joined into a CSV string.

```csharp
var employees = new[] { new { Name = "John", Age = 35 } };
var csv = employees.ToCsv(employee => $"{employee.Name} ({employee.Age})");
// John (35)
```

### string.FromCsv

Splits a CSV-formatted string into an array of strings.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `separator` | `string` | `","` | The separator between values. |
| `trim` | `bool` | `true` | Removes surrounding whitespace from each value. |
| `removeEmpty` | `bool` | `false` | Removes empty values from the result. |

**Return type:** `string[]` - The values extracted from the CSV string.

```csharp
var values = "A, B, C".FromCsv(); // ["A", "B", "C"]
```

### string.FromCsv<T>

Converts a CSV-formatted string into an array of values implementing `IConvertible`.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `separator` | `string` | `","` | The separator between values. |
| `trim` | `bool` | `true` | Removes surrounding whitespace from each value. |
| `removeEmpty` | `bool` | `false` | Removes empty values from the result. |

**Return type:** `T[]` - The CSV values converted to `T`.

```csharp
var values = "10, 20, 30".FromCsv<int>(); // [10, 20, 30]
```

### string.FromCsv<T> (with parser)

Converts each CSV value with a custom parser function.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `parser` | `Func<string, T>` | Required | Converts each string value to `T`. |
| `separator` | `string` | `","` | The separator between values. |
| `trim` | `bool` | `true` | Removes surrounding whitespace from each value. |
| `removeEmpty` | `bool` | `false` | Removes empty values from the result. |

**Return type:** `T[]` - The CSV values converted by the parser.

```csharp
var values = "yes,no".FromCsv(value => value == "yes"); // [true, false]
```

### IEnumerable<T>.FormatAsTable

Formats a sequence as a text table. When no columns are supplied, public properties of the item type are used automatically.

| Parameter | Type | Description |
| --- | --- | --- |
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

### string?.IsNullOrEmpty

Determines whether a string is `null` or empty.

**Return type:** `bool` - `true` when the string is `null` or empty; otherwise, `false`.

```csharp
string? value = null;
var result = value.IsNullOrEmpty(); // true
```

### string?.IsNullOrWhiteSpace

Determines whether a string is `null`, empty, or contains only whitespace characters.

**Return type:** `bool` - `true` when the string is `null`, empty, or whitespace; otherwise, `false`.

```csharp
var result = "  \t".IsNullOrWhiteSpace(); // true
```

### string.IsEmpty

Determines whether a string is empty.

**Return type:** `bool` - `true` when the string is empty; otherwise, `false`.

```csharp
var result = "".IsEmpty(); // true
```

### string.IsEmptyOrWhiteSpace

Determines whether a string is empty or contains only whitespace characters.

**Return type:** `bool` - `true` when the string is empty or whitespace; otherwise, `false`.

```csharp
var result = "  ".IsEmptyOrWhiteSpace(); // true
```

### string.IsCapitalized

Determines whether the first character of a string is uppercase. An empty string returns `false`.

**Return type:** `bool` - `true` when the first character is uppercase; otherwise, `false`.

```csharp
var result = "Hello".IsCapitalized(); // true
```

### string.Capitalize

Converts the first character of a string to uppercase. `null` and empty values are returned unchanged.

**Return type:** `string` - The string with its first character converted to uppercase.

```csharp
var result = "hello".Capitalize(); // Hello
```

### string.Uncapitalize

Converts the first character of a string to lowercase. `null` and empty values are returned unchanged.

**Return type:** `string` - The string with its first character converted to lowercase.

```csharp
var result = "Hello".Uncapitalize(); // hello
```

### string.Pluralize

Returns a basic plural form of a word, including a set of common irregular plurals, while preserving the input capitalization style.

**Return type:** `string` - The pluralized word.

```csharp
var regular = "box".Pluralize(); // boxes
var irregular = "Child".Pluralize(); // Children
```

### string.Hyphenize

Converts a string to kebab-case by inserting hyphens before uppercase characters and replacing spaces with hyphens.

**Return type:** `string` - The string converted to kebab-case.

```csharp
var result = "HelloWorld".Hyphenize(); // hello-world
```

### string.Dehyphenize

Removes hyphens and capitalizes the character following each hyphen.

**Return type:** `string` - The string with hyphens removed and following characters capitalized.

```csharp
var result = "hello-world".Dehyphenize(); // helloWorld
```

### string.Tokenize (by character)

Splits a string into tokens using a character separator.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `separator` | `char` | Required | The character separating tokens. |
| `trim` | `bool` | `false` | Removes surrounding whitespace from each token. |
| `removeEmpty` | `bool` | `false` | Removes empty tokens from the result. |

**Return type:** `string[]` - The tokens extracted from the string.

```csharp
var values = "A | B | C".Tokenize('|', trim: true); // ["A", "B", "C"]
```

### string.Tokenize (by string)

Splits a string into tokens using a string separator. An empty separator throws `ArgumentOutOfRangeException`.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `separator` | `string` | Required | The string separating tokens. |
| `trim` | `bool` | `false` | Removes surrounding whitespace from each token. |
| `removeEmpty` | `bool` | `false` | Removes empty tokens from the result. |

**Return type:** `string[]` - The tokens extracted from the string.

```csharp
var values = "A<->B<->C".Tokenize("<->"); // ["A", "B", "C"]
```

### Type.IsNullable

Determines whether a type can contain a null value. Reference types and nullable value types return `true`.

**Return type:** `bool` - `true` when the type is a reference type or nullable value type; otherwise, `false`.

```csharp
var result = typeof(int?).IsNullable(); // true
```

### Type.IsNumeric

Determines whether a type is one of the supported numeric types, including nullable numeric types.

**Return type:** `bool` - `true` when the type is a supported numeric type; otherwise, `false`.

```csharp
var result = typeof(decimal).IsNumeric(); // true
```

### Type.GetPropertyInfos

Gets the properties matching the specified reflection binding flags. Results can be cached by type and flags.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `useCache` | `bool` | `true` | Indicates whether cached property information may be used. |
| `flags` | `BindingFlags` | `Public \| Instance \| GetProperty` | The binding flags used to find properties. |

**Return type:** `PropertyInfo[]` - The properties matching the specified binding flags.

```csharp
var properties = typeof(Employee).GetPropertyInfos();
```

### Type.GetPropertyNames

Gets the names of properties matching the specified reflection binding flags.

| Parameter | Type | Default | Description |
| --- | --- | --- | --- |
| `useCache` | `bool` | `true` | Indicates whether cached property information may be used. |
| `flags` | `BindingFlags` | `Public \| Instance \| GetProperty` | The binding flags used to find properties. |

**Return type:** `string[]` - The names of properties matching the specified binding flags.

```csharp
var propertyNames = typeof(Employee).GetPropertyNames(); // ["Id", "Name", ...]
```
