using System.Text.RegularExpressions;
using Bodde.Common.Extensions;

namespace RegexExtensions;

public class FromMatchingValues
{
    [Fact]
    public void ToEmployee()
    {
        var pattern = @"(?<Id>\d+)\.\s+(?<FirstName>\w+)\s(?<LastName>\w+)";
        var input = "1. John Smith";

        var sut = new Regex(pattern);
        var actual = sut.FromMatchingValues<Person>(input);
        
        Assert.Equal(1, actual.Id);
        Assert.Equal("John", actual.FirstName);
        Assert.Equal("Smith", actual.LastName);
    }

    private class Person
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}