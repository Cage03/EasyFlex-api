using System.ComponentModel.DataAnnotations;

namespace Interface.Models;

public partial class JobModel
{
    public int Id { get; set; }

    [Required] 
    public string Name { get; set; }

    [Required]
    public string Address { get; set; } = null!;

    [Required]
    public string? Description { get; set; }

    [Required]
    public int MinHours { get; set; }

    [Required]
    public int MaxHours { get; set; }

    [Required]
    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual ICollection<PreferenceModel> Preferences { get; set; } = new List<PreferenceModel>();

    // public JobDto ToDto()
    // {
    //     return new JobDto
    //     {
    //         Id = Id,
    //         Adress = Adress,
    //         Description = Description,
    //         MinHours = MinHours,
    //         MaxHours = MaxHours,
    //         StartDate = StartDate,
    //         EndDate = EndDate,
    //         Flexworkers = Flexworkers.Select(f => f.ToDto()).ToList(),
    //         Skills = Skills.Select(s => s.ToDto()).ToList()
    //     };
    // }
}
