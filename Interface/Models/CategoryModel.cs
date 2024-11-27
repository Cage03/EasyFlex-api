using System.ComponentModel.DataAnnotations;

namespace Interface.Models;

public class CategoryModel
{
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    public ICollection<SkillModel> Skills { get; set; } = new List<SkillModel>();
}
