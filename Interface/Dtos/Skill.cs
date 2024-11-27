using Interface.Models;

namespace Interface.Dtos;

public record Skill
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    
}