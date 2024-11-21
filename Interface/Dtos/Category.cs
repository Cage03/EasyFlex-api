using Interface.Models;

namespace Interface.Dtos;

public record Category
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public ICollection<Skill> Skills { get; init; } = null!;
}