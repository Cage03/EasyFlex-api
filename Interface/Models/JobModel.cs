using System.ComponentModel.DataAnnotations;

namespace Interface.Models;

public partial class JobModel
{
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Address { get; set; }

    public string? Description { get; set; }

    public int MinHours { get; set; }

    public int MaxHours { get; set; }

    [Required]
    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public ICollection<SkillModel> Skills { get; set; } = new List<SkillModel>();

}
