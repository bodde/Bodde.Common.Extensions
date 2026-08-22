namespace Bodde.Common.Extensions.Test.Tests.StringExtensions;

public class IsNullable
{
    [Fact]
    public void Int32_False()
    {
        var sut = typeof(int);

        var actual = sut.IsNullable();

        Assert.False(actual);
    }

    [Fact]
    public void NullableInt32_True()
    {
        var sut = typeof(int?);

        var actual = sut.IsNullable();

        Assert.True(actual);
    }

    [Fact]
    public void String_True()
    {
        var sut = typeof(string);

        var actual = sut.IsNullable();

        Assert.True(actual);
    }


    [Fact]
    public void ArrayInt32_True()
    {
        var sut = typeof(int[]);

        var actual = sut.IsNullable();

        Assert.True(actual);
    }


    [Fact]
    public void DateTime_False()
    {
        var sut = typeof(DateTime);

        var actual = sut.IsNullable();

        Assert.False(actual);
    }
}