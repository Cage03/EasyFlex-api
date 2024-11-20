using System.ComponentModel.DataAnnotations;

namespace Interface.Models;

public partial class CategoryModel
{
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    public virtual ICollection<SkillModel> Skills { get; set; } = new List<SkillModel>();
}
