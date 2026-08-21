namespace Bodde.Common.Extensions.Test.CsvExtensions;

public class FromCsv
{
    [Theory]
    [InlineData("", true, true)]
    [InlineData("", false, true)]
    [InlineData(" ", true, true)]
    public void Empty_ToEmptyStringArray(string sut, bool trim, bool removeEmpty)
    {
        var expected = Array.Empty<string>();

        var actual = sut.FromCsv(trim: trim, removeEmpty: removeEmpty);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("", false, false)]
    [InlineData(" ", true, false)]
    public void Empty_ToStringArray_With_Empty_Element(string sut, bool trim, bool removeEmpty)
    {
        var expected = new[] { string.Empty };

        var actual = sut.FromCsv(trim: trim, removeEmpty: removeEmpty);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("1,2,3", null, true, false)]
    [InlineData("1, 2,3 ", null, true, true)]
    [InlineData("1, 2,3 ,", null, true, true)]
    [InlineData("1|@|2|@|3 ", "|@|", true, true)]
    public void ToStringArray(string sut, string? separator, bool trim, bool removeEmpty)
    {
        var expected = new string[] { "1", "2", "3" };

        var actual = separator == null
            ? sut.FromCsv(trim: trim, removeEmpty: removeEmpty)
            : sut.FromCsv(separator, trim: trim, removeEmpty: removeEmpty);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Empty_ToIntArray_With_Zero_Element()
    {
        var sut = string.Empty;
        var expected = new[] { 0 };

        var actual = sut.FromCsv<int>();

        Assert.Equal(expected, actual);
    }


    [Fact]
    public void Empty_ToIntArray_With_Default_Element()
    {
        var sut = string.Empty;
        var defaultValue = 5;
        var expected = new[] { defaultValue };

        var actual = sut.FromCsv<int>(defaultIfEmpty: defaultValue);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("1,2,3", null, false, false)]
    [InlineData("1, 2,3 ", null, true, false)]
    [InlineData("1, 2,3 ,", null, true, true)]
    [InlineData("1|@|2|@|3 ", "|@|", false, false)]
    public void ToIntArray(string sut, string? separator, bool trim, bool removeEmpty)
    {
        var expected = new int[] { 1, 2, 3 };

        var actual = separator == null
            ? sut.FromCsv<int>(trim: trim, removeEmpty: removeEmpty)
            : sut.FromCsv<int>(separator, trim: trim, removeEmpty: removeEmpty);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToIntArray_Throw()
    {
        var sut = "1,X,2";

        var ex = Assert.Throws<FormatException>(() => sut.FromCsv<int>());
        Assert.Equal("The input string 'X' was not in a correct format.", ex.Message);
    }


    [Theory]
    [InlineData("true, false, true", true, false)]
    [InlineData("TRUE,False,true,", false, true)]
    public void ToBoolArray(string sut, bool trim, bool removeEmpty)
    {
        var expected = new bool[] { true, false, true };

        var actual = sut.FromCsv<bool>(trim: trim, removeEmpty: removeEmpty);

        Assert.Equal(expected, actual);
    }


    [Fact]
    public void ToObject()
    {
        var sut = "Mario, Rossi, 23";
        var expected = new Employee("Mario", "Rossi", 23);

        var actual = sut.FromCsv(tokens => new Employee(
            Name: tokens[0], 
            Surname: tokens[1], 
            Age: int.Parse(tokens[2])
            ));

        Assert.Equal(expected, actual);
    }



    [Fact]
    public void ToObjects()
    {
        var sut = "Mario, Rossi, 23; John, Smith, 35";
        var expected = new Employee[] { new("Mario", "Rossi", 23), new("John", "Smith", 35) };

        var actual = sut
            .FromCsv(";")
            .FromCsv(tokens => new Employee(tokens[0], tokens[1], int.Parse(tokens[2])));

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Multiline_ToObjects()
    {
        var multilineCsv = string.Join(Environment.NewLine, new []
        {
            "Mario, Rossi, 23",
            "John, Smith, 35"
        });

        var expected = new Employee[] { new("Mario", "Rossi", 23), new("John", "Smith", 35) };

        var actual = multilineCsv
            .FromCsv(Environment.NewLine)
            .FromCsv(tokens => new Employee(tokens[0], tokens[1], int.Parse(tokens[2])));

        Assert.Equal(expected, actual);
    }

    private record Employee(string Name, string Surname, int Age)
    {
        public override string ToString()
        {
            return $"{Name} {Surname} ({Age})";
        }
    }
}
