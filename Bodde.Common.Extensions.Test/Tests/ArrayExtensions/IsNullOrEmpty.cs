using Bodde.Common.Extensions;

namespace ArrayExtensions;

public class IsNullOrEmpty
{
    [Theory]
    [InlineData(null, true)]
    [InlineData("", true)]
    [InlineData("A", false)]
    [InlineData("A,B,C", false)]
    public void Test(string? csv, bool expected)
    {
        var sut = csv?.FromCsv(removeEmpty: true);

        var actual = sut.IsNullOrEmpty();

        Assert.Equal(expected, actual);
    }

}