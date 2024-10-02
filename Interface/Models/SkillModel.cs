using System.ComponentModel.DataAnnotations;

namespace Interface.Models;

public partial class SkillModel
{
    public int Id { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public virtual CategoryModel Category { get; set; } = null!;

    public virtual ICollection<FlexworkerModel> Flexworkers { get; set; } = new List<FlexworkerModel>();
    public virtual ICollection<JobModel> Jobs { get; set; } = new List<JobModel>();

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
