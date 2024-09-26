namespace Interface.Models;

public partial class JobModel
{
    public int Id { get; set; }

    public string Adress { get; set; } = null!;

    public string? Description { get; set; }

    public int MinHours { get; set; }

    public int MaxHours { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual ICollection<FlexworkerModel> Flexworkers { get; set; } = new List<FlexworkerModel>();

    public virtual ICollection<SkillModel> Skills { get; set; } = new List<SkillModel>();

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
