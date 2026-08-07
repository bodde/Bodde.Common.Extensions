namespace Bodde.Common.Extensions.Test.StringExtensions;

public class Pluralize
{
    [Theory]
    [InlineData("", "")]
    [InlineData("object", "objects")]
    [InlineData("Object", "Objects")]
    [InlineData("class", "classes")]
    [InlineData("CLASS", "CLASSES")]
    [InlineData("Class", "Classes")]
    [InlineData("buzz", "buzzes")]
    [InlineData("box", "boxes")]
    [InlineData("church", "churches")]
    [InlineData("baby", "babies")]
    [InlineData("city", "cities")]
    [InlineData("toy", "toys")]
    [InlineData("potato", "potatoes")]
    [InlineData("man", "men")]
    [InlineData("Man", "Men")]
    [InlineData("MAN", "MEN")]
    [InlineData("woman", "women")]
    [InlineData("foot", "feet")]
    [InlineData("tooth", "teeth")]
    [InlineData("mouse", "mice")]
    [InlineData("child", "children")]
    [InlineData("person", "people")]
    [InlineData("sheep", "sheep")]
    [InlineData("fish", "fish")]
    [InlineData("deer", "deer")]
    public void Test(string sut, string expected)
    {
        var actual = sut.Pluralize();

        Assert.Equal(expected, actual);
    }
}