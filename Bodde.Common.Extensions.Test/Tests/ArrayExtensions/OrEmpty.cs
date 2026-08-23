namespace ArrayExtensions;

public class OrEmpty
{
    [Fact]
    public void Null_Empty()
    {
        string[]? sut = null;

        var actual = sut.OrEmpty();

        Assert.NotNull(actual);
        Assert.Empty(actual);
    }

    [Fact]
    public void NotNull_Same()
    {
        var sut = new string[] {"A", "B", "C" };
        var expected = sut;

        var actual = sut.OrEmpty();

        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }
}