namespace Bodde.Common.Extensions.Test.Models;

internal record Employee(
    int Id,
    string Name,
    string Surname,
    int Age,
    Department Department,
    Employee? Manager = null
    )
{
    public override string ToString() => $"{Id} - {Name} {Surname}";
}