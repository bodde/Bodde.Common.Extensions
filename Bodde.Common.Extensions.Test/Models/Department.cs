namespace Bodde.Common.Extensions.Test.Models;

internal record Department(int Id, string Name)
{
    public override string ToString() => $"{Id} - {Name}";
}
