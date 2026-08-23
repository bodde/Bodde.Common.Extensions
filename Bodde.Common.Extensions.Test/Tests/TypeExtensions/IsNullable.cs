namespace TypeExtensions;

public class IsNullable
{

    [Theory]
    [InlineData("System.Int32", false)]
    [InlineData("System.Nullable`1[System.Int32]", true)]
    [InlineData("System.String", true)]
    [InlineData("System.Int32[]", true)]
    [InlineData("System.DateTime", false)]
    [InlineData("Bodde.Common.Extensions.Test.Models.Employee", true)]
    public void Test(string typeName, bool expected)
    {
        var sut = Type.GetType(typeName)!;

        var actual = sut.IsNullable();

        Assert.Equal(expected, actual);
    }
}