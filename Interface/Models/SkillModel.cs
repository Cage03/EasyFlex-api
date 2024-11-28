using System.ComponentModel.DataAnnotations;

namespace Interface.Models;

public partial class SkillModel
{
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public int CategoryId { get; set; }
    [Required]
    public virtual CategoryModel Category { get; set; } = null!;
    
}
