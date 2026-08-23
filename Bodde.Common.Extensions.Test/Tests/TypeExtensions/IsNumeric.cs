namespace TypeExtensions;

public class IsNumeric
{
    [Theory]
    [InlineData("System.SByte", true)]
    [InlineData("System.Byte", true)]
    [InlineData("System.Int16", true)]
    [InlineData("System.UInt16", true)]
    [InlineData("System.Int32", true)]
    [InlineData("System.UInt32", true)]
    [InlineData("System.Int64", true)]
    [InlineData("System.UInt64", true)]
    [InlineData("System.Single", true)]
    [InlineData("System.Double", true)]
    [InlineData("System.Decimal", true)]
    [InlineData("System.Nullable`1[System.Int32]", true)]
    [InlineData("System.String", false)]
    [InlineData("System.Boolean", false)]
    [InlineData("System.DateTime", false)]
    [InlineData("System.Int32[]", false)]
    [InlineData("Bodde.Common.Extensions.Test.Models.Employee", false)]
    public void Test(string typeName, bool expected)
    {
        var sut = Type.GetType(typeName)!;

        var actual = sut.IsNumeric();

        Assert.Equal(expected, actual);
    }
}