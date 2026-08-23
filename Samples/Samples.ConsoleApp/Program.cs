
using Bodde.Common.Extensions;

StringSamples();
ToCsvSamples();
FromCsvSamples();
ArraySamples();
TypeSamples();
FormatSamples();

static void StringSamples()
{
    Console.WriteLine();
    Console.WriteLine("String Samples");
    Console.WriteLine("------------------------------------------------------------------------------");

    Console.WriteLine($"((string?)null).IsNullOrEmpty() => {((string?)null).IsNullOrEmpty()}"); // true

    var value = " \t ";
    Console.WriteLine($"{value.Display()}.IsNullOrWhiteSpace() => {value.IsNullOrWhiteSpace()}"); // true

    value = "";
    Console.WriteLine($"{value.Display()}.IsEmpty => {value.IsEmpty()}"); // true

    value = "Hello";
    Console.WriteLine($"{value.Display()}.IsCapitalized() => {value.IsCapitalized()}"); // true

    value = "hello";
    Console.WriteLine($"{value.Display()}.Capitalize() => {value.Capitalize()}"); // "Hello"

    value = "Hello";
    Console.WriteLine($"{value.Display()}.Uncapitalize() => {value.Uncapitalize()}"); // "hello"

    value = "box";
    Console.WriteLine($"{value.Display()}.Pluralize() => {value.Pluralize()}"); // "boxes"

    value = "HelloWorld";
    Console.WriteLine($"{value.Display()}.Hyphenize() => {value.Hyphenize()}"); // "hello-world"

    value = "hello-world";
    Console.WriteLine($"{value.Display()}.Dehyphenize() => {value.Dehyphenize()}"); // "helloWorld"

    value = "A,B,C";
    Console.WriteLine($"{value.Display()}.Tokenize(',') => {value.Tokenize(',').Display()}"); // ["A", "B", "C"]

    value = "A, B, C";
    Console.WriteLine($"{value.Display()}.Tokenize(',') => {value.Tokenize(',').Display()}"); // ["A", " B", " C"]

    value = "A, B, C";
    Console.WriteLine($"{value.Display()}.Tokenize(',', trim: true) => {value.Tokenize(',', trim: true).Display()}"); // ["A", "B", "C"]

    value = "A|B|C";
    Console.WriteLine($"{value.Display()}.Tokenize('|') => {value.Tokenize('|').Display()}"); // ["A", "B", "C"]

    value = "A||C";
    Console.WriteLine($"{value.Display()}.Tokenize('|') => {value.Tokenize('|').Display()}"); // ["A", "", "C"]

    value = "A||C";
    Console.WriteLine($"{value.Display()}.Tokenize('|', removeEmpty: true) => {value.Tokenize('|', removeEmpty: true).Display()}"); // ["A", "C"]

    value = "A| |C";
    Console.WriteLine($"{value.Display()}.Tokenize('|', removeEmpty: true) => {value.Tokenize('|', removeEmpty: true).Display()}"); // ["A", " ", "C"]

    value = "A| |C";
    Console.WriteLine($"{value.Display()}.Tokenize('|', trim: true, removeEmpty: true) => {value.Tokenize('|', trim: true, removeEmpty: true).Display()}"); // ["A", "C"]

    value = "A<->B<->C";
    Console.WriteLine($"{value.Display()}.Tokenize(separator: \"<->\") => {value.Tokenize("<->").Display()}");

}

static void ToCsvSamples()
{
    Console.WriteLine();
    Console.WriteLine("ToCsv Samples");
    Console.WriteLine("------------------------------------------------------------------------------");

    string[] values = ["A", "B", "C"];

    Console.WriteLine($"{values.Display()}.ToCsv() => {values.ToCsv().Display()}"); // "A,B,C"

    Console.WriteLine($"{values.Display()}.ToCsv(separator: \"; \") => \"{values.ToCsv(separator: "; ")}\""); //  "A; B; C"

    var employees = new[]
    {
        new { Name = "John", Surname = "Smith", Age = 35 },
        new { Name = "Mario", Surname = "Rossi", Age = 23 }
    };

    var csv3 = employees.ToCsv(
        formatter: e => $"{e.Surname}, {e.Name} ({e.Age})",
        separator: "; "
    );
    Console.WriteLine($"<employees>.ToCsv(formatter: e => $\"{{e.Surname}}, {{e.Name}} {{e.Age}})\", separator: \"; \") => {csv3.Display()}"); // "Smith, John (35); Rossi, Mario (23)"
}

static void FromCsvSamples()
{
    Console.WriteLine();
    Console.WriteLine("FromCsv Samples");
    Console.WriteLine("------------------------------------------------------------------------------");

    string input1 = "A, B, C";
    Console.WriteLine($"{input1.Display()}.FromCsv() => {input1.FromCsv().Display()}"); // ["A", "B", "C"]

    string input2 = "A, B, C";
    Console.WriteLine($"{input2.Display()}.FromCsv(trim: false): {input2.FromCsv(trim: false).Display()}"); // ["A", " B", " C"]

    string input3 = "A, B, , C";
    Console.WriteLine($"{input3.Display()}.FromCsv() => {input3.FromCsv().Display()}"); // ["A", " B", " ", "C"]

    string input4 = "A, B, , C";
    Console.WriteLine($"{input4.Display()}.FromCsv(removeEmpty: true) => {input4.FromCsv(removeEmpty: true).Display()}"); // ["A", "B", "C"]

    string input5 = "10, 20, 30";
    Console.WriteLine($"{input5.Display()}.FromCsv<int>() => {input5.FromCsv<int>().Display()}"); // [10, 20, 30]

    string input6 = "true, false, true";
    Console.WriteLine($"{input6.Display()}.FromCsv<bool>() => {input6.FromCsv<bool>().Display()}"); // [true, false, true]

    string input7 = "A|B|C";
    Console.WriteLine($"{input7.Display()}.FromCsv(separator: \"|\") => {input7.FromCsv(separator: "|").Display()}"); // ["A", "B", "C"]
}

static void ArraySamples()
{
    Console.WriteLine();
    Console.WriteLine("Array Samples");
    Console.WriteLine("------------------------------------------------------------------------------");

    string[] emptyValues = [];
    Console.WriteLine($"{emptyValues.Display()}.IsEmpty() => {emptyValues.IsEmpty()}"); // true

    int[] values = [1, 2, 3];
    Console.WriteLine($"{values.Display()}.IsEmpty() => {values.IsEmpty()}"); // false

    string[]? nullValues = null;
    Console.WriteLine($"((string[])null).OrEmpty() => {nullValues.OrEmpty().Display()}"); // []

    Console.WriteLine($"{values.Display()}.OrEmpty() => {values.OrEmpty().Display()}"); // [1, 2, 3]
}

static void TypeSamples()
{
    Console.WriteLine();
    Console.WriteLine("Type Samples");
    Console.WriteLine("------------------------------------------------------------------------------");

    Console.WriteLine($"typeof(Employee).GetPropertyNames() => {typeof(Employee).GetPropertyNames().Display()}");
    Console.WriteLine($"typeof(Employee).GetPropertyInfos() => {typeof(Employee).GetPropertyInfos().Display()}");
}

static void FormatSamples()
{
    Console.WriteLine();
    Console.WriteLine("Format Samples");
    Console.WriteLine("------------------------------------------------------------------------------");

    var engineering = new Department(1, "Engineering");

    var john = new Employee(1, "John", "Smith", 35, engineering);
    var mario = new Employee(2, "Mario", "Rossi", 23, engineering, john);

    Employee[] employees = [john, mario];

    Console.WriteLine("\n<employees>.FormatAsTable()");
    Console.WriteLine(employees.FormatAsTable());

    Console.WriteLine("\n<employees>.FormatAsTable([new (_ => _.Name),new (_ => _.Surname), new (_ => _.Department.Name, \"Department\")])");
    Console.WriteLine(employees.FormatAsTable([new(_ => _.Name), new(_ => _.Surname), new(_ => _.Department.Name, "Department")]));
}

internal static class DisplayExtensions
{
    internal static string Display<T>(this T[] values)
    {
        return String.Concat("[", values.ToCsv(_ => Display(_)), "]");
    }

    internal static string Display(this string value)
    {
        return String.Concat("\"", value, "\"");
    }

    internal static string Display<T>(this T value)
    {
        return value?.ToString() ?? "<null>";
    }
}

internal record Department(int Id, string Name)
{
    public override string ToString() => $"{Id} - {Name}";
}

internal record Employee(
    int Id,
    string Name,
    string Surname,
    int Age,
    Department Department,
    Employee? Manager = null
    )
{
    public override string ToString() => $"{Id} - {Name} {Surname}";
}
