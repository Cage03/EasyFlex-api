using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public partial class SkillModel
{
    public int Id { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public virtual CategoryModel Category { get; set; } = null!;

    // public SkillDto ToDto()
    // {
    //     return new SkillDto
    //     {
    //         Id = Id,
    //         CategoryId = CategoryId,
    //         Name = Name,
    //         Category = Category.ToDto(),
    //         Flexworkers = Flexworkers.Select(f => f.ToDto()).ToList(),
    //         Jobs = Jobs.Select(j => j.ToDto()).ToList()
    //     };
    // }
}
