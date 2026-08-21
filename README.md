# Bodde.Common.Extensions

This package contains a collection of minimal extension methods for the most common uses.
Please refer to the test project sources to see usage.

## String extensions

### IsNullOrEmpty
  ```csharp
  using Bodde.Common.Extensions;

  string? value = null;
  var isNullOrEmpty = value.IsNullOrEmpty(); // true
  ```

### IsNullOrWhiteSpace
  ```csharp
  using Bodde.Common.Extensions;

  string value = " \t ";
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
  var capitalized = value.Capitalize(); // Hello
  ```

### Uncapitalize
  ```csharp
  using Bodde.Common.Extensions;

  string value = "Hello";
  var uncapitalized = value.Uncapitalize(); // hello
  ```

### Pluralize
  ```csharp
  using Bodde.Common.Extensions;

  string value = "box";
  var plural = value.Pluralize(); // boxes
  ```

### Hyphenize
  ```csharp
  using Bodde.Common.Extensions;

  string value = "HelloWorld";
  var hyphenized = value.Hyphenize(); // hello-world
  ```

### Dehyphenize
  ```csharp
  using Bodde.Common.Extensions;

  string value = "hello-world";
  var dehyphenized = value.Dehyphenize(); // helloWorld
  ```

### Tokenize
  ```csharp
  using Bodde.Common.Extensions;

  string value = "A|B|C";
  var tokens = value.Tokenize('|'); // ["A", "B", "C"]

  string taggedValue = "A<->B<->C";
  var taggedTokens = taggedValue.Tokenize("<->"); // ["A", "B", "C"]

  string valueWithSpaces = " A |  | C ";
  var cleanedTokens = valueWithSpaces.Tokenize('|', trim: true, removeEmpty: true);  // ["A", "C"]
  ```

## Csv extensions

### ToCsv
  ```csharp
  using Bodde.Common.Extensions;

  string[] values = ["A", "B", "C"];
  
  var csv1 = values.ToCsv(); // A,B,C

  var csv2 = values.ToCsv(separator: "; "); // A; B; C

  var employees = new[]
  {
      new { Name = "John", Surname = "Smith", Age = 35 },
      new { Name = "Mario", Surname = "Rossi", Age = 23 }
  };

  var csv3 = employees.ToCsv(
    formatter: e => $"{e.Surname}, {e.Name} ({e.Age})",
    separator: "; "
    );
  // Smith, John (35); Rossi, Mario (23)
  ```

### FromCsv
  ```csharp
  using Bodde.Common.Extensions;

  string input1 = "A, B, C";
  var result1 = input1.FromCsv(); // ["A", "B", "C"]

  string input2 = "A, B, C";
  var result2 = input2.FromCsv(trim: false); // ["A", " B", " C"]

  string input3 = "A, B, , C";
  var result3 = input3.FromCsv(); // ["A", " B", " ", "C"]

  string input4 = "A, B, , C";
  var result4 = input4.FromCsv(removeEmpty: true); // ["A", "B", "C"]

  string input5 = "10, 20, 30";
  var result5 = input5.FromCsv<int>(); // [10, 20, 30]

  string input6 = "true, false, true";
  var result6 = input6.FromCsv<bool>(); // [true, false, true]

  string input7 = "A|B|C";
  var result7 = input7.FromCsv(separator: "|"); // ["A", "B", "C"]

  ```

