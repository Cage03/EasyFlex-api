using Interface.Models;

namespace Interface.Dtos;

public record Flexworker
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Address { get; init; }
    public DateOnly DateOfBirth { get; init; }
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
    public string? ProfilePictureUrl { get; init; }
    public virtual ICollection<Skill> Skills { get; init; } = new List<Skill>();
}