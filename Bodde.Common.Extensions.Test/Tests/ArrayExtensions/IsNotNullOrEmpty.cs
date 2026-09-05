using Bodde.Common.Extensions;

namespace ArrayExtensions;

public class IsNotNullOrEmpty
{
    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("A", true)]
    [InlineData("A,B,C", true)]
    public void Test(string? csv, bool expected)
    {
        var sut = csv?.FromCsv(removeEmpty: true);

        var actual = sut.IsNotNullOrEmpty();

        Assert.Equal(expected, actual);
    }

}