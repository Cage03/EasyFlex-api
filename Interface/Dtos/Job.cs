using Interface.Models;

namespace Interface.Dtos;

public record Job
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Address { get; init; }
    public string? Description { get; init; }
    public int MinHours { get; init; }
    public int MaxHours { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
    public ICollection<Preference> Preferences { get; init; } = new List<Preference>();
}