using Bodde.Common.Extensions;

namespace StringExtensions;

public class ConvertTo
{
    [Theory]
    [InlineData("1", typeof(int), 1)]
    [InlineData("1", typeof(int?), 1)]
    [InlineData("1", typeof(long), 1L)]    
    [InlineData("3.14", typeof(double), 3.14D)]
    [InlineData("3.14", typeof(float), 3.14F)]
    [InlineData("true", typeof(bool), true)]
    [InlineData("TRUE", typeof(bool), true)]
    [InlineData("Senior", typeof(RoleType), RoleType.Senior)]
    public void To_Types(string sut, Type targetType, object? expected)
    {
        var actual = sut.ConvertTo(targetType);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void To_Decimal()
    {
        var sut = "3.14";
        var expected = 3.14M;
        var actual = sut.ConvertTo<decimal>();

        Assert.Equal(expected, actual);
    }


    [Fact]
    public void To_TimeSpan()
    {
        var sut = "2:15:30";
        var expected = new TimeSpan(hours: 2, minutes: 15, seconds: 30);
        var actual = sut.ConvertTo<TimeSpan>();

        Assert.Equal(expected, actual);
    }



    [Fact]
    public void To_DateTime_Utc()
    {
        var sut = "2009-06-15T13:45:30Z";
        var expected = new DateTime(year: 2009, month: 6, day: 15, hour: 13, minute: 45, second: 30, DateTimeKind.Utc);
        var actual = sut.ConvertTo<DateTime>();

        Assert.Equal(expected.ToUniversalTime(), actual.ToUniversalTime());
    }

    [Fact]
    public void To_DateTime_Local()
    {
        var sut = "2009-06-15T13:45:30";
        var expected = new DateTime(year: 2009, month: 6, day: 15, hour: 13, minute: 45, second: 30);
        var actual = sut.ConvertTo<DateTime>();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void To_DateTimeOffset()
    {
        var sut = "2009-06-15T13:45:30Z";
        var expected = new DateTimeOffset(year: 2009, month: 6, day: 15, hour: 13, minute: 45, second: 30, TimeSpan.Zero);
        var actual = sut.ConvertTo<DateTimeOffset>();

        Assert.Equal(expected, actual);
    }




    public enum RoleType
    {
        Manager,
        Senior,
        Staff,
    }
}