using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public partial class CategoryModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public virtual ICollection<SkillModel> Skills { get; set; } = new List<SkillModel>();

    // public CategoryDto ToDto()
    // {
    //     return new CategoryDto
    //     {
    //         Id = Id,
    //         Name = Name,
    //         Skills = Skills.Select(s => s.ToDto()).ToList()
    //     };
    // }
}
