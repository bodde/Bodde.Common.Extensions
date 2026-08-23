namespace ArrayExtensions;

public class IsEmpty
{
    [Fact]
    public void Empty_True()
    {
        var sut = new string[] {};

        var actual = sut.IsEmpty();

        Assert.True(actual);
    }

    [Fact]
    public void Empty_False()
    {
        var sut = new string[] {"A", "B", "C" };

        var actual = sut.IsEmpty();

        Assert.False(actual);
    }
}