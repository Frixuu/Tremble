namespace Tremble;

public readonly record struct User
{
    public long Id { get; init; }

    public string Name { get; init; }
}
