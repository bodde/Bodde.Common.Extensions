
using Bodde.Common.Extensions;

string? value = null;
var isNullOrEmpty = value.IsNullOrEmpty(); // true
Console.WriteLine($"IsNullOrEmpty: [null] => {isNullOrEmpty}");

value = " \t ";
var isNullOrWhiteSpace = value.IsNullOrWhiteSpace(); // true
Console.WriteLine($"IsNullOrWhiteSpace: [{value}] => {isNullOrWhiteSpace}");

value = "";
var isEmpty = value.IsEmpty(); // true
Console.WriteLine($"IsEmpty: [{value}] => {isEmpty}");

value = "Hello";
var isCapitalized = value.IsCapitalized(); // true
Console.WriteLine($"IsCapitalized: [{value}] => {isCapitalized}");

value = "hello";
var capitalized = value.Capitalize(); // Hello
Console.WriteLine($"Capitalize: [{value}] => {capitalized}");

value = "Hello";
var uncapitalized = value.Uncapitalize(); // hello
Console.WriteLine($"Uncapitalize: [{value}] => {uncapitalized}");

value = "box";
var plural = value.Pluralize(); // boxes
Console.WriteLine($"Pluralize: [{value}] => {plural}");

value = "HelloWorld";
var hyphenized = value.Hyphenize(); // hello-world
Console.WriteLine($"Hyphenize: [{value}] => {hyphenized}");

value = "hello-world";
var dehyphenized = value.Dehyphenize(); // helloWorld
Console.WriteLine($"Dehyphenize: [{value}] => {dehyphenized}");

value = "A|B|C";
var tokens = value.Tokenize('|'); // ["A", "B", "C"]
Console.WriteLine($"Tokenize: [{value}] => {tokens.ToCsv()}");

string taggedValue = "A<->B<->C";
var taggedTokens = taggedValue.Tokenize("<->"); // ["A", "B", "C"]
Console.WriteLine($"Tokenize: [{taggedValue}] => {taggedTokens.ToCsv()}");

string valueWithSpaces = " A |  | C ";
var cleanedTokens = valueWithSpaces.Tokenize('|', trim: true, removeEmpty: true);  // ["A", "C"]
Console.WriteLine($"Tokenize: [{valueWithSpaces}] => {cleanedTokens.ToCsv()}");

string[] values = ["A", "B", "C"];

var csv1 = values.ToCsv(); // A,B,C
Console.WriteLine($"ToCsv: [{values.ToCsv()}] => {csv1}"); // A,B,C

var csv2 = values.ToCsv(separator: "; "); // A; B; C
Console.WriteLine($"ToCsv: [{values.ToCsv()}] => {csv2}"); // A,B,C

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

Console.WriteLine($"ToCsv: <employees> => {csv3}"); 

string input1 = "A, B, C";
var result1 = input1.FromCsv(); // ["A", "B", "C"]
Console.WriteLine($"FromCsv: [{input1}] => {result1.ToCsv()}"); 

string input2 = "A, B, C";
var result2 = input2.FromCsv(trim: false); // ["A", " B", " C"]
Console.WriteLine($"FromCsv: [{input2}] => {result2.ToCsv()}"); 

string input3 = "A, B, , C";
var result3 = input3.FromCsv(); // ["A", " B", " ", "C"]
Console.WriteLine($"FromCsv: [{input3}] => {result3.ToCsv()}"); 

string input4 = "A, B, , C";
var result4 = input4.FromCsv(removeEmpty: true); // ["A", "B", "C"]
Console.WriteLine($"FromCsv: [{input4}] => {result4.ToCsv()}"); 

string input5 = "10, 20, 30";
var result5 = input5.FromCsv<int>(); // [10, 20, 30]
Console.WriteLine($"FromCsv<int>: [{input5}] => {result5.ToCsv()}"); 

string input6 = "true, false, true";
var result6 = input6.FromCsv<bool>(); // [true, false, true]
Console.WriteLine($"FromCsv<bool>: [{input6}] => {result6.ToCsv()}"); 

string input7 = "A|B|C";
var result7 = input7.FromCsv(separator: "|"); // ["A", "B", "C"]
Console.WriteLine($"FromCsv: [{input7}] => {result7.ToCsv()}"); 

