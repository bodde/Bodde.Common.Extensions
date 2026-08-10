# Bodde.Common.Extensions

This package contains a collection of minimal extension methods for the most common uses.
Please refer to the test project sources to see usage.

## string

### IsNullOrEmpty
  ```csharp
  using Bodde.Common.Extensions;

  string? value = null;
  var isNullOrEmpty = value.IsNullOrEmpty(); // true
  ```

### IsNullOrWhiteSpace
  ```csharp
  using Bodde.Common.Extensions;

  string value = "   ";
  var isNullOrWhiteSpace = value.IsNullOrWhiteSpace(); // true
  ```

### IsEmpty
  ```csharp
  using Bodde.Common.Extensions;

  string value = "";
  var isEmpty = value.IsEmpty(); // true
  ```

### IsCapitalized
  ```csharp
  using Bodde.Common.Extensions;

  string value = "Hello";
  var isCapitalized = value.IsCapitalized(); // true
  ```

### Capitalize
  ```csharp
  using Bodde.Common.Extensions;

  string value = "hello";
  var capitalized = value.Capitalize(); // "Hello"
  ```

### Uncapitalize
  ```csharp
  using Bodde.Common.Extensions;

  string value = "Hello";
  var uncapitalized = value.Uncapitalize(); // "hello"
  ```

### Pluralize
  ```csharp
  using Bodde.Common.Extensions;

  string value = "box";
  var plural = value.Pluralize(); // "boxes"
  ```

### Hyphenize
  ```csharp
  using Bodde.Common.Extensions;

  string value = "HelloWorld";
  var hyphenized = value.Hyphenize(); // "hello-world"
  ```

### Dehyphenize
  ```csharp
  using Bodde.Common.Extensions;

  string value = "hello-world";
  var dehyphenized = value.Dehyphenize(); // "helloWorld"
  ```

## IEnumerable\<T\>

### ToCsv
  ```csharp
  using Bodde.Common.Extensions;

  string[] values = ["A", "B", "C"];
  var csv = values.ToCsv(); // "A,B,C"
  ```

### ToCsv
  ```csharp
  using Bodde.Common.Extensions;

  var employees = new[]
  {
      new { Name = "John", Surname = "Smith", Age = 35 }
      new { Name = "Mario", Surname = "Rossi", Age = 24 }
  };

  var csv = employees.ToCsv("; ", e => $"{e.Surname}, {e.Name} ({e.Age})");
  // "Smith, John (35); Rossi, Mario (24)"
  ```

